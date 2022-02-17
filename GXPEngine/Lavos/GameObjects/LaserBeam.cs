using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class LaserBeam : AnimationSprite
	{
		private const int DEADLY_FRAME = 4; //TODO change this value.

		private readonly int laneNumber;
		private readonly int lastFrame;
		private readonly Player player;

		private AnimationSprite motor;

		public LaserBeam(string filename, int columns, int rows, int laneNumber) : base(filename, columns, rows)
		{
			this.laneNumber = laneNumber;
			player = SceneManager.Instance.GetActiveScene<GameScene>().Player;

			lastFrame = (columns * rows) - 1;
			SetCycle(0, lastFrame + 1);
		}

		public void AddMotor()
		{
			motor = new AnimationSprite("enemyMotorcycleSpritesheet.png", 3, 1);
			AddChild(motor);
			motor.SetXY(width - motor.width, -motor.height*0.5f);
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

			if (!player.IsUsingAbility || player.AbilityType != AbilityType.Shield) { MyGame.Instance.PlayerDied(); }
		}
	}
}