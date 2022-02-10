using GXPEngine;

namespace Lavos
{
	public class MainMenuScene : GameObject
	{
		public MainMenuScene() { Start(); }

		private void Start()
		{
			var startButton = new Button("White1x1.png", new Vector2(140, 40), "Start", new Vector2(613, 366));
			startButton.TextDraw.SetColor(0,0,0);
			startButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			game.AddChild(startButton);
			
			
			var quitButton = new Button("White1x1.png", new Vector2(140, 40), "Quit", new Vector2(613, 432));
			quitButton.TextDraw.SetColor(0, 0, 0);
			quitButton.OnClicked += game.Destroy;
			game.AddChild(quitButton);


		}
	}
}