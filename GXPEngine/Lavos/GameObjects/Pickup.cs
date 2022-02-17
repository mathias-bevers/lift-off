using GXPEngine;
using MBevers;

namespace Lavos
{
	public class Pickup : Deployable
	{
		public const int LANE_SIZE = 73;

		public AbilityType AbilityType { get; }

		public Pickup(int laneNumber, float speed) : base(laneNumber, speed)
		{
			AbilityType = AbilityType.Random(Time.time);

			string fileName = "pickup-" + AbilityType.ToString().ToLower() + ".png";
			SetupSprite(fileName);
		}

		protected override void OnPlayerCollision()
		{
			new Sound(@"sounds\pickup powerup.wav").Play();

			if (AbilityType != AbilityType.Chip) { player.PickedUpPickup(this); }

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