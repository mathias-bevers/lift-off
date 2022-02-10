using GXPEngine;

namespace Lavos
{
	public class MainMenuScene : Scene
	{
		public override string Name { get; protected set; } = "main-menu";
		
		public MainMenuScene() { Start(); }

		public override void Start()
		{
			var startButton = new Button("White1x1.png", new Vector2(140, 40), "Start", new Vector2(613, 366));
			startButton.TextDraw.SetColor(1, 0, 1);
			startButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			AddChild(startButton);


			var quitButton = new Button("White1x1.png", new Vector2(140, 40), "Quit", new Vector2(613, 432));
			quitButton.TextDraw.SetColor(0, 1, 1);
			quitButton.OnClicked += game.Destroy;
			AddChild(quitButton);
		}
	}
}