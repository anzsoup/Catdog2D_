using CatdogEngine.Graphics;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CatdogEngine.Playground.Object.Component
{
	public class SpriteRenderer : BehaviorComponent
	{
		private Camera _camera;
		private Sprite _sprite;
		private string _initSpriteName;

		private bool _enable;                   // true : 그린다. false : 그리지 않는다.

		private Vector2 _initialSpriteScale;

		#region Properties
		public Camera Camera { get { return _camera; } set { _camera = value; } }
		public Sprite Sprite { get { return _sprite; } set { _sprite = value; } }
		public bool IsEnabled { get { return _enable; } set { _enable = value; } }
		#endregion

		public SpriteRenderer()
		{
			IsEnabled = true;
		}

		// Don't use it
		/*
		public SpriteRenderer(Sprite sprite)
		{
			IsEnabled = true;
			Sprite = sprite;
			_initialSpriteScale = Sprite.Scale;
		}
		*/

		public SpriteRenderer(string spriteName)
		{
			IsEnabled = true;
			_initSpriteName = spriteName;
			_initialSpriteScale = new Vector2(1, 1);
		}

		public SpriteRenderer(string spriteName, Vector2 scale)
		{
			IsEnabled = true;
			_initSpriteName = spriteName;
			_initialSpriteScale = scale;
		}

		public override void Initialize(World world)
		{
			Camera = world.Camera;

			if(_initSpriteName != null)
			{
				Sprite = new Sprite(world.CurrentScreen.Content.Load<Texture2D>(_initSpriteName));
				Sprite.Scale = _initialSpriteScale;
			}
		}

		public override void Update(GameTime gameTime)
		{
			// World상에서 카메라와의 상대적 위치로 버퍼에 그릴 위치를 계산.
			Vector2 distance = (Owner.Transform.Position - Camera.Transform.Position) * Camera.Zoom;
			int bufferWidth = ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth;
			int bufferHeight = ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight;

			Vector2 temp = new Vector2(distance.X + (bufferWidth / 2), bufferHeight - (distance.Y + (bufferHeight / 2)));
			if (Sprite != null)
			{
				Sprite.Position = temp;
				Sprite.Scale = _initialSpriteScale * Owner.Transform.Scale * Camera.Zoom;

				// Owner의 좌표계에 따라 스프라이트를 회전시킨다.
				double radian = Math.Acos(Owner.Transform.Up.Y);
				if(Owner.Transform.Up.X < 0)
				{
					radian = -radian;
				}
				Sprite.Rotation = (float)radian;
			}
		}

		public override void Draw(GameTime gameTime)
		{
			if(IsEnabled && Sprite != null) Sprite.Draw(ScreenManager.SpriteBatch);
		}
	}
}
