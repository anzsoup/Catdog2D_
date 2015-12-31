using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.Object.Component {
	/// <summary>
	/// Behavior에 추가할 수 있는 요소.
	/// Behavior 안에 각 요소들이 모여 하나의 Game Object를 이룬다.
	/// </summary>
	public interface BehaviorComponent {
		/// <summary>
		/// Behavior가 Instantiate 될 때 필요한 초기화 작업을 여기서 한다.
		/// </summary>
		/// <param name="world">Behavior가 속한 World</param>
		void Initialize(World world);

		/// <summary>
		/// World의 Update 로직에서 호출된다.
		/// </summary>
		void Update();

		/// <summary>
		/// World의 Draw 로직에서 호출된다.
		/// </summary>
		void Draw();
	}
}
