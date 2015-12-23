using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Graphics {
	public class Sprite {
		private Texture2D _texture;                 // 텍스쳐
		private Vector2 _position;					// 위치
		private Vector2 _scale;						// 스케일
		private float _rotation;                    // 회전각

		#region Properties
		public Vector2 Position {
			get { return _position; }
			set {
				if (value == null) _position = new Vector2(0, 0);
				else _position = value;
			}
		}
		public Vector2 Scale {
			get { return _scale; }
			set {
				if (value == null) _scale = new Vector2(1, 1);
				else _scale = value;
			}
		}
		public float Rotation { get { return _rotation; } set { _rotation = value; } }
		#endregion

		public Sprite(Texture2D texture) {
			_texture = texture;
			Position = new Vector2(0, 0);
			Scale = new Vector2(1, 1);
			Rotation = 0f;
		}

		public Sprite(Texture2D texture, Vector2 position) {
			_texture = texture;
			Position = position;
			Scale = new Vector2(1, 1);
			Rotation = 0f;
		}

		public Sprite(Texture2D texture, Vector2 position, Vector2 scale, float rotation = 0) {
			_texture = texture;
			Position = position;
			Scale = scale;
			Rotation = rotation;
		}

		/// <summary>
		/// <para>스프라이트를 그린다.</para>
		/// <para>반드시 SpriteBatch.Begin()과 SpriteBatch.End() 사이에 있어야한다.</para>
		/// <para>그러나 GameScreen의 Draw로직 앞뒤에 Begin과 End가 있으므로 특별한 경우가 아니면 신경쓰지 않아도 된다.</para>
		/// <para>회전 중심은 스프라이트의 중앙이다.</para>
		/// <para>스프라이트의 Position이 나타내는 위치는 texture의 좌측 하단이다.</para>
		/// </summary>
		/// <param name="spriteBatch">현재 그리기 작업중인 SpriteBatch</param>
		public void Draw(SpriteBatch spriteBatch) {
			// 회전 중심 계산
			Vector2 origin = new Vector2(Position.X + (Scale.X * (_texture.Width / 2)), Position.Y + (Scale.Y * (_texture.Height / 2)));

			// 그리기
			spriteBatch.Draw(texture: _texture,
				position: origin,
				rotation: Rotation,
				scale: Scale,
				origin: origin);
		}

		/// <summary>
		/// 리소스 해제
		/// </summary>
		public void Dispose() {
			_texture.Dispose();
		}
	}
}
