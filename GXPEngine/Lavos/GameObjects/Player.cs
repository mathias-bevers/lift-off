using System;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class Player : AnimationSprite
	{
		private const float GRAVITY = 0.3f;

		//TODO: Test movement variables.
		private const float JUMP_FORCE = 14.0f;
		private const float MOVEMENT_SPEED = 3.0f;
		private const int MAX_ABILITY_USE_TIME = 7500;
		private readonly Sound shieldSound;
		private readonly Sound slowMotionSound;

		public AbilityType? AbilityType { get; private set; }
		public bool IsUsingAbility { get; private set; }

		public float AbilityTimeLeft01
		{
			get
			{
				try { return (float)abilityUsageTimeLeft / MAX_ABILITY_USE_TIME; }
				catch (DivideByZeroException exception)
				{
					Debug.LogWaring(exception.Message);
					return 0;
				}
			}
		}

		public int LaneNumber { get; private set; }

		private bool isGrounded;
		private float currentLaneBottom;
		private float velocity;
		private int abilityUsageStartTime;
		private int abilityUsageTimeLeft;
		private SoundChannel abilitySC;

		public Player(string fileName, int columns, int rows) : base(fileName, columns, rows)
		{
			SetCollider();

			currentLaneBottom = 672;

			SetXY(game.width * 0.1f, currentLaneBottom);
			slowMotionSound = new Sound(@"sounds\enter slowmotion+wobly sound.wav");
			shieldSound = new Sound(@"sounds\the shield is active.wav");
		}

		private void Update()
		{
			//ProcessHorizontalInput();
			ProcessVerticalInput();
			ProcessAbilityUsage();

			velocity += GRAVITY;

			if (isGrounded && Input.GetKeyDown(Key.SPACE)) { velocity -= JUMP_FORCE; }

			isGrounded = false;

			y += velocity;

			if (y < currentLaneBottom) { return; }

			y = currentLaneBottom;
			velocity = 0;
			isGrounded = true;

			Animate(0.1f);
		}

		private void ProcessVerticalInput()
		{
			if (Input.GetKeyDown(Key.W))
			{
				if (LaneNumber == 2) { return; }

				++LaneNumber;
				currentLaneBottom = SceneManager.Instance.GetActiveScene<GameScene>().GetLaneBottom(LaneNumber) - height;
				y = currentLaneBottom;
			}

			if (!Input.GetKeyDown(Key.S)) { return; }

			if (LaneNumber == 0) { return; }

			--LaneNumber;
			currentLaneBottom = SceneManager.Instance.GetActiveScene<GameScene>().GetLaneBottom(LaneNumber) - height;
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
			if (IsUsingAbility)
			{
				abilityUsageTimeLeft -= Time.time - abilityUsageStartTime;

				if (abilityUsageTimeLeft <= 0)
				{
					abilityUsageTimeLeft = 0;
					IsUsingAbility = false;
					AbilityType = null;
					abilitySC?.Stop();
				}
			}

			if (!Input.GetKeyDown(Key.LEFT_SHIFT)) { return; }

			if (AbilityType == null) { return; }

			if (AbilityType == Lavos.AbilityType.SlowTime) { abilitySC = slowMotionSound.Play(); }

			abilityUsageStartTime = Time.time;
			IsUsingAbility = true;
		}

		public void PickedUpPickup(Pickup pickup)
		{
			AbilityType = pickup.AbilityType;
			abilityUsageTimeLeft = MAX_ABILITY_USE_TIME;
		}

		public override void Animate(float deltaFrameTime = 1)
		{
			if (!isGrounded) { SetCycle(0); }
			else { SetCycle(4, 12); }

			base.Animate(deltaFrameTime);
		}
	}
}