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
	public class NormalBullet : Behavior {
		private Vector2 _focus;
		private SpriteRenderer _renderer;
		private float _speed;

		#region Properties
		public float Speed { set { _speed = value; } }
		#endregion

		public NormalBullet(Vector2 focus) {
			_focus = new Vector2(focus.X, focus.Y);
			_focus.Normalize();
			_speed = 400f;

			Transform.Velocity = new Vector2(0, _speed);
			Transform.Up = _focus;
		}

		public override void Start() {
			Sprite sprite = new Sprite(World.CurrentScreen.Content.Load<Texture2D>("hasami1"));
			sprite.Scale = new Vector2(0.5f);
			_renderer = new SpriteRenderer(sprite);

			AddComponent(_renderer);
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
