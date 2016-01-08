using CatdogEngine.Playground.Object.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.PhysicsSystem {
	/// <summary>
	/// 회전하지 않는 Box Collider간의 충돌처리를 담당하는 모듈.
	/// </summary>
	public class SimpleAABB : CollisionModule {
		#region Properties
		public override PHYSICS__APPLY_IMPULSE_ALGORITHM APPLY_IMPULSE {
			get {
				return ApplyImpulse;
			}
		}

		public override PHYSICS__COLLISION_CHECK_ALGORITHM COLLISION_CHECK {
			get {
				return CollisionCheck;
			}
		}
		#endregion

		private Collision CollisionCheck(Collider c1, Collider c2) {
			return null;
		}

		private void ApplyImpulse(Collision c) {

		}
	}
}
