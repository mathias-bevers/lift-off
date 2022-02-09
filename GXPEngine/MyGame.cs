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

		public float GetLaneCenter(int laneNumber)
		{
			float halfScreen = height * 0.5f;
			float laneSize = halfScreen / 3.0f;
			float laneCenter = laneSize * 0.5f;

			return game.height - laneCenter - (laneSize * laneNumber);
		}
	}
}