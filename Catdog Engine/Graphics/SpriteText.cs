using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Graphics {
	/// <summary>
	/// SpriteFont와 문자열로 구성되는 글자 요소.
	/// </summary>
	public class SpriteText : Drawable {
		private SpriteFont _font;                   // 글꼴
		private string _text;                       // 문자열
		private Color _color;						// 글자 색

		#region Properties
		public SpriteFont Font { get{ return _font; } set{ _font = value; } }
		public string Text { get { return _text; } set { _text = value; } }
		public Color Color { get { return _color; } set { _color = value; } }
		#endregion

		public SpriteText(SpriteFont font, String text = "Text") {
			Position = new Vector2(0, 0);
			Font = font;
			Text = text;
			Color = Color.Black;
		}

		/// <summary>
		/// 주어진 문자열이 버퍼에서 차지하는 영역을 구한다.
		/// </summary>
		/// <returns>영역의 Width와 Height를 각각 X, Y 성분으로 갖는 Vector2</returns>
		public Vector2 MeasureString(string text) {
			if (Font != null) return Font.MeasureString(text);
			else return Vector2.Zero;
		}

		/// <summary>
		/// 현재 버퍼에서 차지하고 있는 영역을 구한다.
		/// </summary>
		public Vector2 MeasureString() {
			if (Font != null && Text != null) return Font.MeasureString(Text);
			else return Vector2.Zero;
		}

		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.DrawString(Font, Text, Position, Color);
		}
	}
}
