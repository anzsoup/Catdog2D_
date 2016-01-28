using Microsoft.Xna.Framework;
using CatdogEngine.Playground.Object.Component;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CatdogEngine.Playground.Object
{
	public class TestObject : Behavior
	{
		public override void Start()
		{
			AddComponent(new SpriteRenderer(new Sprite(World.CurrentScreen.Content.Load<Texture2D>("gojam"))));
			Location location = new Location(300f, 300f);
			AddComponent(location);
		}

		public override void Update(GameTime gameTime)
		{
			
		}
	}
}
