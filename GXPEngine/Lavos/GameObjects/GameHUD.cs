using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class GameHUD : GameObject
	{
		private readonly EasyDraw abilityText;
		private readonly EasyDraw scoreText;
		private readonly GameScene gameScene;
		private readonly Sprite abilityBar;

		public GameHUD(GameScene gameScene)
		{
			this.gameScene = gameScene;

			scoreText = new EasyDraw(200, 50);
			scoreText.TextAlign(CenterMode.Min, CenterMode.Min);
			scoreText.SetXY(10, 10);
			AddChild(scoreText);

			abilityText = new EasyDraw(200, 50);
			abilityText.TextAlign(CenterMode.Max, CenterMode.Min);
			abilityText.SetXY(game.width - abilityText.width, 10);
			AddChild(abilityText);

			abilityBar = new Sprite("White1x1.png") {width = 200, height = 10};
			abilityBar.SetXY((game.width * 0.5f) - (abilityBar.width * 0.5f), 10);
			AddChild(abilityBar);
		}

		private void Update()
		{
			scoreText.Text($"Score: {gameScene.Score:n2}", true);
			abilityText.Text($"Ability: {gameScene.Player.AbilityColor}", true);
			abilityBar.SetScaleXY(gameScene.Player.AbilityTimeLeft01 * 200.0f, 10);
			Debug.Log(gameScene.Player.AbilityTimeLeft01.ToString());
		}
	}
}