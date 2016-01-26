using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground.Object.Component;

namespace SampleGame.Prefab {
	public class Maki : Behavior
	{
		private double _time;
		private Yuzuki _yuzuki;

		public Maki(Yuzuki yuzuki)
		{
			_time = 0;
			_yuzuki = yuzuki;
		}

		public override void Start()
		{
			Sprite sprite = new Sprite(World.CurrentScreen.Content.Load<Texture2D>("maki"));
			sprite.Scale = new Vector2(0.5f);
			SpriteRenderer renderer = new SpriteRenderer(sprite);

			AddComponent(renderer);
		}

		public override void Update(GameTime gameTime)
		{
			if(_time >= 1)
			{
				Vector2 focus = _yuzuki.Transform.Position - this.Transform.Position;
				Behavior normalBullet = new NormalBullet(focus);
				normalBullet.Transform.Position = new Vector2(this.Transform.Position.X, this.Transform.Position.Y);
				World.Instantiate(normalBullet);

				_time = 0;
			}

			_time += gameTime.ElapsedGameTime.Milliseconds / 1000f;
		}
	}
}
