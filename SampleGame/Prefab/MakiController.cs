using CatdogEngine.Playground.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SampleGame.Prefab
{
	public class MakiController : Behavior
	{
		private Maki _maki;
		private Difficulty _difficulty;
		private double _seconds;

		private int _phase;

		public MakiController(Maki maki, Difficulty difficulty)
		{
			_maki = maki;
			_seconds = 0;
			_difficulty = difficulty;
			_phase = 1;
		}

		public override void Start()
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			_seconds += gameTime.ElapsedGameTime.Milliseconds / 1000f;
			if(_seconds > 10)
			{
				_phase++;
				_maki.Phase = _phase;

				_seconds = 0;

				switch(_difficulty)
				{
					case Difficulty.Easy:
						break;

					case Difficulty.Normal:
						if(_phase == 2)
						{
							_maki.State = MakiState.Chase | MakiState.AimShot | MakiState.BallBulletShot;
						}
						break;

					case Difficulty.Hard:
						if (_phase == 2)
						{
							_maki.State = MakiState.Chase | MakiState.AimShot | MakiState.BallBulletShot | MakiState.FastShot;
						}
						break;
				}
			}
		}
	}
}
