using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework;

namespace SampleGame.Prefab {
	public class Bullet : Behavior
	{
		private Vector2 _focus;
		private float _speed;

		#region Properties
		public Vector2 Focus { get { return _focus; } set { _focus = value; } }
		public float Speed { get { return _speed; } set { _speed = value; } }
		#endregion

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			
		}
	}
}
