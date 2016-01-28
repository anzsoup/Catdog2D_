using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Playground.Object.Component
{

	public delegate void LOCATION__TRIGGER_EVENT(Location other);

	/// <summary>
	/// 트리거 이벤트를 발생시키는 충돌체
	/// </summary>
	public class Location : BehaviorComponent
	{
		private Vector2 _relativePosition;
		private string _label;

		private float _width, _height;

		private LOCATION__TRIGGER_EVENT _onTriggerEnter;
		private LOCATION__TRIGGER_EVENT _onTriggerExit;

		#region Properties
		/// <summary>
		/// Owner의 위치로부터의 상대적 위치.
		/// 좌측 상단.
		/// </summary>
		public Vector2 RelativePosition { get { return _relativePosition; } set { _relativePosition = value; } }

		/// <summary>
		/// 월드상의 실제 위치.
		/// 좌측 상단.
		/// </summary>
		public Vector2 WorldPosition { get { return Owner.Transform.Position + RelativePosition; } set { RelativePosition = value - Owner.Transform.Position; } }

		public float Width { get { return _width * Owner.Transform.Scale.X; } set { _width = value; } }
		public float Height { get { return _height * Owner.Transform.Scale.Y; } set { _height = value; } }

		/// <summary>
		/// 로케이션 분류를 위해 사용하는 레이블
		/// </summary>
		public string Label { get { return _label; } set { _label = value; } }

		/// <summary>
		/// OnTriggerEnter 이벤트와 동일
		/// </summary>
		public LOCATION__TRIGGER_EVENT ON_TRIGGER_ENTER { get { return _onTriggerEnter; } set { _onTriggerEnter = value; } }

		/// <summary>
		/// OnTriggerExit 이벤트와 동일
		/// </summary>
		public LOCATION__TRIGGER_EVENT ON_TRIGGER_EXIT { get { return _onTriggerExit; } set { _onTriggerExit = value; } }
		#endregion

		public Location()
		{
			RelativePosition = new Vector2(0, 0);
			Label = null;
			Width = 40f;
			Height = 40f;
		}

		public Location(float width, float height)
		{
			RelativePosition = new Vector2(0, 0);
			Label = null;
			Width = width;
			Height = height;
		}

		public override void Initialize(World world)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			
		}
	}
}
