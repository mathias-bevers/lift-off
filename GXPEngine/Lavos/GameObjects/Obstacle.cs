using System;
using System.IO;
using System.Linq;
using GXPEngine;
using Mathias.Utilities;

namespace Lavos
{
	public class Obstacle : Deployable
	{
		private static string _folderPath;
		private static string[] _imagePaths;

		public Obstacle(int laneNumber, float speed) : base(laneNumber, speed)
		{
			_folderPath ??= Directory.GetCurrentDirectory() + @"\obstacles";
			_imagePaths ??= Directory.GetFiles(_folderPath, "*.png");

			string fileName = @"obstacles\" + _imagePaths[new Random(Time.deltaTimeMiliseconds).Next(0, _imagePaths.Length)].Split('\\').Last();
			SetupSprite(fileName);
		}

		protected override void OnPlayerCollision(Player player)
		{
			if (!player.IsUsingAbility || player.AbilityColor != AbilityType.Strength) { MyGame.Instance.PlayerDied(); }

			Destroy();
		}

		protected override void Update()
		{
			if (x <= -sprite.width)
			{
				LateDestroy();
				return;
			}

			base.Update();
		}
	}
}