using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class GameOverScene : Scene
	{
		public override string Name { get; protected set; } = "game-over";

		public GameOverScene() { Start(); }

		public override void Start()
		{
			AddChild(new Sprite("start-background.png"));

			var startButton = new Button("Replay.png", position: new Vector2(game.width * 0.5f, game.height * 0.6f));
			startButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			AddChild(startButton);


			var quitButton = new Button("Quit.png", position: new Vector2(game.width * 0.5f, game.height * 0.75f));
			quitButton.OnClicked += game.Destroy;
			AddChild(quitButton);

			float[] scores = ReadScores();

			var playerScore = new EasyDraw(250, 40);
			playerScore.SetXY(800, 125);
			playerScore.TextAlign(CenterMode.Center, CenterMode.Center);
			playerScore.TextSize(24);
			playerScore.Text($"You scored {scores.Last():n2}");
			AddChild(playerScore);

			Array.Sort(scores);
			Array.Reverse(scores);


			var highScoresText = new EasyDraw(200, 40);
			highScoresText.SetXY(965, 375);
			highScoresText.Text("HIGH SCORES: ");
			highScoresText.TextSize(24);
			AddChild(highScoresText);

			var scoreTextSize = new Vector2(200, 40);
			const float startY = 425;

			for (var i = 0; i < (scores.Length < 3 ? scores.Length : 3); i++)
			{
				var highScore = new EasyDraw((int)scoreTextSize.x, (int)scoreTextSize.y);
				highScore.SetXY(965, startY + (highScore.height * 1.1f * i));
				highScore.TextAlign(CenterMode.Min, CenterMode.Center);
				highScore.Text($"{i + 1}: {scores[i]:n2}");
				AddChild(highScore);
			}
		}

		private float[] ReadScores()
		{
			var scores = new List<float>();

			foreach (string line in File.ReadAllLines(MyGame.Instance.scoreFilePath))
			{
				try { scores.Add(float.Parse(line)); }
				catch (Exception) { Debug.LogError($"\"{line}\" cannot be parsed to an float."); }
			}

			return scores.ToArray();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(2)) { Debug.Log($"Mouse x:{Input.mouseX}, y:{Input.mouseY}"); }
		}
	}
}