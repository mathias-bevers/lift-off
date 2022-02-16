using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class LaserBeam : AnimationSprite
	{
		private const int DEADLY_FRAME = 2; //TODO change this value.

		private readonly int lastFrame;
		private readonly Player player;

		private readonly int laneNumber;

		public LaserBeam(string filename, int columns, int rows, int laneNumber) : base(filename, columns, rows)
		{
			this.laneNumber = laneNumber;
			player = SceneManager.Instance.GetActiveScene<GameScene>().Player;

			lastFrame = (columns * rows) -1;
			SetCycle(0, lastFrame + 1);
		}

		private void Update()
		{
			Animate(0.1f);

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