using GXPEngine;
using GXPEngine.Core;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;
		private const float JUMP_FORCE = 14.0f;
		private const float MOVEMENT_SPEED = 3.0f; //TODO: needs testing whether this is to fast.

		private bool isGrounded;
		private float velocity;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows) { SetCollider(); }

		private void Update()
		{
			Vector2 input = GetPlayerInput();

			velocity += GRAVITY;

			if (isGrounded && input.y > 0) { velocity -= JUMP_FORCE; }

			isGrounded = false;

			x += input.x * MOVEMENT_SPEED;

			Collision other = MoveUntilCollision(0, velocity);
			if (other == null) { return; }

			velocity = 0;

			if (other.normal.y < 0) { isGrounded = true; }
		}

		private static Vector2 GetPlayerInput()
		{
			var horizontal = 0.0f;
			var vertical = 0.0f;

			if (Input.GetKey(Key.W)) { vertical = 1; }

			if (Input.GetKey(Key.A)) { horizontal = -1; }

			if (Input.GetKey(Key.S)) { vertical = -1; }

			if (Input.GetKey(Key.D)) { horizontal = 1; }

			return new Vector2(horizontal, vertical).normalized;
		}
	}
}