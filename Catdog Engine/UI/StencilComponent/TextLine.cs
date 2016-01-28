using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.ScreenSystem;
using System.Diagnostics;
using CatdogEngine.Graphics;

namespace CatdogEngine.UI.StencilComponent
{
	/// <summary>
	/// 한 줄로 이루어진 UI 텍스트
	/// </summary>
	public class TextLine : Stencil
	{
		private SpriteText _spriteText;

		#region Properties
		public SpriteFont Font
		{
			get { return _spriteText.Font; }
			set
			{
				if (value != null) _spriteText.Font = value;
				else _spriteText.Font = Screen.Content.Load<SpriteFont>("DefaultText");

				// 텍스트가 차지하는 영역 다시 계산
				Vector2 textRegion = _spriteText.MeasureString();
				if (textRegion != Vector2.Zero)
					BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)textRegion.X, (int)textRegion.Y);
				else
					Debug.WriteLine("### TextLine : Something goes wrong while calculating text region. ###");
			}
		}
		public string Text
		{
			get { return _spriteText.Text; }
			set
			{
				if (value != null) _spriteText.Text = value;
				else _spriteText.Text = "Text";

				// 텍스트가 차지하는 영역 다시 계산
				Vector2 textRegion = _spriteText.MeasureString();
				if (textRegion != Vector2.Zero)
					BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)textRegion.X, (int)textRegion.Y);
				else
					Debug.WriteLine("### TextLine : Something goes wrong while calculating text region. ###");
			}
		}
		public Color Color { get { return _spriteText.Color; } set { _spriteText.Color = value; } }

		// Setter에서 SpriteText의 Position도 함께 변경하기 위해 Stencil의 Position Property를 가린다.
		public new Vector2 Position { get { return base.Position; } set { base.Position = value; _spriteText.Position = value; } }
		#endregion

		public TextLine(GameScreen screen, SpriteFont font, String text = "Text") : base(screen)
		{
			if (font == null) _spriteText = new SpriteText(Screen.Content.Load<SpriteFont>("DefaultText"));
			else _spriteText = new SpriteText(font);

			Text = text;

			Color = Color.Black;
		}

		public override void Update(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			_spriteText.Draw(ScreenManager.SpriteBatch);
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
