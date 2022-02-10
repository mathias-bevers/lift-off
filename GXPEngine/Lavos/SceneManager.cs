using System;
using System.Linq;
using System.Reflection;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class SceneManager : GameObject
	{
		private static SceneManager _instance;

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

		public string CurrentSceneName = string.Empty;

		public void LoadScene(string sceneName)
		{
			foreach (GameObject child in MyGame.Instance.GetChildren())
			{
				if (child == this) { continue; }

				child.Destroy();
			}

			switch (sceneName)
			{
				case "game":
					game.AddChild(new GameScene());
					CurrentSceneName = sceneName;
					break;
				case "main-menu":
					game.Add(new MainMenuScene());
					CurrentSceneName = sceneName;
					break;
				default:
					Debug.LogError($"\"{sceneName}\" is not a valid scene name.");
					game.Destroy();
					break;
			}
		}
	}
}