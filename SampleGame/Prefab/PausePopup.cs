using CatdogEngine.UI.StencilComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.ScreenSystem;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace SampleGame.Prefab
{
	public class PausePopup : Stencil
	{
		private Sprite _popup;

		public PausePopup(GameScreen currentScreen) : base(currentScreen)
		{
			_popup = new Sprite(currentScreen.Content.Load<Texture2D>("pausepopup"));
		}

		public override void Update(GameTime gameTime)
		{
			
		}

		public override void Draw(GameTime gameTime)
		{
			_popup.Draw(ScreenManager.SpriteBatch);
		}

		public override void OnKeyDown(Keys key)
		{
			
		}

		public override void OnKeyUp(Keys key)
		{
			
		}

		public override void OnLeftMouseDown(int x, int y)
		{
			
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			
		}

		public override void OnMouseMove(int x, int y)
		{
			
		}
	}
}
