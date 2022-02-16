using System;
using GXPEngine;

namespace Lavos
{
	public abstract class Deployable : GameObject
	{
		public event Action<Deployable> OnDestroyed;
		
		public int LaneNumber { get; }

		private readonly float speed;

		protected Sprite sprite;

		protected Deployable(int laneNumber, float speed)
		{
			LaneNumber = laneNumber;
			this.speed = speed;
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