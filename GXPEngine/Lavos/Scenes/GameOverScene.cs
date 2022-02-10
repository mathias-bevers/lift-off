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
			var buttonSize = new Vector2(140, 40);

			var replayButton = new Button("White1x1.png", buttonSize, "Replay", new Vector2(271.5f, 366));
			replayButton.TextDraw.SetColor(1, 0, 1);
			replayButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			AddChild(replayButton);


			var quitButton = new Button("White1x1.png", buttonSize, "Quit", new Vector2(271.5f, 432));
			quitButton.TextDraw.SetColor(0, 1, 1);
			quitButton.OnClicked += game.Destroy;
			AddChild(quitButton);

			float[] scores = ReadScores();

			var playerScore = new EasyDraw(250, 40);
			playerScore.SetXY((game.width * 0.5f) - (playerScore.width * 0.5f), 25);
			playerScore.TextAlign(CenterMode.Center, CenterMode.Center);
			playerScore.TextSize(24);
			playerScore.Text($"You scored {scores.Last():n2}");
			AddChild(playerScore);

			Array.Sort(scores);
			Array.Reverse(scores); 

			var scoreTextSize = new Vector2(200, 40);
			float startY = (game.height * 0.5f) - (scoreTextSize.y * 0.5f); // Placed in the middle of the screen.
			startY -= scoreTextSize.y * 1.1f;

			for (var i = 0; i < (scores.Length < 3 ? scores.Length : 3); i++)
			{
				var highScore = new EasyDraw((int)scoreTextSize.x, (int)scoreTextSize.y);
				highScore.SetXY(game.width * 0.5f, startY + (highScore.height * 1.1f * i));
				highScore.TextAlign(CenterMode.Min, CenterMode.Center);
				highScore.Text($"{i+1}: {scores[i]:n2}");
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
	}
}