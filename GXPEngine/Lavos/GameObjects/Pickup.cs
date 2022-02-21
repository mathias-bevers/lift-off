using GXPEngine;
using MBevers;

namespace Lavos
{
	public class Pickup : Deployable
	{
		public AbilityType AbilityType { get; }

		public Pickup(int laneNumber, float speed) : base(laneNumber, speed)
		{
			AbilityType = AbilityType.Random(Time.time);

			string fileName = @"assets\pickup-" + AbilityType.ToString().ToLower() + ".png";
			SetupSprite(fileName);
		}

		protected override void OnPlayerCollision()
		{
			new Sound(@"assets\sounds\pickup powerup.wav").Play();

			player.PickedUpPickup(this);

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