using System;
using GXPEngine;
using MBevers;

namespace Lavos
{
	public abstract class Deployable : GameObject
	{
		public event Action<Deployable> OnDestroyed;

		public enum Color { Red, Blue, Purple }

		public Color DeployableColor { get; }
		public int LaneNumber { get; }

		private readonly float speed;

		protected Sprite sprite;

		protected Deployable(int laneNumber, float speed) : base()
		{
			LaneNumber = laneNumber;
			this.speed = speed;
			
			DeployableColor = DeployableColor.Random(Time.time);
		}

		protected void SetupSprite(string fileName)
		{
			sprite = new Sprite(fileName);

			SetXY(game.width, SceneManager.Instance.GetCurrentScene<GameScene>().GetLaneBottom(LaneNumber) - sprite.height);

			sprite.SetCollider();
			sprite.collider.isTrigger = true;
			AddChild(sprite);
		}

		protected virtual void Update()
		{
			x -= speed;

			CheckCollisions();
		}

		private void CheckCollisions()
		{
			GameObject[] collisions = sprite.GetCollisions();

			if (collisions.Length == 0) { return; }

			foreach (GameObject collision in collisions)
			{
				if (collision is not Player player) { continue; }

				if (player.LaneNumber != LaneNumber) { continue; }

				OnPlayerCollision(player);
				return;
			}
		}

		protected abstract void OnPlayerCollision(Player player);

		protected override void OnDestroy()
		{
			OnDestroyed?.Invoke(this);
			base.OnDestroy();
		}
	}
}