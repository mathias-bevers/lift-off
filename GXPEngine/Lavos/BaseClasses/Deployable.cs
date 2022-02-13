using System;
using GXPEngine;
using MBevers;

namespace Lavos
{
	public abstract class Deployable : Sprite
	{
		public event Action<Deployable> OnDestroyed;

		public enum Color { Red, Blue, Purple }

		public Color DeployableColor { get; }
		public int LaneNumber { get; }

		private readonly float speed;

		protected Deployable(string fileName, int laneNumber, float speed) : base(fileName)
		{
			DeployableColor = DeployableColor.Random();
			LaneNumber = laneNumber;
			this.speed = speed;

			SetXY(game.width, MyGame.Instance.GetLaneCenter(LaneNumber) - height);

			SetCollider();
			collider.isTrigger = true;
		}

		protected virtual void Update()
		{
			x -= speed;

			CheckCollisions();
		}

		private void CheckCollisions()
		{
			GameObject[] collisions = GetCollisions();

			if (collisions.Length == 0) { return; }

			foreach (GameObject collision in collisions)
			{
				if (collision is not Player player) { continue; }

				if (player.LaneNumber != LaneNumber) { continue; }

				OnPlayerCollision();
				return;
			}
		}

		protected abstract void OnPlayerCollision();

		protected override void OnDestroy()
		{
			OnDestroyed?.Invoke(this);
			base.OnDestroy();
		}
	}
}