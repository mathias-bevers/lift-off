using System;
using GXPEngine;

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

			Player = new Player("triangle.png", 1, 1);
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

			int nextLane = new Random(spawnInterval).Next(0, DeploymentManager.LANES_COUNT);
			var laser = new LaserBeam("laser-beam.png", 1, 5, nextLane);
			laser.SetXY(0, GetLaneBottom(nextLane) - laser.height);
			AddChild(laser);

			lastLaserSpawnTime = TimeSurvived;
		}

		public float GetLaneBottom(int laneNumber)
		{
			float allLanes = game.height * 0.27375f;
			float laneSize = allLanes / 3.0f;

			return game.height - (laneSize * laneNumber);
		}
	}
}