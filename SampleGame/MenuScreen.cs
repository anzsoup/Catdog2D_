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

		public override void LoadContent() {
			base.LoadContent();

			canvas = new Canvas();

			Button button = new Button(this, ButtonType.Red);
			button.Text = "Hello";
			button.Width = 160;
			button.Height = 60;
			canvas.Add(button);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if(canvas != null) canvas.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			if(canvas != null) canvas.Draw(gameTime);
		}
	}
}
