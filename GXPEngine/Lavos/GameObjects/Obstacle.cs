namespace Lavos
{
	public class Obstacle : Deployable
	{
		public Obstacle(int laneNumber, float speed) : base(laneNumber, speed)
		{
			string fileName = "obstacle-" + DeployableColor.ToString().ToLower() + ".png";
			SetupSprite(fileName);
		}

		protected override void OnPlayerCollision(Player player)
		{
			if (!player.isUsingAbility || player.CurrentDeployableColor != DeployableColor) { MyGame.Instance.PlayerDied(); }

			Destroy();
		}

		protected override void Update()
		{
			if (x <= -sprite.width)
			{
				LateDestroy();
				return;
			}

			base.Update();
		}
	}
}