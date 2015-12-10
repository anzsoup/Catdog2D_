using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CatdogEngine.ScreenSystem {
    public class LogoScreen : GameScreen {

        private ScreenManager _screenManager;
        private Texture2D _logo;
        private Rectangle _region;

        public LogoScreen(ScreenManager screenManager) : base(UpdateTransition) {
            ScreenManager = screenManager;
        }

        #region Properties
        public new ScreenManager ScreenManager { get { return _screenManager; } set { _screenManager = value; } }
        #endregion

        public override void LoadContent() {
            _logo = ScreenManager.Content.Load<Texture2D>("logo");
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

        private static bool UpdateTransition(GameTime gameTime, TimeSpan transitionTime, int direction) {
            return false;
        }
    }
}
