using CatdogEngine.UI.StencilComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.ScreenSystem;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground;
using CatdogEngine.UI;

namespace SampleGame.Prefab
{
	public class PausePopup : Stencil
	{
		Sprite popup;
		Button resume;
		Button back;

		public PausePopup(MainGameScreen currentScreen) : base(currentScreen)
		{
			popup = new Sprite(currentScreen.Content.Load<Texture2D>("pausepopup"));
			resume = new Button(currentScreen, ButtonType.Purple);
			resume.Position = new Vector2(340, 270);
			resume.Text = "Resume";
			Stencil thisPopup = this;
			resume.ON_CLICK = delegate ()
			{
				currentScreen.Unpause();
			};
			back = new Button(currentScreen, ButtonType.Yellow);
			back.Position = new Vector2(440, 270);
			back.Text = "Back";
			back.ON_CLICK = delegate ()
			{
				currentScreen.ScreenManager.SetScreen(new MenuScreen());
			};
		}

		public override void Update(GameTime gameTime)
		{
			resume.Update(gameTime);
			back.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			popup.Draw(spriteBatch);
			resume.Draw(spriteBatch, gameTime);
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
			resume.OnLeftMouseDown(x, y);
			back.OnLeftMouseDown(x, y);
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			resume.OnLeftMouseUp(x, y);
			back.OnLeftMouseUp(x, y);
		}

		public override void OnMouseMove(int x, int y)
		{
			resume.OnMouseMove(x, y);
			back.OnMouseMove(x, y);
		}
	}
}
