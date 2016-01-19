using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CatdogEngine.Playground.Object {
	/// <summary>
	/// World에 존재하는 게임 오브젝트. 각 Behavior는 독립적으로 행동한다.
	/// 여러개의 BehaviorComponent를 가질 수 있다.
	/// </summary>
	public abstract class Behavior {
		private Transform _transform;

		private List<BehaviorComponent> _components;

		private World _world;								// 현재 속한 월드

		#region Properties
		public Transform Transform { get { return _transform; } set { _transform = value; } }
		public List<BehaviorComponent> Components { get { return _components; } }
		public World World { get { return _world; } set { _world = value; } }
		#endregion

		public Behavior() {
			Transform = new Transform();
			_components = new List<BehaviorComponent>();
		}

		/// <summary>
		/// 새로운 BehaviorComponent를 추가한다.
		/// </summary>
		public void AddComponent(BehaviorComponent component) {
			if (component != null) {
				component.Owner = this;
				_components.Add(component);
			}
		}

		/// <summary>
		/// 컴포넌트를 찾는다. 컴포넌트 리스트중에서 가장 먼저 검색되는 컴포넌트를 반환한다.
		/// 같은 컴포넌트가 여러개 중복되지 않는다고 가정했다. 추후에 수정이 필요할 것이다.
		/// </summary>
		/// <typeparam name="T">찾고자 하는 컴포넌트</typeparam>
		public BehaviorComponent GetComponent<T>() {
			foreach(BehaviorComponent component in Components) {
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
	}
}
