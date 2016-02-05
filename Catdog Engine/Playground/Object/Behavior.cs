using CatdogEngine.Playground.Object.Component;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace CatdogEngine.Playground.Object
{
	/// <summary>
	/// World에 존재하는 게임 오브젝트. 각 Behavior는 독립적으로 행동한다.
	/// 여러개의 BehaviorComponent를 가질 수 있다.
	/// </summary>
	public abstract class Behavior : InputListener
	{
		private Transform _transform;

		private List<BehaviorComponent> _components;

		private World _world;                               // 현재 속한 월드
		private GameScreen _currentScreen;                  // 현재 속한 스크린

		private bool _hasSeparateInputProcess;

		#region Properties
		public Transform Transform { get { return _transform; } set { _transform = value; } }
		public List<BehaviorComponent> Components { get { return _components; } }
		public World World { get { return _world; } set { _world = value; } }

		/// <summary>
		/// Input Listener 등록과 해제를 직접 관리하겠다고 World에게 알린다.
		/// 등록 후 적절한 해제가 이루어지지 않을 경우 의도하지 않은 동작이 일어날 수 있으니 주의해야한다.
		/// 특별한 상황이 아닐 경우에는 건드리지 않는 것이 좋다.
		/// </summary>
		public bool HasSeparateInputProcess { get { return _hasSeparateInputProcess; } set { _hasSeparateInputProcess = value; } }
		#endregion

		public Behavior()
		{
			Transform = new Transform();
			_components = new List<BehaviorComponent>();

			HasSeparateInputProcess = false;
		}

		/// <summary>
		/// 새로운 BehaviorComponent를 추가한다.
		/// </summary>
		public void AddComponent(BehaviorComponent component)
		{
			if (component != null)
			{
				component.Owner = this;
				_components.Add(component);
			}
		}

		/// <summary>
		/// 컴포넌트를 찾는다. 컴포넌트 리스트중에서 가장 먼저 검색되는 컴포넌트를 반환한다.
		/// 같은 컴포넌트가 여러개 중복되지 않는다고 가정했다. 추후에 수정이 필요할 것이다.
		/// </summary>
		/// <typeparam name="T">찾고자 하는 컴포넌트</typeparam>
		public BehaviorComponent GetComponent<T>()
		{
			foreach(BehaviorComponent component in Components)
			{
				if (component is T) return component;
			}

			return null;
		}

		/// <summary>
		/// Behavior가 처음 World에 생성되었을 때 한 번 호출된다.
		/// </summary>
		public abstract void Start();

		/// <summary>
		/// World가 Update 될 때마다 매번 호출된다. 여기서 Behavior의 행동을 진행시킨다.
		/// </summary>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// 자기 자신을 World에서 제거한다.
		/// </summary>
		public void Destroy()
		{
			World.Destroy(this);
		}

		/// <summary>
		/// 해당 Behavior를 World에서 제거한다.
		/// </summary>
		public void Destroy(Behavior behavior)
		{
			World.Destroy(behavior);
		}

		public virtual void OnCollisionEnter(Collision c) { }

		public virtual void OnCollisionExit(Collision c) { }

		/// <summary>
		/// 자신의 Location Component에 다른 Location이 접촉했을 때 발생하는 이벤트
		/// </summary>
		/// <param name="mine">본인의 로케이션</param>
		/// <param name="other">접촉한 외부 로케이션</param>
		public virtual void OnTriggerEnter(Location mine, Location other) { }

		/// <summary>
		/// 접촉했던 Location이 영역을 벗어나는 순간 발생하는 이벤트
		/// </summary>
		/// <param name="mine">본인의 로케이션</param>
		/// <param name="other">접촉한 외부 로케이션</param>
		public virtual void OnTriggerExit(Location mine, Location other) { }


		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Input Events
		// 필요한 함수를 재정의 하여 사용
		///////////////////////////////////////////////////////////////////////////////////////////////////////////

		public virtual void OnLeftMouseDown(int x, int y)
		{
			
		}

		public virtual void OnLeftMouseUp(int x, int y)
		{
			
		}

		public virtual void OnMouseMove(int x, int y)
		{
			
		}

		public virtual void OnKeyDown(Keys key)
		{
			
		}

		public virtual void OnKeyUp(Keys key)
		{
			
		}
	}
}
