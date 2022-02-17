using System;
using System.IO;
using GXPEngine;
using GXPEngine.Core;

namespace Lavos
{
	public sealed class MyGame : Game
	{
		public readonly string scoreFilePath;

		public static MyGame Instance => main as MyGame;

		public MyGame() : base(1366, 800, false) //TODO: Check if the resolution is correct.
		{
			targetFps = 144;
			SetVSync(false);

			var sceneManager = new SceneManager();
			AddChild(sceneManager);
			sceneManager.LoadScene("main-menu");

			scoreFilePath = Directory.GetCurrentDirectory() + "\\high-scores.txt";
		}

		private void Update()
		{
			if (Input.GetKey(Key.ESCAPE)) { Destroy(); }

			if (Input.GetKeyDown(Key.C)) { Collision.drawCollision = !Collision.drawCollision; }
		}

		private static void Main()
		{
			new MyGame().Start();

			Console.Write("Press enter to exit...");
			Console.ReadLine();
		}

		public void PlayerDied()
		{
			var stackFrame = new StackFrame(1);
			Mathias.Utilities.Debug.Log($"Killed by {stackFrame.GetMethod().DeclaringType?.Name}");

			var gameScene = SceneManager.Instance.GetActiveScene<GameScene>();

			if (!File.Exists(scoreFilePath))
			{
				using StreamWriter writer = File.CreateText(scoreFilePath);
				writer.WriteLine(gameScene.Score);
			}
			else
			{
				using StreamWriter writer = File.AppendText(scoreFilePath);
				writer.WriteLine(gameScene.Score);
			}

			SceneManager.Instance.LoadScene("game-over");
		}
	}
}