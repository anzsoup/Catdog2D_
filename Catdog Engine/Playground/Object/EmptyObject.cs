using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Playground.Object
{
	public delegate void EMPTYOBJECT__BEHAVIOR_START();
	public delegate void EMPTYOBJECT__BEHAVIOR_UPDATE(GameTime gameTime);

	/// <summary>
	/// 클래스를 따로 정의하지 않고 일회용으로 게임 오브젝트를 만들 수 있는 빈 오브젝트.
	/// </summary>
	public class EmptyObject : Behavior
	{
		private EMPTYOBJECT__BEHAVIOR_START _start;
		private EMPTYOBJECT__BEHAVIOR_UPDATE _update;

		#region Properties
		public EMPTYOBJECT__BEHAVIOR_START START { get { return _start; } set { _start = value; } }
		public EMPTYOBJECT__BEHAVIOR_UPDATE UPDATE { get { return _update; } set { _update = value; } }
		#endregion

		public override void Start()
		{
			if (START != null) START();
		}

		public override void Update(GameTime gameTime)
		{
			if (UPDATE != null) UPDATE(gameTime);
		}
	}
}
