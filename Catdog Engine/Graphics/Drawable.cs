using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Graphics
{
	/// <summary>
	/// 윈도우의 렌더링 과정에 포함되는 그래픽 요소.
	/// </summary>
	public abstract class Drawable
	{
		private Vector2 _position;              // 버퍼에 그려지는 위치. 좌측 상단.

		#region Properties
		public Vector2 Position { get { return _position; } set { _position = value; } }
		#endregion

		public Drawable()
		{
			_position = new Vector2(0, 0);
		}

		/// <summary>
		/// 그리기 작업을 여기서 한다.
		/// </summary>
		/// <param name="spriteBatch"></param>
		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
