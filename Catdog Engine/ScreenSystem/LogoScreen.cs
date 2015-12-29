using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CatdogEngine.Graphics;

namespace CatdogEngine.ScreenSystem {
    public class LogoScreen : GameScreen {

        Sprite logo;
		Sprite gojam;

        public override void LoadContent() {
            logo = new Sprite(ResourceManager.Instance.Load<Texture2D>("Catdog"));
			logo.Position = new Vector2(ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth/2, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight/2);
			gojam = new Sprite(ResourceManager.Instance.Load<Texture2D>("gojam"));
			gojam.Position = new Vector2(ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth / 2, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight / 2);
			gojam.Scale = new Vector2(0.5f, 0.5f);
			gojam.Rotation = 45f;
		}

        public override void UnloadContent() {
			logo.Dispose();
			gojam.Dispose();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
			logo.Draw(ScreenManager.SpriteBatch);
			gojam.Draw(ScreenManager.SpriteBatch);
        }
    }
}
