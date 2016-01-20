using CatdogEngine.ScreenSystem;
using CatdogEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CatdogEngine.UI.StencilComponent;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame {
	public class MenuScreen : GameScreen {
		Canvas canvas;

		Sprite background;

		public override void LoadContent() {
			base.LoadContent();

			// 캔버스 생성
			canvas = new Canvas();

			// 버튼 생성
			Button easyButton = new Button(this);
			easyButton.NormalImage = new Sprite(this.Content.Load<Texture2D>("difficulty_easy"));
			easyButton.ClickedImage = new Sprite(this.Content.Load<Texture2D>("difficulty_easy"));
			easyButton.Text = "";
			easyButton.ON_MOUSE_IN = delegate () {
				easyButton.NormalImage.Scale = new Vector2(1.2f);
				float width = easyButton.NormalImage.TextureWidth;
				float height = easyButton.NormalImage.TextureHeight;
				Vector2 position = easyButton.Position;
				easyButton.NormalImage.Position = new Vector2(position.X - 0.2f * width, position.Y - 0.2f * height);
			};
			easyButton.ON_MOUSE_OUT = delegate() {
				easyButton.NormalImage.Scale = new Vector2(1f);
				easyButton.NormalImage.Position = easyButton.Position;
			};
			easyButton.Position = new Vector2(640, 340);

			Button normalButton = new Button(this);
			normalButton.NormalImage = new Sprite(this.Content.Load<Texture2D>("difficulty_normal"));
			normalButton.ClickedImage = new Sprite(this.Content.Load<Texture2D>("difficulty_normal"));
			normalButton.Text = "";
			normalButton.ON_MOUSE_IN = delegate () {
				normalButton.NormalImage.Scale = new Vector2(1.2f);
				float width = normalButton.NormalImage.TextureWidth;
				float height = normalButton.NormalImage.TextureHeight;
				Vector2 position = normalButton.Position;
				normalButton.NormalImage.Position = new Vector2(position.X - 0.2f * width, position.Y - 0.2f * height);
			};
			normalButton.ON_MOUSE_OUT = delegate () {
				normalButton.NormalImage.Scale = new Vector2(1f);
				normalButton.NormalImage.Position = normalButton.Position;
			};
			normalButton.Position = new Vector2(620, 380);

			Button hardButton = new Button(this);
			hardButton.NormalImage = new Sprite(this.Content.Load<Texture2D>("difficulty_hard"));
			hardButton.ClickedImage = new Sprite(this.Content.Load<Texture2D>("difficulty_hard"));
			hardButton.Text = "";
			hardButton.ON_MOUSE_IN = delegate () {
				hardButton.NormalImage.Scale = new Vector2(1.2f);
				float width = hardButton.NormalImage.TextureWidth;
				float height = hardButton.NormalImage.TextureHeight;
				Vector2 position = hardButton.Position;
				hardButton.NormalImage.Position = new Vector2(position.X - 0.2f * width, position.Y - 0.2f * height);
			};
			hardButton.ON_MOUSE_OUT = delegate () {
				hardButton.NormalImage.Scale = new Vector2(1f);
				hardButton.NormalImage.Position = hardButton.Position;
			};
			hardButton.Position = new Vector2(640, 420);

			// 캔버스에 버튼 추가
			canvas.Add(easyButton);
			canvas.Add(normalButton);
			canvas.Add(hardButton);

			// 뒷배경 이미지 불러오기
			background = new Sprite(this.Content.Load<Texture2D>("menuscreen"));
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			// 캔버스의 Update 로직 추가
			if(canvas != null) canvas.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			// 뒷배경 이미지를 그린다
			if(background != null) background.Draw(ScreenManager.SpriteBatch);

			// 캔버스의 Draw 로직 추가
			// 캔버스는 가장 마지막에 그려야 화면 최상단에 그려진다.
			if(canvas != null) canvas.Draw(gameTime);
		}
	}
}
