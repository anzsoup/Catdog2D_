using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.Object {
	/// <summary>
	/// World에 존재하는 게임 오브젝트. 각 Behavior는 독립적으로 행동한다.
	/// 여러개의 BehaviorComponent를 가질 수 있다.
	/// </summary>
	public abstract class Behavior {
		private Transform _transform;

		private List<BehaviorComponent> _components;

		#region Properties
		public Transform Transform { get { return _transform; } private set { _transform = value; } }
		public List<BehaviorComponent> Components { get { return _components; } }
		#endregion

		public Behavior() {
			Transform = new Transform();
			_components = new List<BehaviorComponent>();
		}

		/// <summary>
		/// 새로운 BehaviorComponent를 추가한다.
		/// </summary>
		protected void AddComponent(BehaviorComponent component) {
			_components.Add(component);
		}

		/// <summary>
		/// Behavior가 처음 World에 생성되었을 때 한 번 호출된다.
		/// </summary>
		public abstract void Start();

		/// <summary>
		/// World가 Update 될 때마다 매번 호출된다. 여기서 Behavior의 행동을 진행시킨다.
		/// </summary>
		public abstract void Update(GameTime gameTime);
	}
}
