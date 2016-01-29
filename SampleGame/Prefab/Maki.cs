using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground.Object.Component;

namespace SampleGame.Prefab
{
	public static class MakiState
	{
		public static int AimShot = 1;
		public static int Chase = 2;
	}

	public class Maki : Behavior
	{
		private double _seconds;
		private Yuzuki _yuzuki;
		private int _phase;
		private int _state;

		#region Properties
		public int Phase { get { return _phase; } set { _phase = value; } }
		public int State { get { return _state; } set { _state = value; } }
		#endregion

		public Maki(Yuzuki yuzuki)
		{
			_seconds = 0;
			_yuzuki = yuzuki;
			_phase = 1;
			_state = MakiState.AimShot;

			SpriteRenderer renderer = new SpriteRenderer("maki", new Vector2(0.5f));
			AddComponent(renderer);
		}

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			if(State > 0)
			{
				int test = State & MakiState.AimShot;
				if (test > 0) AimShot(gameTime);

				test = State & MakiState.Chase;
				if (test > 0) Chase(gameTime);
			}
		}

		private void AimShot(GameTime gameTime)
		{
			if (_seconds >= 2f / (2f * Phase))
			{
				Vector2 focus = _yuzuki.Transform.Position - this.Transform.Position;
				Bullet normalBullet = new NormalBullet(focus);
				normalBullet.Transform.Position = new Vector2(this.Transform.Position.X, this.Transform.Position.Y);
				World.Instantiate(normalBullet);

				_seconds = 0;
			}

			_seconds += gameTime.ElapsedGameTime.Milliseconds / 1000f;
		}

		private void Chase(GameTime gameTime)
		{
			Vector2 focus = _yuzuki.Transform.Position - this.Transform.Position;
			focus.Normalize();
			Vector2 velocity = focus * 100f;
			this.Transform.Velocity = velocity;
		}
	}
}
