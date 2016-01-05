using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground {
	/// <summary>
	/// World 상의 Behavior의 상태를 나타내는 클래스.
	/// 윈도우 좌표와는 분리 된 가상의 좌표계에 존재한다.
	/// </summary>
	public class Transform {
		private Vector2 _position;					// 위치
		private Vector2 _velocity;					// 속도
		private Vector2 _scale;						// 스케일
		private float _rotation;					// 회전각

		#region Properties
		public Vector2 Position { get { return _position; } set { _position = value; } }
		public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
		public Vector2 Scale { get { return _scale; } set { _scale = value; } }
		public float Rotation { get { return _rotation; } set { _rotation = value; } }
		#endregion

		public Transform() {
			Position = new Vector2(0, 0);
			Velocity = new Vector2(0, 0);
			Scale = new Vector2(1, 1);
			Rotation = 0f;
		}

		/// <summary>
		/// 주어진 변위만큼 이동한다.
		/// </summary>
		/// <param name="delta">이동할 변위</param>
		public void Translate(Vector2 delta) {
			Position += delta;
		}
	}
}
