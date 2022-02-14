using System;
using Mathias.Utilities;

namespace Lavos
{
	public class Pickup : Deployable
	{
		public Pickup(int laneNumber, float speed) : base(laneNumber, speed)
		{
			string fileName = "pickup-" + DeployableColor.ToString().ToLower() + ".png";
			SetupSprite(fileName);
		}

		protected override void OnPlayerCollision(Player player)
		{
			player.CurrentItemColor = DeployableColor;
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