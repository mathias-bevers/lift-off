using System.Drawing.Text;
using GXPEngine;

namespace Lavos
{
	public class GameScene : Scene
	{
		public int TimeSurvived { get; private set; }
		public float Score => TimeSurvived / 1000 * (obstacleManager.ObstacleSpeed - 1.5f);

		public override string Name { get; protected set; } = "game";

		private int startTime;
		private ObstacleManager obstacleManager;

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

			startTime = Time.time;
		}

		private void Update() { TimeSurvived = Time.time - startTime; }
	}
}