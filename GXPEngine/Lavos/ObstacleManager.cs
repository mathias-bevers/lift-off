using System.Collections.Generic;
using GXPEngine;

namespace Lavos
{
	public class ObstacleManager : GameObject
	{
		private const float SPEED_UP_INCREMENT = 0.25f;
		private const int LANES_COUNT = 3;

		private readonly List<Obstacle> obstacles = new();

		public float ObstacleSpeed { get; private set; } = 2.25f;

		private float spawnInterval;
		private int lastSpawnTime;

		public ObstacleManager()
		{
			spawnInterval = game.width / (ObstacleSpeed * game.targetFps);
			DeployObstacles();
		}

		private void Update()
		{
			var pendingDestroy = new List<Obstacle>();

			foreach (Obstacle obstacle in obstacles)
			{
				if (obstacle.x < -obstacle.width)
				{
					pendingDestroy.Add(obstacle);
					continue;
				}

				obstacle.x -= ObstacleSpeed;
			}

			foreach (Obstacle obstacle in pendingDestroy)
			{
				obstacles.Remove(obstacle);
				obstacle.Destroy();
			}

			float timeSurvived = ((GameScene)SceneManager.Instance.CurrentScene).TimeSurvived / 1000.0f;
			if (timeSurvived < (lastSpawnTime / 1000) + spawnInterval) { return; }

			DeployObstacles();
		}

		private void DeployObstacles()
		{
			if (obstacles.Count >= 3) { return; } // Make sure there is only one line of obstacles at the time.

			for (var i = 0; i < LANES_COUNT; i++)
			{
				var obstacle = new Obstacle("colors.png", i);
				obstacles.Add(obstacle);
				AddChild(obstacle);
			}

			ObstacleSpeed += SPEED_UP_INCREMENT;

			spawnInterval = game.width / (ObstacleSpeed * game.currentFps);

			lastSpawnTime = Time.time;
		}
	}
}