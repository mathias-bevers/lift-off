using GXPEngine;
using MBevers;

namespace Lavos
{
	public class Obstacle : Sprite
	{
		public enum Color { Red, Blue, Purple }

		public Color ObstacleColor { get; }

		private readonly int laneNumber;

		public Obstacle(string fileName, int laneNumber) : base(fileName)
		{
			this.laneNumber = laneNumber;

			SetXY(game.width, MyGame.Instance.GetLaneCenter(laneNumber) - height);

			SetCollider();
			collider.isTrigger = true;

			ObstacleColor = ObstacleColor.Random();
		}

		private void Update()
		{
			GameObject[] collisions = GetCollisions();

			if (collisions.Length == 0) { return; }

			foreach (GameObject collision in collisions)
			{
				if (collision is not Player player) { continue; }

				if (player.LaneNumber != laneNumber) { continue; }

				game.Destroy(); //TODO: make game over scene.
				return;
			}
		}
	}
}