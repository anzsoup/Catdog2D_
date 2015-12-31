using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.Object {
	/// <summary>
	/// World에 존재하는 게임 오브젝트.
	/// 각 Behavior는 독립적으로 행동한다.
	/// </summary>
	public abstract class Behavior {
		private GameTime _updateTime;
		private GameTime _drawTime;

		private Transform _transform;

		#region Properties
		public GameTime UpdateTime { get { return _updateTime; } set { _updateTime = value; } }
		public GameTime DrawTime { get { return _drawTime; } set { _drawTime = value; } }
		public Transform Transform { get { return _transform; } private set { _transform = value; } }
		#endregion

		/// <summary>
		/// Behavior가 처음 World에 생성되었을 때 한 번 호출된다.
		/// </summary>
		public abstract void Start();

		/// <summary>
		/// World가 Update 될 때마다 매번 호출된다. 여기서 Behavior의 행동을 진행시킨다.
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Behavior가 자신을 화면에 그려야할 때 호출된다. 그리기 작업을 여기서 한다.
		/// </summary>
		public abstract void Draw();
	}
}
