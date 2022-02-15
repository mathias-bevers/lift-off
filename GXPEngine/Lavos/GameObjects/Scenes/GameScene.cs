using System.Drawing.Text;
using GXPEngine;
using Mathias.Utilities;

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
			var ground = new Sprite("ground-temp.png");
			ground.SetXY(0, game.height - ground.height);
			ground.SetCollider();
			AddChild(ground);

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
	}
}