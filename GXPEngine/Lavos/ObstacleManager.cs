using System.Collections.Generic;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class ObstacleManager : GameObject
	{
		private const int GROUND_HEIGHT = 64;
		private EasyDraw easyDraw;

		private readonly float spawnInterval = 5.0f;
		private readonly float obstacleSpeed = 2.5f;
		private readonly List<Sprite> obstacles = new();

		private int lastSpawnTime;

		public ObstacleManager() => easyDraw = new EasyDraw(200, 150);

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

			if (MyGame.Instance.TimeSurvived < lastSpawnTime / 1000 + spawnInterval) { return; }

			DeployObstacle();
		}

		private void DeployObstacle()
		{
			Debug.Log("Deploying obstacle");

			var obstacle = new Sprite("colors.png");
			obstacle.SetCollider();
			obstacle.collider.isTrigger = true;
			obstacle.SetXY(game.width, game.height - GROUND_HEIGHT - obstacle.height);
			obstacles.Add(obstacle);
			AddChild(obstacle);

			lastSpawnTime = Time.time;
		}
	}
}