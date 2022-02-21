using GXPEngine;

namespace Lavos
{
	public class MainMenuScene : Scene
	{
		public override string Name { get; protected set; } = "main-menu";

		public MainMenuScene() { Start(); }

		public override void Start()
		{
			AddChild(new Sprite(@"assets\start-background.png") { width = game.width, height = game.height });

			var startButton = new Button(@"assets\StartGame.png", position: new Vector2(game.width * 0.5f, game.height * 0.6f));
			startButton.OnClicked += () => SceneManager.Instance.LoadScene("game");
			AddChild(startButton);


			var quitButton = new Button(@"assets\Quit.png", position: new Vector2(game.width * 0.5f, game.height * 0.75f));
			quitButton.OnClicked += game.Destroy;
			AddChild(quitButton);
		}
	}
}