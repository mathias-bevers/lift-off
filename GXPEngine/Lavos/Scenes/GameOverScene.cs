using GXPEngine;

namespace Lavos
{
	public class GameOverScene : Scene
	{
		public GameOverScene()
		{
			Start();
		}

		public override string Name { get; protected set; } = "game-over";

		public override void Start()
		{
			//TODO: add score text.
			//TODO: Display three highest scores.

			var replayButton = new Button("White1x1.png", new Vector2(140, 40), "Replay", new Vector2(613, 366));
			replayButton.TextDraw.SetColor(1, 0, 1);
			replayButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			AddChild(replayButton);


			var quitButton = new Button("White1x1.png", new Vector2(140, 40), "Quit", new Vector2(613, 432));
			quitButton.TextDraw.SetColor(0, 1, 1);
			quitButton.OnClicked += game.Destroy;
			AddChild(quitButton);
		}
	}
}