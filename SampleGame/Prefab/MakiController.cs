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
		private List<Maki> _maki;
		private Difficulty _difficulty;
		private double _seconds;

		private int _phase;

		public MakiController(Maki maki, Difficulty difficulty)
		{
			_maki = new List<Maki>();
			_maki.Add(maki);
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
				foreach(Maki maki in _maki)
				{
					maki.Phase = _phase;
				}

				_seconds = 0;

				switch(_difficulty)
				{
					case Difficulty.Easy:
						break;

					case Difficulty.Normal:
						break;

					case Difficulty.Hard:
						break;
				}
			}
		}
	}
}
