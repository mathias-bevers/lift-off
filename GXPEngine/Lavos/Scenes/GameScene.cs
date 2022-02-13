using System.Drawing.Text;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class GameScene : Scene
	{
		public int TimeSurvived { get; private set; }
		public float Score => TimeSurvived / 1000.0f * (obstacleManager.DeployableSpeed - (obstacleManager.DeployableSpeed - 1.0f));

		public override string Name { get; protected set; } = "game";

		private int startTime;
		private ObstacleManager obstacleManager;
		private EasyDraw scoreText;

		public GameScene() { Start(); }

		public override void Start()
		{
			var ground = new Sprite("ground-temp.png");
			ground.SetXY(0, game.height - ground.height);
			ground.SetCollider();
			AddChild(ground);

			MyGame.Instance.Player = new Player("triangle.png", 1, 1);
			AddChild(MyGame.Instance.Player);

			obstacleManager = new ObstacleManager();
			AddChild(obstacleManager);

			scoreText = new EasyDraw(200, 50);
			scoreText.TextAlign(CenterMode.Min, CenterMode.Min);
			scoreText.SetXY(10, 10);
			AddChild(scoreText);

			startTime = Time.time;
		}

		private void Update()
		{
			TimeSurvived = Time.time - startTime;
			scoreText.Text($"Score: {Score:n2}", true);
		}
	}
}