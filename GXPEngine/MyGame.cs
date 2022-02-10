using System;
using GXPEngine;

namespace Lavos
{
	public sealed class MyGame : Game
	{
		public float TimeSurvived { get; private set; }
		public static MyGame Instance => main as MyGame;
		public Player Player { get; set; }

		public MyGame() : base(1366, 800, false) //TODO: check if the resolution is correct.
		{
			targetFps = 144;
			SetVSync(false);

			var sceneManager = new SceneManager();
			AddChild(sceneManager);
			sceneManager.LoadScene("main-menu");
		}

		private void Update()
		{
			TimeSurvived = (float)Math.Round(Time.time / 1000.0f, 1);

			if (Input.GetKey(Key.ESCAPE)) { Destroy(); }
		}

		private static void Main()
		{
			new MyGame().Start();

			Console.Write("Press enter to exit...");
			Console.ReadLine();
		}

		public float GetLaneCenter(int laneNumber)
		{
			float halfScreen = height * 0.5f;
			float laneSize = halfScreen / 3.0f;
			float laneCenter = laneSize * 0.5f;

			return game.height - laneCenter - (laneSize * laneNumber);
		}
	}
}