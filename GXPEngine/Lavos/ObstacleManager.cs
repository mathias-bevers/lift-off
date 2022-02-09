using System.Collections.Generic;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class ObstacleManager : GameObject
	{
		private const float SPEED_UP_INCREMENT = 0.25f;
		private const int LANES_COUNT = 3;

		private readonly List<Sprite> obstacles = new();

		private float spawnInterval;
		private float obstacleSpeed = 2.25f; 
		private int lastSpawnTime;

		public ObstacleManager()
		{
			spawnInterval = game.width / (obstacleSpeed * game.targetFps);
			DeployObstacles();
		}

		private void Update()
		{
			var pendingDestroy = new List<Sprite>();

			foreach (Sprite obstacle in obstacles)
			{
				if (obstacle.x < -obstacle.width)
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
			if (obstacles.Count >= 3) { return; } // Make sure there is only one line of obstacles at the time.

			Debug.Log($"speed: {obstacleSpeed:n2}\t interval: {spawnInterval}.");

			for (var i = 0; i < LANES_COUNT; i++)
			{
				var obstacle = new Obstacle("colors.png", i);
				obstacles.Add(obstacle);
				AddChild(obstacle);
			}

			obstacleSpeed += SPEED_UP_INCREMENT;

			spawnInterval = game.width / (obstacleSpeed * game.currentFps);

			lastSpawnTime = Time.time;
		}
	}
}