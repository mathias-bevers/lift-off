using System.IO;
using GXPEngine;
using GXPEngine.Core;

namespace Lavos
{
	public sealed class MyGame : Game
	{
		public readonly string scoreFilePath;
		private readonly Sound dieSound;

		public static MyGame Instance => main as MyGame;

		public MyGame() : base(1366, 800, false) //TODO: Check if the resolution is correct.
		{
			targetFps = 144;
			SetVSync(false);

			var sceneManager = new SceneManager();
			AddChild(sceneManager);
			sceneManager.LoadScene("main-menu");

			scoreFilePath = Directory.GetCurrentDirectory() + @"\assets\high-scores.txt";
			dieSound = new Sound(@"assets\sounds\die.wav");
		}

		private void Update()
		{
			if (Input.GetKey(Key.ESCAPE)) { Destroy(); }

			if (Input.GetKeyDown(Key.C)) { Collision.drawCollision = !Collision.drawCollision; }
		}

		private static void Main() { new MyGame().Start(); }

		public void PlayerDied()
		{
			dieSound.Play();
			var gameScene = SceneManager.Instance.GetActiveScene<GameScene>();

			if (!File.Exists(scoreFilePath))
			{
				using StreamWriter writer = File.CreateText(scoreFilePath);
				writer.WriteLine(gameScene.Score);
				writer.Close();
			}
			else
			{
				using StreamWriter writer = File.AppendText(scoreFilePath);
				writer.WriteLine(gameScene.Score);
				writer.Close();
			}

			SceneManager.Instance.LoadScene("game-over");
		}
	}
}