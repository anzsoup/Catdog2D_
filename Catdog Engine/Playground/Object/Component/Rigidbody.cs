using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Playground.Object.Component {
	/// <summary>
	/// 이 컴포넌트를 포함하는 Behavior는 힘(Force)의 영향을 받는 강체가 된다.
	/// </summary>
	public class Rigidbody : BehaviorComponent {
		private bool _useGravity;                           // World의 중력의 영향을 받는가.
		private float _mass;                                // 질량

		private World _world;

		#region Properties
		public bool UseGravity { get { return _useGravity; } set { _useGravity = value; } }
		public float Mass { get { return _mass; } set { _mass = value; } }
		#endregion

		public Rigidbody() {
			UseGravity = true;
			Mass = 1f;
		}

		public override void Initialize(World world) {
			_world = world;
		}

		public override void Update(GameTime gameTime) {
			// 중력
			if(UseGravity) {
				// 중력 가속도
				Vector2 gravity = _world.Gravity;

				// 가속도는 초당 속도 변화량이므로
				Owner.Transform.Velocity += gravity * gameTime.ElapsedGameTime.Seconds;
			}
		}

		public override void Draw(GameTime gameTime) {
			
		}

		/// <summary>
		/// 힘을 가한다. Mass가 1 이하면 가속도 = 힘으로 계산된다.
		/// 힘은 항상 Impulse 하게 작용한다.
		/// </summary>
		public void AddForce(Vector2 force) {
			Vector2 accel;
			if (Mass > 1) accel = force / Mass;
			else accel = force;
			Owner.Transform.Velocity += accel;
		}
	}
}
