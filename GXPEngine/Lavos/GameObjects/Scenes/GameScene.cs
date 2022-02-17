using System;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class GameScene : Scene
	{
		public float Score => (TimeSurvived / 1000.0f) *
		                      (deploymentManager.DeployableSpeed - (deploymentManager.DeployableSpeed - 1.0f));

		public int TimeSurvived { get; private set; }
		public Player Player { get; private set; }

		public override string Name { get; protected set; } = "game";
		private DeploymentManager deploymentManager;

		private int startTime;
		private int lastLaserSpawnTime;

		public GameScene() { Start(); }

		public override void Start()
		{
			AddChild(new Sprite("background.png"));

			var foreground = new Sprite("foreground.png");
			foreground.SetXY(0, game.height - foreground.height);
			foreground.SetCollider();
			AddChild(foreground);

			Player = new Player("MainCharacterSpriteSheet.png", 8, 7);
			AddChild(Player);

			deploymentManager = new DeploymentManager();
			AddChild(deploymentManager);

			AddChild(new GameHUD(this));

			startTime = Time.time;
		}

		private void Update()
		{
			TimeSurvived = Time.time - startTime;

			if (new Random(TimeSurvived).Next(0, game.currentFps * 15) > 10) { return; }

			int spawnInterval = TimeSurvived - lastLaserSpawnTime;
			if (spawnInterval < 3000) { return; }

			int nextLane = new Random().Next(0, DeploymentManager.LANES_COUNT);
			var laser = new LaserBeam("LaserSpritesheet.png", 9, 1, nextLane);
			laser.SetXY(game.width - laser.width, GetLaneCenter(nextLane) - (laser.height * 0.5f));
			laser.AddMotor();
			AddChild(laser);
			lastLaserSpawnTime = TimeSurvived;
			Debug.Log($"Spawned laser beam in lane {nextLane}.");
		}

		public float GetLaneBottom(int laneNumber)
		{
			float allLanes = game.height * 0.27375f;
			float laneSize = allLanes / 3.0f;
			float result = game.height - (laneSize * laneNumber);

			return result;
		}

		public float GetLaneCenter(int laneNumber)
		{
			float allLanes = game.height * 0.27375f;
			float laneSize = allLanes / 3.0f;
			float laneCenter = laneSize * 0.5f;
			float result = game.height - laneCenter - (laneSize * laneNumber);

			return result;
		}
	}
}