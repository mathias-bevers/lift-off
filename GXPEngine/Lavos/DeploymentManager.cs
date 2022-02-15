using System;
using System.Collections.Generic;
using GXPEngine;

namespace Lavos
{
	public class DeploymentManager : GameObject
	{
		private const float SPEED_UP_INCREMENT = 0.25f;
		private const int LANES_COUNT = 3;
		private readonly Dictionary<int, int> lanes = new();

		public float DeployableSpeed { get; private set; } = 6.25f;
		private float SpawnInterval => (game.width / (DeployableSpeed * game.targetFps) / 3) * 1000;
		private bool isTryingDeployment;
		private float lastSpawnTime;

		private int objectsDeployed;

		public DeploymentManager()
		{
			for (var i = 0; i < LANES_COUNT; i++) { lanes.Add(i, 0); }
		}

		private void Update()
		{
			if (isTryingDeployment) { return; }

			if (Time.time < SpawnInterval + lastSpawnTime) { return; }

			TryDeployment(new Random().Next(0, LANES_COUNT));
		}

		private void DeployInLane(int laneNumber)
		{
			if (new Random().Next(0, 101) < 75)
			{
				var obstacle = new Obstacle(laneNumber, DeployableSpeed);
				obstacle.OnDestroyed += OnDeployableDestroy;
				AddChild(obstacle);
			}
			else
			{
				var obstacle = new Pickup(laneNumber, DeployableSpeed);
				obstacle.OnDestroyed += OnDeployableDestroy;
				AddChild(obstacle);
			}

			++lanes[laneNumber];
			++objectsDeployed;
			if (objectsDeployed % 3 != 0) { return; }

			DeployableSpeed += SPEED_UP_INCREMENT;
		}

		private void OnDeployableDestroy(Deployable deployable)
		{
			deployable.OnDestroyed -= OnDeployableDestroy;
			--lanes[deployable.LaneNumber];
		}

		private void TryDeployment(int laneNumber)
		{
			isTryingDeployment = true;

			while (SceneManager.Instance.CurrentScene.Name == "game")
			{
				if (lanes[laneNumber] >= 2)
				{
					laneNumber = new Random().Next(0, LANES_COUNT);
					continue;
				}

				DeployInLane(laneNumber);
				break;
			}

			lastSpawnTime = Time.time;
			isTryingDeployment = false;
		}
	}
}