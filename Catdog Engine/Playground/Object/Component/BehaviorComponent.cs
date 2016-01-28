using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.Object.Component
{
	/// <summary>
	/// Behavior에 추가할 수 있는 요소.
	/// Behavior 안에 각 요소들이 모여 하나의 Game Object를 이룬다.
	/// </summary>
	public abstract class BehaviorComponent
	{
		private Behavior _owner;					// 현재 속해있는 Behavior

		#region Properties
		public Behavior Owner { get { return _owner; } set { _owner = value; } }
		#endregion

		/// <summary>
		/// Behavior가 Instantiate 될 때 필요한 초기화 작업을 여기서 한다.
		/// </summary>
		/// <param name="world">Behavior가 속한 World</param>
		public abstract void Initialize(World world);

		/// <summary>
		/// World의 Update 로직에서 호출된다.
		/// </summary>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// World의 Draw 로직에서 호출된다.
		/// </summary>
		public abstract void Draw(GameTime gameTime);
	}
}
