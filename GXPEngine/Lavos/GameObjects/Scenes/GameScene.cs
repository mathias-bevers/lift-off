using System;
using GXPEngine;

namespace Lavos
{
	public class GameScene : Scene
	{
		private readonly SoundChannel themeSC;

		public float Score => (TimeSurvived / 1000.0f) *
		                      (deploymentManager.DeployableSpeed - (deploymentManager.DeployableSpeed - 1.0f));

		public int TimeSurvived { get; private set; }
		public Player Player { get; private set; }
		public override string Name { get; protected set; } = "game";

		private DeploymentManager deploymentManager;
		private int startTime;
		private int lastLaserSpawnTime;

		public GameScene()
		{
			themeSC = new Sound(@"assets\sounds\theme-iteration7.wav", true).Play();
			Start();
		}

		public override void Start()
		{
			AddChild(new Sprite(@"assets\game-background.png"));

			var parallaxManager = new ParallaxManager();
			AddChild(parallaxManager);

			var foreground = new Sprite(@"assets\foreground.png");
			foreground.SetXY(0, game.height - foreground.height);
			foreground.SetCollider();
			AddChild(foreground);

			Player = new Player(@"assets\MainCharacterSpriteSheet.png", 8, 7);
			AddChild(Player);

			deploymentManager = new DeploymentManager();
			AddChild(deploymentManager);

			AddChild(new GameHUD(this));

			startTime = Time.time;
			themeSC.Volume = 0.1f;
		}

		private void Update()
		{
			TimeSurvived = Time.time - startTime;

			if (new Random(TimeSurvived).Next(0, game.currentFps * 15) > 10) { return; }

			int spawnInterval = TimeSurvived - lastLaserSpawnTime;
			if (spawnInterval < 3000) { return; }

			int nextLane = new Random().Next(0, DeploymentManager.LANES_COUNT);
			var laser = new LaserBeam(nextLane);
			laser.SetScaleXY(1.5f, 1);
			laser.SetXY(game.width - laser.width, GetLaneCenter(nextLane) - (laser.height * 0.5f));
			AddChild(laser);
			lastLaserSpawnTime = TimeSurvived;
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

		public override void OnOffload()
		{
			themeSC.Stop();
			base.OnOffload();
		}
	}
}