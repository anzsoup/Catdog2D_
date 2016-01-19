using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System;

namespace CatdogEngine.Playground {
	/// <summary>
	/// Farseer Physics Engine 기반 2D 물리 효과를 지원해 주는 월드.
	/// 전용 BehaviorComponent들을 사용할 수 있다.
	/// </summary>
	public class FarseerPhysicsWorld : World {
		private FarseerPhysics.Dynamics.World _world;

		#region Properties
		public FarseerPhysics.Dynamics.World World { get { return _world; } set { _world = value; } }
		public Vector2 Gravity { get { return World.Gravity; } set { World.Gravity = value; } }
		#endregion

		public FarseerPhysicsWorld(GameScreen currentScreen) : base(currentScreen) {
			if (World == null)
				World = new FarseerPhysics.Dynamics.World(new Vector2(0, -9.81f));
			else
				World.Clear();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			// variable time step but never less then 30 Hz
			World.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
		}
	}
}
