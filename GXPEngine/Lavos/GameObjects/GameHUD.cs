using GXPEngine;

namespace Lavos
{
	public class GameHUD : GameObject
	{

		private EasyDraw scoreText;
		private EasyDraw abilityText;
		private GameScene gameScene;

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
		}

		private void Update()
		{
			scoreText.Text($"Score: {gameScene.Score:n2}", true);
			abilityText.Text($"Ability: {gameScene.Player.CurrentDeployableColor}", true);
		}
	}
}