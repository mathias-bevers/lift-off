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
		private EasyDraw scoreText;
		private EasyDraw abilityText;

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

			scoreText = new EasyDraw(200, 50);
			scoreText.TextAlign(CenterMode.Min, CenterMode.Min);
			scoreText.SetXY(10, 10);
			AddChild(scoreText);

			abilityText = new EasyDraw(200, 50);
			abilityText.TextAlign(CenterMode.Max, CenterMode.Min);
			abilityText.SetXY(game.width - abilityText.width, 10);
			AddChild(abilityText);

			startTime = Time.time;
		}

		private void Update()
		{
			TimeSurvived = Time.time - startTime;

			scoreText.Text($"Score: {Score:n2}", true);
			abilityText.Text($"Ability: {Player.CurrentItemColor}", true);
		}
	}
}