using CatdogEngine.UI.StencilComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.ScreenSystem;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.Prefab
{
	public class HPMeter : Stencil
	{
		private Yuzuki _yuzuki;
		private Sprite _heart;

		public HPMeter(GameScreen screen, Yuzuki yuzuki) : base(screen)
		{
			_heart = new Sprite(CurrentScreen.Content.Load<Texture2D>("heart"));
			_yuzuki = yuzuki;
		}

		public override void Update(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			for(int i=0; i< _yuzuki.HP; ++i)
			{
				_heart.Position = new Vector2(this.Position.X + (_heart.Width * 1.5f * i), this.Position.Y);
				_heart.Draw(ScreenManager.SpriteBatch);
			}
		}

		public override void OnKeyDown(Keys key)
		{
			
		}

		public override void OnKeyUp(Keys key)
		{
			
		}

		public override void OnLeftMouseDown(int x, int y)
		{
			
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			
		}

		public override void OnMouseMove(int x, int y)
		{
			
		}
	}
}
