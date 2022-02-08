using GXPEngine;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;
		private const float JUMP_FORCE = 14.0f;
		private const float MOVEMENT_SPEED = 3.0f; //TODO: needs testing whether this is to fast.
		private float CurrentLanePosition => game.height - 66.67f - (133.33f * laneNumber) - height;

		private bool isGrounded;
		private float velocity;
		private int laneNumber = 1;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows)
		{
			SetCollider();
			x = game.width * 0.1f;
		}

		/// <summary>
		///     Update is called by the engine. It handles the player's input.
		/// </summary>
		private void Update()
		{
			ProcessVerticalInput();
			ProcessHorizontalInput();

			velocity += GRAVITY;

			if (isGrounded && Input.GetKeyDown(Key.SPACE)) { velocity -= JUMP_FORCE; }

			isGrounded = false;

			y += velocity;

			if (y >= CurrentLanePosition)
			{
				y = CurrentLanePosition;
				velocity = 0;
				isGrounded = true;
			}
		}


		/// <summary>
		///     Checks if the player has released the "w" or "s", then switches the player's lane.
		/// </summary>
		private void ProcessVerticalInput()
		{
			if (Input.GetKeyDown(Key.W))
			{
				if (laneNumber == 2) { return; }

				++laneNumber;
				y = CurrentLanePosition;
			}

			if (!Input.GetKeyDown(Key.S)) { return; }

			if (laneNumber == 0) { return; }

			--laneNumber;
			y = CurrentLanePosition;
		}

		private void ProcessHorizontalInput()
		{
			if (Input.GetKey(Key.A)) { x -= MOVEMENT_SPEED; }

			if (Input.GetKey(Key.D)) { x += MOVEMENT_SPEED; }
		}
	}
}