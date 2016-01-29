using Microsoft.Xna.Framework;
using CatdogEngine.Playground.Object.Component;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CatdogEngine.Playground.Object
{
	public class TestObject : Behavior
	{
		public TestObject()
		{
			SpriteRenderer renderer = new SpriteRenderer("gojam");
			AddComponent(renderer);

			Location location = new Location(300f, 300f);
			AddComponent(location);
		}

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			
		}
	}
}
