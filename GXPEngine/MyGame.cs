using System;
using System.Globalization;
using System.Linq.Expressions;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class MyGame : Game
	{
		public static MyGame Instance => main as MyGame;
		public Player Player { get; private set; }
		public float TimeSurvived { get; private set; }

		public MyGame() : base(800, 600, false)
		{
			var ground = new Sprite("square.png");
			ground.width = width;
			ground.SetXY(0, height - ground.height);
			ground.SetCollider();
			AddChild(ground);

			Player = new Player("triangle.png", 1, 1);
			AddChild(Player);

			AddChild(new ObstacleManager());
		}

		private void Update()
		{
			TimeSurvived = (float)Math.Round(Time.time / 1000.0f, 1);
		}

		private static void Main() { new MyGame().Start(); }
	}
}