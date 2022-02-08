using System;
using GXPEngine;

namespace Lavos
{
	public sealed class MyGame : Game
	{
		public float TimeSurvived { get; private set; }
		public static MyGame Instance => main as MyGame;
		public Player Player { get; }

		public MyGame() : base(1366, 800, false) //TODO: check if the resolution is correct.
		{
			var ground = new Sprite("ground-temp.png");
			ground.SetXY(0, height - ground.height);
			ground.SetCollider();
			AddChild(ground);

			Player = new Player("triangle.png", 1, 1);
			AddChild(Player);

			AddChild(new ObstacleManager());
		}

		private void Update() { TimeSurvived = (float)Math.Round(Time.time / 1000.0f, 1); }

		private static void Main() { new MyGame().Start(); }
	}
}