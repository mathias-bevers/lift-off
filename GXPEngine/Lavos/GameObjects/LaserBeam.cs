using GXPEngine;

namespace Lavos
{
	public class LaserBeam : AnimationSprite
	{
		private const int DEADLY_FRAME = 4;

		private readonly AnimationSprite motor;
		private readonly int laneNumber;
		private readonly int lastFrame;
		private readonly Player player;

		private readonly SoundChannel soundChannel;

		public LaserBeam(int laneNumber) : base(@"assets\LaserSpritesheet.png", 9, 1)
		{
			this.laneNumber = laneNumber;
			player = SceneManager.Instance.GetActiveScene<GameScene>().Player;

			lastFrame = (_cols * _rows) - 1;
			SetCycle(0, lastFrame + 1);

			motor = new AnimationSprite(@"assets\enemyMotorcycleSpritesheet.png", 3, 1);
			AddChild(motor);
			motor.SetXY(width - motor.width, -motor.height * 0.5f);

			soundChannel = new Sound(@"assets\sounds\laser fire.wav").Play();
		}

		private void Update()
		{
			Animate(0.05f);
			motor.Animate(0.05f);

			if (_currentFrame == lastFrame)
			{
				Destroy();
				return;
			}

			if (_currentFrame < DEADLY_FRAME) { return; }

			if (player.LaneNumber != laneNumber) { return; }

			if (player.IsUsingAbility && player.AbilityType == AbilityType.Shield) { return; }

			MyGame.Instance.PlayerDied();
		}

		protected override void OnDestroy()
		{
			soundChannel.Stop();
			base.OnDestroy();
		}
	}
}