using System;
using System.IO;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public sealed class MyGame : Game
	{
		public static MyGame Instance => main as MyGame;
		public Player Player { get; set; }

		public MyGame() : base(1366, 800, false) //TODO: Check if the resolution is correct.
		{
			targetFps = 144;
			SetVSync(false);

			var sceneManager = new SceneManager();
			AddChild(sceneManager);
			sceneManager.LoadScene("main-menu");
		}

		private void Update()
		{
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

		public void PlayerDied()
		{
			var gameScene = (GameScene)SceneManager.Instance.CurrentScene;

			string filePath = Directory.GetCurrentDirectory() + "\\high-scores.txt";

			if (!File.Exists(filePath))
			{
				using StreamWriter writer = File.CreateText(filePath);
				writer.WriteLine(gameScene.Score);
			}
			else
			{
				using StreamWriter writer = File.AppendText(filePath);
				writer.WriteLine(gameScene.Score);
			}


			Debug.Log($"Saved score to {filePath}.");

			SceneManager.Instance.LoadScene("game-over");
		}
	}
}