using Mathias.Utilities;

namespace Lavos
{
	public class Pickup : Deployable
	{
		public Pickup(string fileName, int laneNumber, float speed) : base(fileName, laneNumber, speed) { }

		protected override void OnPlayerCollision()
		{
			Debug.LogError("NOT IMPLEMENTED");
		}

		protected override void Update()
		{
			if (x <= -width)
			{
				LateDestroy();
				return;
			}

			base.Update();
		}
	}
}