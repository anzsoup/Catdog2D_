using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground.Object.Component;
using System;

namespace SampleGame.Prefab
{
	public static class MakiState
	{
		public static int AimShot = 1;
		public static int Chase = 2;
		public static int BallBulletShot = 4;
		public static int FastShot = 8;
	}

	public class Maki : Behavior
	{
		private float _seconds;
		private Yuzuki _yuzuki;
		private int _phase;
		private int _state;

		private int _bulletCount;

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

			_bulletCount = 0;

			SpriteRenderer renderer = new SpriteRenderer("maki", new Vector2(0.5f));
			AddComponent(renderer);
		}

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			_seconds += gameTime.ElapsedGameTime.Milliseconds / 1000f;

			if (State > 0)
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
				Bullet bullet;
				int test = State & MakiState.BallBulletShot;
				if(test > 0)
				{
					if(_bulletCount == 4)
					{
						bullet = new BallBullet(focus);
						_bulletCount = 0;
					}
					else
					{
						bullet = new NormalBullet(focus);
						_bulletCount++;
					}
				}
				else
				{
					bullet = new NormalBullet(focus);
				}

				test = State & MakiState.FastShot;
				if(test > 0)
				{
					Random r = new Random();
					int random = r.Next(0, 100);

					if(random < 20)
					{
						bullet = new FastBullet(focus);
					}
				}

				bullet.Transform.Position = new Vector2(this.Transform.Position.X, this.Transform.Position.Y);
				World.Instantiate(bullet);

				_seconds = 0;
			}
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
