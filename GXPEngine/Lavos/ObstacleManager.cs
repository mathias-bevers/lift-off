using System.Collections.Generic;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class ObstacleManager : GameObject
	{
		private const int LANES_COUNT = 3;

		private readonly float spawnInterval = 5.0f;
		private readonly float obstacleSpeed = 2.5f;

		private int lastSpawnTime;
		private readonly List<Sprite> obstacles = new();

		private void Update()
		{
			var pendingDestroy = new List<Sprite>();

			foreach (Sprite obstacle in obstacles)
			{
				if (obstacle.x < obstacle.width)
				{
					pendingDestroy.Add(obstacle);
					continue;
				}

				obstacle.x -= obstacleSpeed;
			}

			foreach (Sprite obstacle in pendingDestroy)
			{
				obstacles.Remove(obstacle);
				obstacle.Destroy();
			}

			if (MyGame.Instance.TimeSurvived < (lastSpawnTime / 1000) + spawnInterval) { return; }

			DeployObstacles();
		}

		private void DeployObstacles()
		{
			Debug.Log("Deploying obstacles");

			for (var i = 0; i < LANES_COUNT; i++)
			{
				var obstacle = new Obstacle("colors.png", i);
				obstacles.Add(obstacle);
				AddChild(obstacle);
			}

			lastSpawnTime = Time.time;
		}
	}
}