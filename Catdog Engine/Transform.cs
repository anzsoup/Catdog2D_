using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine {
	/// <summary>
	/// <para>World 상에 존재하는 객체의 2차원 상태 정보를 표현한다.</para>
	/// <para>화면에 실제로 보이는 위치를 알려면 Window 좌표로 변환해야한다.</para>
	/// <para><seealso cref="CatdogEngine.Graphics.Camera"/></para>
	/// </summary>
	public class Transform {
		private Vector2 _position;					// 위치
		private Vector2 _scale;						// 스케일
		private float _rotation;                    // 회전각

		#region Properties
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

		public Transform() {
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
