using System;
using GXPEngine;

namespace Lavos
{
	public class Button : GameObject
	{
		public event Action OnClicked;

		private readonly Sprite sprite;
		private readonly string text;

		public EasyDraw TextDraw { get; private set; }

		public Button(string fileName, string text = null, Vector2 position = default)
		{
			SetXY(position);

			sprite = new Sprite(fileName);
			sprite.SetCollider();

			this.text = text;

			Start();
		}

		public Button(string fileName, Vector2 size, string text = null, Vector2 position = default)
		{
			SetXY(position);

			sprite = new Sprite(fileName) { width = (int)size.x, height = (int)size.y };
			sprite.SetCollider();

			this.text = text;

			Start();
		}

		private void Start()
		{
			AddChild(sprite);

			if (text == null) { return; }

			TextDraw = new EasyDraw((int)(sprite.width * 0.9f), (int)(sprite.height * 0.9f));
			TextDraw.TextAlign(CenterMode.Center, CenterMode.Center);
			TextDraw.Text(text);
			AddChild(TextDraw);
		}

		private void Update()
		{
			if (sprite.HitTestPoint(Input.mouseX, Input.mouseY))
			{
				sprite.SetColor(1.0f, 1.0f, 1.0f);

				if (!Input.GetMouseButtonUp(0)) { return; }

				OnClicked?.Invoke();
			}
			else { sprite.SetColor(0.7f, 0.7f, 0.7f); }
		}
	}
}