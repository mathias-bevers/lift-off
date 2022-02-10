using GXPEngine;

namespace Lavos
{
	public class GameScene : GameObject
	{
		private ObstacleManager obstacleManager;

		public GameScene() { Start(); }

		public void Start()
		{
			var ground = new Sprite("ground-temp.png");
			ground.SetXY(0, game.height - ground.height);
			ground.SetCollider();
			AddChild(ground);

			MyGame.Instance.Player = new Player("triangle.png", 1, 1);
			AddChild(MyGame.Instance.Player);

			obstacleManager = new ObstacleManager();
			AddChild(obstacleManager);
		}
	}
}