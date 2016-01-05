using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CatdogEngine.Playground.Object.Component;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CatdogEngine.Playground.Object {
	public class TestObject : Behavior {
		public override void Start() {
			AddComponent(new SpriteRenderer(new Sprite(World.CurrentScreen.Content.Load<Texture2D>("gojam"))));
		}

		public override void Update(GameTime gameTime) {
			Transform.Translate(new Vector2(1, 0));
			Debug.WriteLine(Transform.Position.X);
		}
	}
}
