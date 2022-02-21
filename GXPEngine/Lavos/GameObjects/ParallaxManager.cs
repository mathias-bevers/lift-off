using System.IO;
using System.Linq;
using GXPEngine;

namespace Lavos
{
	public class ParallaxManager : GameObject
	{
		private readonly Sprite[] citySprites;
		private int scrollSpeed = 40;

		public ParallaxManager()
		{
			string folderPath = Directory.GetCurrentDirectory() + @"\assets\cities";
			string[] spriteNames = Directory.GetFiles(folderPath, "*.png")
				.Select(spritePath => spritePath.Split('\\').Last())
				.ToArray();

			citySprites = new Sprite[spriteNames.Length];
			for (var i = 0; i < spriteNames.Length; i++)
			{
				var citySprite = new Sprite($@"assets\cities\{spriteNames[i]}");
				citySprites[i] = citySprite;
				citySprite.SetXY(citySprite.width * i, 20);
				AddChild(citySprite);
			}
		}

		private void Update()
		{
			foreach (Sprite citySprite in citySprites)
			{
				citySprite.x -= scrollSpeed * Time.deltaTime;

				if (citySprite.x > -citySprite.width) { continue; }

				citySprite.x = (citySprite.width * citySprites.Length) - 1;
			}
		}
	}
}