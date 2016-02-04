using CatdogEngine;
using CatdogEngine.Graphics;
using CatdogEngine.Playground;
using CatdogEngine.ScreenSystem;
using CatdogEngine.UI;
using CatdogEngine.UI.StencilComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SampleGame.Prefab;
using System;
using Microsoft.Xna.Framework.Input;

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

		int milliseconds;

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
			Yuzuki yuzuki = new Yuzuki();
			yuzuki.Transform.Position = new Vector2(-400 + 32, -240 + 72 + 32);
			world.Instantiate(yuzuki);

			// 마키
			Maki maki = new Maki(yuzuki);
			maki.Transform.Position = new Vector2(400 - 52 - 32, 240 - 32);
			world.Instantiate(maki);

			// 마키 컨트롤러
			MakiController controller = new MakiController(maki, difficulty);
			world.Instantiate(controller);

			// 점수판
			score = new TextLine(this, this.Content.Load<SpriteFont>("score"));
			canvas.Add(score);

			// HP 미터
			HPMeter hpMeter = new HPMeter(this, yuzuki);
			hpMeter.Position = new Vector2(0, 20);
			canvas.Add(hpMeter);

			// 팝업
			pausePopup = new PausePopup(this);
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

			// 월드의 Update 로직 추가
			world.Update(gameTime);

			if (score != null) score.Text = scoreText + (int)currentScore;
			canvas.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if(background != null) background.Draw(ScreenManager.SpriteBatch);

			// 월드의 Draw 로직 추가
			world.Draw(gameTime);

			canvas.Draw(gameTime);
		}

		public override void UnloadContent()
		{
			base.UnloadContent();

			MediaPlayer.Stop();
		}

		public override void OnLeftMouseDown(int x, int y)
		{
			
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			
		}

		public override void OnMouseMove(int x, int y)
		{
			
		}

		public override void OnKeyDown(Keys key)
		{
			if(key == Keys.Escape)
			{
				if(isPaused)
				{
					isPaused = false;
					world.Unpause();
					canvas.Remove(pausePopup);
				}
				else
				{
					isPaused = true;
					world.Pause();
					canvas.Add(pausePopup);
				}
				
			}
		}

		public override void OnKeyUp(Keys key)
		{
			
		}
	}
}
