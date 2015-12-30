using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Graphics;

namespace CatdogEngine.ScreenSystem {
    public class LogoScreen : GameScreen {

        Sprite logo;
		Sprite gojam;

        public override void LoadContent() {
			// It is Important!
			base.LoadContent();

			// Load Textures.
            logo = new Sprite(Content.Load<Texture2D>("Catdog"));
			logo.Position = new Vector2(ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth/2, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight/2);
			gojam = new Sprite(Content.Load<Texture2D>("gojam"));
			gojam.Position = new Vector2(ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth / 2, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight / 2);
			gojam.Scale = new Vector2(0.5f, 0.5f);
			gojam.Rotation = 45f;

			// We don't need override UnloadContent rogic because 'Content' member does it for us in UnloadContent rogic of base class.
		}

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
			// Draw Textures.
			logo.Draw(ScreenManager.SpriteBatch);
			gojam.Draw(ScreenManager.SpriteBatch);
        }
    }
}
