using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleGame.Prefab
{
	public class FastBullet : Bullet
	{
		public FastBullet(Vector2 focus)
		{
			Focus = new Vector2(focus.X, focus.Y);
			Focus.Normalize();
			Speed = 800f;

			Location location = new Location(36f, 39.5f);
			location.RelativePosition = new Vector2(18f, -19.75f);
			AddComponent(location);

			SpriteRenderer renderer = new SpriteRenderer("hasami2", new Vector2(0.5f));
			AddComponent(renderer);

			Transform.Velocity = new Vector2(0, Speed);
			Transform.Up = Focus;
		}

		public override void Start()
		{

		}

		public override void Update(GameTime gameTime)
		{
			if (Transform.Position.X > 600 || Transform.Position.X < -600)
			{
				Destroy();
			}

			if (Transform.Position.Y > 440 || Transform.Position.Y < -440)
			{
				Destroy();
			}
		}
	}
}
