using CatdogEngine.Playground.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground.Object.Component;

namespace SampleGame.Prefab {
	public class NormalBullet : Bullet {
		public NormalBullet(Vector2 focus) {
			Focus = new Vector2(focus.X, focus.Y);
			Focus.Normalize();
			Speed = 400f;

			Transform.Velocity = new Vector2(0, Speed);
			Transform.Up = Focus;
		}

		public override void Start() {
			Sprite sprite = new Sprite(World.CurrentScreen.Content.Load<Texture2D>("hasami1"));
			sprite.Scale = new Vector2(0.5f);
			SpriteRenderer renderer = new SpriteRenderer(sprite);

			AddComponent(renderer);
		}

		public override void Update(GameTime gameTime) { 
			if(Transform.Position.X > 600 || Transform.Position.X < -600) {
				Destroy();
			}

			if(Transform.Position.Y > 440 || Transform.Position.Y < -440) {
				Destroy();
			}
		}
	}
}
