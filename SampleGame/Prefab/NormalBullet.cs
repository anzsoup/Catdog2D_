using Microsoft.Xna.Framework;
using CatdogEngine.Playground.Object.Component;

namespace SampleGame.Prefab
{
	public class NormalBullet : Bullet
	{
		public NormalBullet(Vector2 focus)
		{
			Focus = new Vector2(focus.X, focus.Y);
			Focus.Normalize();
			Speed = 400f;

			Location location = new Location(36f, 39.5f);
			location.RelativePosition = new Vector2(18f, -19.75f);
			AddComponent(location);

			SpriteRenderer renderer = new SpriteRenderer("hasami1", new Vector2(0.5f));
			AddComponent(renderer);

			Transform.Velocity = new Vector2(0, Speed);
			Transform.Up = Focus;
		}

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{ 
			if(Transform.Position.X > 600 || Transform.Position.X < -600)
			{
				Destroy();
			}

			if(Transform.Position.Y > 440 || Transform.Position.Y < -440)
			{
				Destroy();
			}
		}
	}
}
