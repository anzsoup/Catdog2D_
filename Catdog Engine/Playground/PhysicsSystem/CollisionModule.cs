using CatdogEngine.Playground.Object.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.PhysicsSystem {
	/// <summary>
	/// 충돌 검사 알고리즘.
	/// </summary>
	/// <returns>충돌했을 경우 충돌정보를 담은 Collision 객체 반환. 충돌하지 않았을 경우 null 반환.</returns>
	public delegate Collision PHYSICS__COLLISION_CHECK_ALGORITHM(Collider c1, Collider c2);

	/// <summary>
	/// 충돌한 두 물체가 모두 강체일 때(Rigidbody Component를 포함할 때), 충격량을 계산하고 적용한다.
	/// 각 모듈은 충격을 가한 후 반드시 두 강체는 겹치지 않음을 보장해야 하는 책임이 있다.
	/// </summary>
	public delegate void PHYSICS__APPLY_IMPULSE_ALGORITHM(Collision c);

	/// <summary>
	/// Collider의 종류에 따른 최적화된 알고리즘을 가지고 있는 모듈이다.
	/// Physics는 검사하고자 하는 두 Collider를 비교하여 적절한 CollisionModule을 선택하고 연산의 책임을 넘긴다.
	/// </summary>
	public abstract class CollisionModule {
		// 각 모듈은 자신만의 알고리즘으로 충돌검사를 수행한다.
		public abstract PHYSICS__COLLISION_CHECK_ALGORITHM COLLISION_CHECK { get; }
		public abstract PHYSICS__APPLY_IMPULSE_ALGORITHM APPLY_IMPULSE { get; }
	}
}
