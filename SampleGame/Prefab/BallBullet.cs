using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CatdogEngine.Playground.Object.Component;

namespace SampleGame.Prefab
{
	public class BallBullet : Bullet
	{
		public BallBullet(Vector2 focus)
		{
			Focus = new Vector2(focus.X, focus.Y);
			Focus.Normalize();
			Speed = 400f;

			Location location = new Location(16f, 16f);
			AddComponent(location);

			SpriteRenderer renderer = new SpriteRenderer("ballbullet");
			AddComponent(renderer);

			Transform.Velocity = new Vector2(0, Speed);
			Transform.Up = Focus;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if(Transform.Position.X > 400 - 16 || Transform.Position.X < -400)
			{
				Transform.Up = new Vector2(-Transform.Up.X, Transform.Up.Y);
			}

			if(Transform.Position.Y > 240 || Transform.Position.Y < -240 + 16)
			{
				Transform.Up = new Vector2(Transform.Up.X, -Transform.Up.Y);
			}
		}
	}
}
