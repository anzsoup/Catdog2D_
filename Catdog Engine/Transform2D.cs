using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine {
	/// <summary>
	/// 2차원 평면상의 수학적 정보(위치, 스케일, 회전각)를 표현하는 클래스
	/// 좌측 하단을 (0, 0)으로 정의한다.
	/// 좌표 시스템을 통일하기 위해 화면 좌표를 사용할 때는 반드시 Transform2D를 이용하길 권장한다.
	/// </summary>
	public class Transform2D {
		private Vector2 _position;					// 위치
		private Vector2 _scale;						// 스케일
		private float _rotation;                    // 회전각

		#region Properties
		/// <summary>
		/// 실제로 저장되는 값 : 윈도우 좌표
		/// 출력되는 값 : 윈도우 좌측 하단이 (0, 0)인 좌표계로의 사상
		/// </summary>
		public Vector2 Position {
			get {
				Vector2 virtualPosition = new Vector2(_position.X, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight - _position.Y);
				return virtualPosition;
			}

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

		public Transform2D() {
			Position = new Vector2(0, 0);
			Scale = new Vector2(1, 1);
			Rotation = 0f;
		}

		/// <summary>
		/// 주어진 벡터 만큼 Position을 이동한다.
		/// </summary>
		public void Translate(Vector2 delta) {
			_position.X += delta.X;
			_position.Y += delta.Y;
		}

		/// <summary>
		/// 주어진 벡터 만큼 Position을 이동한다.
		/// </summary>
		/// <param name="x">벡터의 x값</param>
		/// <param name="y">벡터의 y값</param>
		public void Translate(float x, float y) {
			_position.X += x;
			_position.Y += y;
		}
	}
}
