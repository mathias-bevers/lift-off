using GXPEngine;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;
		private const float JUMP_FORCE = 14.0f;
		private const float MOVEMENT_SPEED = 3.0f; //TODO: needs testing whether this is to fast.
		private bool isGrounded = false;
		private float velocity = 0.0f;

		private int laneNumber = 1;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows)
		{
			SetCollider();
			x = game.width * 0.1f;
		}

		//TODO: 3 lane movement.
		//TODO: jump.

		/// <summary>
		///     Update is called by the engine. It handles the player's input.
		/// </summary>
		private void Update() { ProcessInput(); }


		/// <summary>
		///     Checks if the player has released the "w" or "s", then switches the player's lane.
		/// </summary>
		private void ProcessInput()
		{
			if (Input.GetKeyUp(Key.W))
			{
				if (laneNumber == 2) { return; }

				++laneNumber;
			}

			if (Input.GetKeyUp(Key.S))
			{
				if (laneNumber == 0) { return; }

				--laneNumber;
			}

			y = game.height - 66.67f - 133.33f * laneNumber - height;
		}
	}
}