using System;
using System.Collections.Generic;
using System.Timers;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class ObstacleManager : GameObject
	{
		private const float SPEED_UP_INCREMENT = 0.25f;
		private const int LANES_COUNT = 3;
		private readonly Dictionary<int, int> lanes = new();

		public float DeployableSpeed { get; private set; } = 6.25f;
		private float SpawnInterval => game.width / (DeployableSpeed * game.currentFps) / 3;

		private int objectsDeployed;
		private readonly Timer timer;

		public ObstacleManager()
		{
			for (var i = 0; i < LANES_COUNT; i++)
			{
				lanes.Add(i, 0);
			}

			timer = new Timer(SpawnInterval);
			timer.Elapsed += OnTimer;
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		private void DeployInLane(int laneNumber)
		{
			if (new Random().Next(0, 2) != 0)
			{
				var obstacle = new Obstacle("circle.png", laneNumber, DeployableSpeed);
				obstacle.OnDestroyed += OnDeployableDestroy;
				AddChild(obstacle);
			}
			else
			{
				var obstacle = new Pickup("colors.png", laneNumber, DeployableSpeed);
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

		private void OnTimer(object source, ElapsedEventArgs data)
		{
			TryDeployment(new Random().Next(0, LANES_COUNT));
			timer.Interval = SpawnInterval * 1000;

			void TryDeployment(int laneNumber)
			{
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
			}
		}
	}
}