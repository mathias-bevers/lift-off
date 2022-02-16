using System.Linq;
using System.Management.Instrumentation;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class SceneManager : GameObject
	{
		private static SceneManager _instance;

		public Scene CurrentScene { get; private set; }

		public static SceneManager Instance
		{
			get
			{
				_instance ??= new SceneManager();

				return _instance;
			}
		}

		public SceneManager()
		{
			if (_instance != null)
			{
				Debug.LogWaring($"Tried to initialize a second instance of {GetType().FullName}. Destroying...");
				Destroy();
				return;
			}

			_instance = this;
		}

		public void LoadScene(string sceneName)
		{
			CurrentScene?.OnOffload();

			foreach (GameObject child in GetChildren()) { child.Destroy(); }

			switch (sceneName)
			{
				case "game":
					var gameScene = new GameScene();
					CurrentScene = gameScene;
					AddChild(gameScene);
					break;

				case "main-menu":
					var menuScene = new MainMenuScene();
					CurrentScene = menuScene;
					AddChild(menuScene);
					break;

				case "game-over":
					var gameOverScene = new GameOverScene();
					CurrentScene = gameOverScene;
					AddChild(gameOverScene);
					break;

				default:
					Debug.LogError($"\"{sceneName}\" is not a valid scene name.");
					game.Destroy();
					break;
			}
		}

		public T GetActiveScene<T>() where T : Scene
		{
			T[] scenesOfT = FindObjectsOfType<T>();

			switch (scenesOfT.Length)
			{
				case 0: throw new InstanceNotFoundException($"No instances of {typeof(T).Name} found.");
				case > 1:
					Debug.LogWaring($"There are {scenesOfT.Length} instances of {typeof(T).Name}");
					break;
			}

			return scenesOfT.First();
		}
	}
}