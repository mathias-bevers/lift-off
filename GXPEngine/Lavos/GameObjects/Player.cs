using GXPEngine;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;

		//TODO: Test movement variables.
		private const float JUMP_FORCE = 14.0f;
		private const float MOVEMENT_SPEED = 3.0f;
		private const int MAX_ABILITY_USE_TIME = 3000;
		public bool isUsingAbility { get; private set; }
		public Deployable.Color? CurrentDeployableColor { get; private set; }

		public int LaneNumber { get; private set; } = 0;

		private bool isGrounded;
		private float currentLaneBottom;
		private float velocity;
		private int abilityUsageStartTime;
		private int abilityUsageTimeLeft;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows)
		{
			SetCollider();

			float allLanes = game.height * 0.27375f;
			float laneSize = allLanes / 3.0f;
			currentLaneBottom = game.height - laneSize;

			SetXY(game.width * 0.1f, currentLaneBottom);
		}

		private void Update()
		{
			ProcessVerticalInput();
			ProcessHorizontalInput();
			ProcessAbilityUsage();

			velocity += GRAVITY;

			if (isGrounded && Input.GetKeyDown(Key.SPACE)) { velocity -= JUMP_FORCE; }

			isGrounded = false;

			y += velocity;

			if (y < currentLaneBottom) { return; }

			y = currentLaneBottom;
			velocity = 0;
			isGrounded = true;
		}

		private void ProcessVerticalInput()
		{
			if (Input.GetKeyDown(Key.W))
			{
				if (LaneNumber == 2) { return; }

				++LaneNumber;
				currentLaneBottom = SceneManager.Instance.GetCurrentScene<GameScene>().GetLaneBottom(LaneNumber) - height;
				y = currentLaneBottom;
			}

			if (!Input.GetKeyDown(Key.S)) { return; }

			if (LaneNumber == 0) { return; }

			--LaneNumber;
			currentLaneBottom = SceneManager.Instance.GetCurrentScene<GameScene>().GetLaneBottom(LaneNumber) - height;
			y = currentLaneBottom;
		}

		private void ProcessHorizontalInput()
		{
			if (Input.GetKey(Key.A)) { x -= MOVEMENT_SPEED; }

			if (Input.GetKey(Key.D)) { x += MOVEMENT_SPEED; }

			x = Mathf.Clamp(x, 0, game.width - width);
		}

		private void ProcessAbilityUsage()
		{
			if (isUsingAbility)
			{
				abilityUsageTimeLeft -= Time.time - abilityUsageStartTime;

				if (abilityUsageTimeLeft <= 0)
				{
					isUsingAbility = false;
					CurrentDeployableColor = null;
				}
			}

			if (Input.GetKeyDown(Key.LEFT_SHIFT))
			{
				if (CurrentDeployableColor == null) { return; }

				abilityUsageStartTime = Time.time;
				isUsingAbility = true;
			}

			if (Input.GetKeyUp(Key.LEFT_SHIFT)) { isUsingAbility = false; }
		}

		public void PickedUpPickup(Pickup pickup)
		{
			CurrentDeployableColor = pickup.DeployableColor;
			abilityUsageTimeLeft = MAX_ABILITY_USE_TIME;
		}
	}
}