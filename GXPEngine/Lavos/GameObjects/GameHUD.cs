using GXPEngine;

namespace Lavos
{
	public class GameHUD : GameObject
	{
		private readonly EasyDraw chipsText;
		private readonly EasyDraw abilityText;
		private readonly EasyDraw scoreText;
		private readonly GameScene gameScene;
		private readonly Sprite abilityBar;

		public GameHUD(GameScene gameScene)
		{
			this.gameScene = gameScene;

			chipsText = new EasyDraw(200, 50);
			chipsText.TextAlign(CenterMode.Min, CenterMode.Min);
			chipsText.SetXY(10, 10);
			AddChild(chipsText);

			var scoreBG = new Sprite(@"assets\scoreUI.png") { width = 200, height = 50 };
			scoreBG.SetXY((game.width * 0.5f) - (scoreBG.width * 0.5f), 10);
			AddChild(scoreBG);

			scoreText = new EasyDraw(190, 50);
			scoreText.TextAlign(CenterMode.Center, CenterMode.Center);
			scoreText.SetXY((game.width * 0.5f) - (scoreText.width * 0.5f), 10);
			AddChild(scoreText);

			abilityText = new EasyDraw(200, 30);
			abilityText.TextAlign(CenterMode.Max, CenterMode.Min);
			abilityText.SetXY(game.width - abilityText.width - 10, 10);
			AddChild(abilityText);

			abilityBar = new Sprite(@"assets\White1x1.png") { width = 200, height = 25 };
			abilityBar.SetXY(game.width - abilityBar.width - 10, 10 + abilityText.height + 10);
			AddChild(abilityBar);

			UpdateUI();
		}

		private void Update()
		{
			UpdateUI();
		}

		private void UpdateUI()
		{
			chipsText.Text($"Chips: {gameScene.Player.CollectedChips}", true);
			scoreText.Text($"Score: {gameScene.Score:n2}", true);
			abilityText.Text($"Ability: {gameScene.Player.AbilityType}", true);
			abilityBar.SetScaleXY(gameScene.Player.AbilityTimeLeft01 * 200.0f, 10);
		}
	}
}