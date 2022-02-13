namespace Lavos
{
	public class Obstacle : Deployable
	{
		public Obstacle(string fileName, int laneNumber, float speed) : base(fileName, laneNumber, speed) { }

		protected override void OnPlayerCollision()
		{
			MyGame.Instance.PlayerDied();
			Destroy();
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