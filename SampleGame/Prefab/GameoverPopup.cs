using CatdogEngine.UI.StencilComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.ScreenSystem;

namespace SampleGame.Prefab
{
	public class GameoverPopup : Stencil
	{
		Sprite popup;
		Button retry;
		Button back;

		public GameoverPopup(MainGameScreen currentScreen) : base(currentScreen)
		{
			popup = new Sprite(currentScreen.Content.Load<Texture2D>("gameoverpopup"));

			retry = new Button(currentScreen, ButtonType.Purple);
			retry.Position = new Vector2(300, 300);
			retry.Text = "Retry";
			retry.ON_CLICK = delegate ()
			{
				currentScreen.ScreenManager.SetScreen(new MainGameScreen(currentScreen.Difficulty));
			};

			back = new Button(currentScreen, ButtonType.AshBlue);
			back.Position = new Vector2(400, 300);
			back.Text = "Back";
			back.ON_CLICK = delegate ()
			{
				currentScreen.ScreenManager.SetScreen(new MenuScreen());
			};
		}

		public override void Update(GameTime gameTime)
		{
			retry.Update(gameTime);
			back.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			popup.Draw(spriteBatch);
			retry.Draw(spriteBatch, gameTime);
			back.Draw(spriteBatch, gameTime);
		}

		public override void OnKeyDown(Keys key)
		{
			
		}

		public override void OnKeyUp(Keys key)
		{
			
		}

		public override void OnLeftMouseDown(int x, int y)
		{
			retry.OnLeftMouseDown(x, y);
			back.OnLeftMouseDown(x, y);
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			retry.OnLeftMouseUp(x, y);
			back.OnLeftMouseUp(x, y);
		}

		public override void OnMouseMove(int x, int y)
		{
			retry.OnMouseMove(x, y);
			back.OnMouseMove(x, y);
		}
	}
}
