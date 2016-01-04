using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Graphics;
using CatdogEngine.UI;
using CatdogEngine.UI.StencilComponent;

namespace CatdogEngine.ScreenSystem {
    public class LogoScreen : GameScreen {

        Sprite logo;
		Sprite gojam;

		Canvas canvas;

        public override void LoadContent() {
			// It is Important!
			base.LoadContent();

			// 화면 전환 효과 시간 2초
			TransitionTime = new System.TimeSpan(0, 0, 2);

			// Load Textures.
            logo = new Sprite(Content.Load<Texture2D>("Catdog"));
			logo.Position = new Vector2(0, 0);
			gojam = new Sprite(Content.Load<Texture2D>("gojam"));
			gojam.Position = new Vector2(0, 0);
			gojam.Scale = new Vector2(0.5f, 0.5f);
			gojam.Rotation = 45f;

			// Canvas
			canvas = new Canvas();

			// Button
			Button button = new Button(this);
			button.Position = new Vector2(250, 350);
			canvas.Add(button);

			// We don't need override UnloadContent logic because 'Content' member does it for us in UnloadContent logic of base class.
		}

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

			canvas.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
			// Draw Textures.
			logo.Draw(ScreenManager.SpriteBatch);
			gojam.Draw(ScreenManager.SpriteBatch);

			// 캔버스를 가장 마지막에 그려야 화면 최상단에 그려진다.
			canvas.Draw(gameTime);
        }
    }
}
