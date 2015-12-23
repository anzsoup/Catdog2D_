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

        public override void LoadContent() {
            logo = new Sprite(ScreenManager.Content.Load<Texture2D>("Catdog"));
        }

        public override void UnloadContent() {
			logo.Dispose();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
			logo.Draw(ScreenManager.SpriteBatch);
        }
    }
}
