using CatdogEngine.Graphics;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.Object.Component {
	public class SpriteRenderer : BehaviorComponent {
		private Camera _camera;
		private Sprite _sprite;

		private bool _enable;					// true : 그린다. false : 그리지 않는다.

		#region Properties
		public Camera Camera { get { return _camera; } set { _camera = value; } }
		public Sprite Sprite { get { return _sprite; } set { _sprite = value; } }
		public bool IsEnabled { get { return _enable; } set { _enable = value; } }
		#endregion

		public SpriteRenderer() {
			IsEnabled = true;
		}

		public SpriteRenderer(Sprite sprite) {
			IsEnabled = true;
			Sprite = sprite;
		}

		public override void Initialize(World world) {
			Camera = world.Camera;
		}

		public override void Update(GameTime gameTime) {
			// World상에서 카메라와의 상대적 위치로 버퍼에 그릴 위치를 계산.
			Vector2 distance = (Parent.Transform.Position - Camera.Transform.Position) * Camera.Zoom;
			int bufferWidth = ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth;
			int bufferHeight = ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight;

			Vector2 temp = new Vector2(distance.X + (bufferWidth / 2), distance.Y + (bufferHeight / 2));
			if (Sprite != null) {
				Sprite.Position = temp;
				Sprite.Scale = Parent.Transform.Scale * Camera.Zoom;
			}
		}

		public override void Draw(GameTime gameTime) {
			if(IsEnabled && Sprite != null) Sprite.Draw(ScreenManager.SpriteBatch);
		}
	}
}
