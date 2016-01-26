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

namespace SampleGame {
	public enum Difficulty {
		Easy,
		Normal,
		Hard
	}
	public class MainGameScreen : GameScreen {
		float currentScore;
		Difficulty difficulty;

		Song bgm;
		Sprite background;
		World world;
		Canvas canvas;
		TextLine score;

		public MainGameScreen(Difficulty difficulty) {
			this.difficulty = difficulty;
			currentScore = 0f;
			world = new World(this);
			canvas = new Canvas();

			TransitionTime = new TimeSpan(0, 0, 2);
		}

		public override void LoadContent() {
			base.LoadContent();

			// 배경
			background = new Sprite(this.Content.Load<Texture2D>("maingamescreen"));

			// BGM
			bgm = this.Content.Load<Song>("bgm2");
			MediaPlayer.Play(bgm);

			// 점수판
			score = new TextLine(this, this.Content.Load<SpriteFont>("score"));
			canvas.Add(score);

			// 유즈키
			Yuzuki yuzuki = new Yuzuki();
			yuzuki.Transform.Position = new Vector2(-400 + 32, -240 + 72 + 32);
			world.Instantiate(yuzuki);

			// 마키
			Maki maki = new Maki(yuzuki);
			maki.Transform.Position = new Vector2(400 - 52 - 32, 240 - 32);
			world.Instantiate(maki);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			// 월드의 Update 로직 추가
			world.Update(gameTime);
			
			if (score != null) score.Text = "Score : " + currentScore;
			canvas.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			if(background != null) background.Draw(ScreenManager.SpriteBatch);

			// 월드의 Draw 로직 추가
			world.Draw(gameTime);

			canvas.Draw(gameTime);
		}

		public override void UnloadContent() {
			base.UnloadContent();

			MediaPlayer.Stop();
		}
	}
}
