using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CatdogEngine.ScreenSystem {
    public class LogoScreen : GameScreen {

        private Texture2D _logo;
        private Rectangle _region;

        public override void LoadContent() {
            _logo = ScreenManager.Content.Load<Texture2D>("Catdog.png");
            _region = new Rectangle(0, 0, _logo.Width, _logo.Height);
        }

        public override void UnloadContent() {
            _logo.Dispose();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(_logo, _region, Color.White);
            ScreenManager.SpriteBatch.End();
        }
    }
}
