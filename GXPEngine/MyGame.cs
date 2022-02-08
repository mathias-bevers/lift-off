using GXPEngine;

namespace Lavos
{
	public class MyGame : Game
	{
		public static MyGame Instance => main as MyGame;
		public Player Player { get; }

		public MyGame() : base(800, 600, false)
		{
			var ground = new Sprite("square.png");
			ground.width = width;
			ground.SetXY(0, height - ground.height);
			ground.SetCollider();
			AddChild(ground);

			Player = new Player("triangle.png", 1, 1);
			AddChild(Player);
		}

		private void Update() { }

		private static void Main() { new MyGame().Start(); }
	}
}