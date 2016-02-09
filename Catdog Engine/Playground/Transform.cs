using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground
{
	/// <summary>
	/// World 상의 Behavior의 상태를 나타내는 클래스.
	/// 윈도우 좌표와는 분리 된 가상의 좌표계에 존재한다.
	/// </summary>
	public class Transform
	{
		private Vector2 _position;					// 위치
		private Vector2 _velocity;					// 속도
		private Vector2 _scale;						// 스케일
		private float _rotation;                    // 회전각

		private Vector2 _unitX, _unitY;				// Behavior 좌표계의 단위벡터. 항상 서로 직각이다.

		#region Properties
		public Vector2 Position { get { return _position; } set { _position = value; } }
		public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
		public Vector2 Scale { get { return _scale; } set { _scale = value; } }
		public float Rotation { get { return _rotation; } set { _rotation = value; } }

		///////////////////////////////////////////////////////////////////////////////////
		// Behavior의 좌표축이 World의 좌표평면 안에서 어느쪽을 향하는지 지정한다.
		///////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Y 좌표축
		/// </summary>
		public Vector2 Up
		{
			get { return _unitY; }
			set
			{
				if(value != null)
				{
					if (value.LengthSquared() > 0)
					{
						_unitY = new Vector2(value.X, value.Y);
						_unitY.Normalize();

						double theta = -Math.PI / 2;
						double x = Math.Cos(theta) * _unitY.X - Math.Sin(theta) * _unitY.Y;
						double y = Math.Sin(theta) * _unitY.X + Math.Cos(theta) * _unitY.Y;
						_unitX = new Vector2((float)x, (float)y);
					}
				}
			}
		}

		/// <summary>
		/// X 좌표축
		/// </summary>
		public Vector2 Right
		{
			get { return _unitX; }
			set
			{
				if(value != null)
				{
					if (value.LengthSquared() > 0)
					{
						_unitX = new Vector2(value.X, value.Y);
						_unitX.Normalize();

						double theta = Math.PI / 2;
						double x = Math.Cos(theta) * _unitX.X - Math.Sin(theta) * _unitX.Y;
						double y = Math.Sin(theta) * _unitX.X + Math.Cos(theta) * _unitX.Y;
						_unitY = new Vector2((float)x, (float)y);
					}
				}
			}
		}
		#endregion

		public Transform()
		{
			Position = new Vector2(0, 0);
			Velocity = new Vector2(0, 0);
			Scale = new Vector2(1, 1);
			Rotation = 0f;

			_unitX = new Vector2(1, 0);
			_unitY = new Vector2(0, 1);
		}

		/// <summary>
		/// 주어진 변위만큼 이동한다.
		/// </summary>
		/// <param name="delta">이동할 변위</param>
		public void Translate(Vector2 delta)
		{
			Position += (_unitX * delta.X) + (_unitY * delta.Y);
		}
	}
}
