using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Graphics2D {
	public class Sprite {
		private Texture2D _texture;					// 텍스쳐
		private Transform2D _transform;             // 위치, 스케일, 회전각

		#region Properties
		public Transform2D Transform { get { return _transform; } set { _transform = value; } }
		#endregion

		public Sprite(Texture2D texture) {
			_texture = texture;
			_transform = new Transform2D();
		}

		public Sprite(Texture2D texture, Transform2D transform) {
			_texture = texture;
			_transform = transform;
		}

		public Sprite(Texture2D texture, Vector2 position) {
			_texture = texture;
			_transform = new Transform2D();
			_transform.Position = position;
		}

		public Sprite(Texture2D texture, Vector2 position, Vector2 scale, float rotation = 0) {
			_texture = texture;
			_transform = new Transform2D();
			_transform.Position = position;
			_transform.Scale = scale;
			_transform.Rotation = rotation;
		}

		/// <summary>
		/// 스프라이트를 그린다.
		/// 반드시 SpriteBatch.Begin()과 SpriteBatch.End() 사이에 있어야한다.
		/// 회전 중심은 스프라이트의 중앙이다.
		/// 스프라이트의 Position이 나타내는 위치는 texture의 좌측 하단이다.
		/// </summary>
		/// <param name="spriteBatch">현재 그리기 작업중인 SpriteBatch</param>
		public void Draw(SpriteBatch spriteBatch) {
			// 회전 중심 계산
			Vector2 origin = new Vector2(Transform.Position.X + (Transform.Scale.X * (_texture.Width / 2)), 
				Transform.Position.Y + (Transform.Scale.Y * (_texture.Height / 2)));

			// 그리기
			spriteBatch.Draw(texture: _texture,
				position: origin,
				rotation: Transform.Rotation,
				scale: Transform.Scale,
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
