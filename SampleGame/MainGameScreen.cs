using CatdogEngine.Graphics;
using CatdogEngine.Playground;
using CatdogEngine.UI;
using CatdogEngine.UI.StencilComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SampleGame.Prefab;
using System;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using CatdogEngine.ScreenSystem;

namespace SampleGame
{
	public enum Difficulty
	{
		Easy,
		Normal,
		Hard
	}
	public class MainGameScreen : GameScreen
	{
		float currentScore;
		Difficulty difficulty;

		Song bgm;
		Sprite background;
		World world;
		Canvas canvas;
		TextLine score;
		string scoreText;

		Stencil pausePopup;
		Stencil gameoverPopup;
		bool isPaused;

		Yuzuki yukari;

		int milliseconds;

		#region Properties
		public Difficulty Difficulty { get { return difficulty; } }
		#endregion

		public MainGameScreen(Difficulty difficulty)
		{
			this.difficulty = difficulty;
			currentScore = 0f;
			world = new World(this);
			canvas = new Canvas();
			milliseconds = 0;
			isPaused = false;
			switch(difficulty)
			{
				case Difficulty.Easy:
					scoreText = "Score(x1) : ";
					break;

				case Difficulty.Normal:
					scoreText = "Score(x10) : ";
					break;

				case Difficulty.Hard:
					scoreText = "Score(x50) : ";
					break;
			}

			TransitionTime = new TimeSpan(0, 0, 2);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			// 배경
			background = new Sprite(this.Content.Load<Texture2D>("maingamescreen"));

			// BGM
			bgm = this.Content.Load<Song>("bgm2");
			MediaPlayer.Play(bgm);

			// 유즈키
			yukari = new Yuzuki();
			yukari.Transform.Position = new Vector2(-400 + 32, -240 + 72 + 32);
			world.Instantiate(yukari);

			// 마키
			Tsurumaki maki = new Tsurumaki(yukari);
			maki.Transform.Position = new Vector2(400 - 52 - 32, 240 - 32);
			world.Instantiate(maki);

			// 마키 컨트롤러
			MakiController controller = new MakiController(maki, difficulty);
			world.Instantiate(controller);

			// 점수판
			score = new TextLine(this, this.Content.Load<SpriteFont>("score"));
			canvas.Add(score);

			// HP 미터
			HPMeter hpMeter = new HPMeter(this, yukari);
			hpMeter.Position = new Vector2(0, 20);
			canvas.Add(hpMeter);

			// 팝업
			pausePopup = new PausePopup(this);
			gameoverPopup = new GameoverPopup(this);

			// 월드 등록
			this.World = world;

			// 캔버스 등록
			this.Canvas = canvas;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			milliseconds += gameTime.ElapsedGameTime.Milliseconds;

			if (!isPaused)
			{
				switch (difficulty)
				{
					case Difficulty.Easy:
						currentScore += milliseconds / 10000f;
						break;

					case Difficulty.Normal:
						currentScore += milliseconds / 1000f;
						break;

					case Difficulty.Hard:
						currentScore += milliseconds / 200f;
						break;
				}
			}

			if (score != null) score.Text = scoreText + (int)currentScore;

			if(yukari != null && yukari.HP <= 0)
			{
				if(!isPaused) GameOver();
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(background != null) background.Draw(spriteBatch);
		}

		public override void UnloadContent()
		{
			base.UnloadContent();

			MediaPlayer.Stop();
		}



		public override void OnKeyDown(Keys key)
		{
			base.OnKeyDown(key);

			if(key == Keys.Escape)
			{
				if(isPaused)
				{
					Unpause();
				}
				else
				{
					Pause();
				}
				
			}
		}

		public void Pause()
		{
			isPaused = true;
			this.World.Pause();
			this.Canvas.Add(pausePopup);
		}

		public void Unpause()
		{
			isPaused = false;
			this.World.Unpause();
			this.Canvas.Remove(pausePopup);
		}

		private void GameOver()
		{
			if (currentScore > GameData.Instance.MaxScore)
			{
				score.Text += "  <--- New Record!!";
				GameData.Instance.MaxScore = (int)currentScore;
			}

			isPaused = true;
			this.World.Pause();
			this.Canvas.Add(gameoverPopup);
		}
	}
}
