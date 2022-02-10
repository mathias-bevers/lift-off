using GXPEngine;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;
		//TODO: Test movement variables.
		private const float JUMP_FORCE = 14.0f; 
		private const float MOVEMENT_SPEED = 3.0f;

		public int LaneNumber { get; private set; } = 1;

		private bool isGrounded;
		private float currentLanePosition;
		private float velocity;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows)
		{
			SetCollider();
			x = game.width * 0.1f;
			currentLanePosition = MyGame.Instance.GetLaneCenter(LaneNumber) - height;
			y = currentLanePosition;
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

			if (y >= currentLanePosition)
			{
				y = currentLanePosition;
				velocity = 0;
				isGrounded = true;
			}
		}


		/// <summary>
		///     Checks if the player has pressed the "w" or "s", then switches the player's lane.
		/// </summary>
		private void ProcessVerticalInput()
		{
			if (Input.GetKeyDown(Key.W))
			{
				if (LaneNumber == 2) { return; }

				++LaneNumber;
				currentLanePosition = MyGame.Instance.GetLaneCenter(LaneNumber) - height;
				y = currentLanePosition;
			}

			if (!Input.GetKeyDown(Key.S)) { return; }

			if (LaneNumber == 0) { return; }

			--LaneNumber;
			currentLanePosition = MyGame.Instance.GetLaneCenter(LaneNumber) - height;
			y = currentLanePosition;
		}

		private void ProcessHorizontalInput()
		{
			if (Input.GetKey(Key.A)) { x -= MOVEMENT_SPEED; }

			if (Input.GetKey(Key.D)) { x += MOVEMENT_SPEED; }

			x = Mathf.Clamp(x, 0, game.width - width);
		}
	}
}