using GXPEngine;

namespace Lavos
{
	public class GameScene : Scene
	{
		public Player Player { get; private set; }
		public int TimeSurvived { get; private set; }
		public float Score => TimeSurvived / 1000.0f * (deploymentManager.DeployableSpeed - (deploymentManager.DeployableSpeed - 1.0f));

		public override string Name { get; protected set; } = "game";

		private int startTime;
		private DeploymentManager deploymentManager;

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
		}

		public float GetLaneBottom(int laneNumber)
		{
			float allLanes = game.height * 0.27375f;
			float laneSize = allLanes / 3.0f;

			return game.height - (laneSize * laneNumber);
		}
	}
}