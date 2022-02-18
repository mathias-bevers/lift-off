using System;
using GXPEngine;

namespace Lavos
{
	public abstract class Deployable : GameObject
	{
		public event Action<Deployable> OnDestroyed;

		private readonly float speed;

		public int LaneNumber { get; }

		protected Player player;
		protected Sprite sprite;

		protected Deployable(int laneNumber, float speed)
		{
			LaneNumber = laneNumber;
			this.speed = speed * 150;
		}

		protected void SetupSprite(string fileName)
		{
			sprite = new Sprite(fileName);

			var gameScene = SceneManager.Instance.GetActiveScene<GameScene>();
			SetXY(game.width, gameScene.GetLaneBottom(LaneNumber) - sprite.height);
			player = gameScene.Player;

			sprite.SetCollider();
			sprite.collider.isTrigger = true;
			AddChild(sprite);
		}

		protected virtual void Update()
		{
			if (!player.IsUsingAbility || player.AbilityType != AbilityType.SlowTime) { x -= speed * Time.deltaTime; }
			else { x -= speed * 0.25f * Time.deltaTime; }


			CheckCollisions();
		}

		private void CheckCollisions()
		{
			GameObject[] collisions = sprite.GetCollisions();

			if (collisions.Length == 0) { return; }

			foreach (GameObject collision in collisions)
			{
				if (collision is not Player) { continue; }

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