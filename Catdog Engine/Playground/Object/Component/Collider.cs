using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Playground.Object.Component {
	/// <summary>
	/// 이 컴포넌트를 포함하는 Behavior는 이 컴포넌트를 포함하는 다른 Behavior와 충돌한다.
	/// 충돌 시 Trigger 이벤트만 발생시킬지 실제 물리적 충돌을 일으킬 지 옵션을 선택할 수 있다.
	/// 충돌한 두 Collider가 모두 Trigger Collider가 아닐 경우에만 물리적 충돌이 발생한다.
	/// </summary>
	public abstract class Collider : BehaviorComponent {
		private bool _isTrigger;

		#region Properties
		public bool IsTrigger { get { return _isTrigger; } set { _isTrigger = value; } }
		#endregion

		public override void Initialize(World world) {
			
		}

		public override void Update(GameTime gameTime) {
			
		}

		public override void Draw(GameTime gameTime) {
			
		}
	}
}
