using CatdogEngine.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.ScreenSystem {
	/// <summary>
	/// Canvas를 갖는 Game Screen
	/// 이 클래스를 상속하면 화면 위에 UI를 찍어낼 수 있다.
	/// </summary>
	public abstract class CanvasScreen : GameScreen {
		private Canvas _canvas;                 // 캔버스

		#region Properties
		public Canvas Canvas { get { return _canvas; } }
		#endregion

		/// <summary>
		/// 자식 클래스에서 Override 할 경우 반드시 한 번 호출해야 한다.
		/// </summary>
		public override void LoadContent() {
			base.LoadContent();

			// Make Canvas
			// Canvas size always equal to Window size
			_canvas = new Canvas(ScreenManager.SpriteBatch, 
				ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth, ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight);
		}

		/// <summary>
		/// 자식 클래스에서 Override 할 경우 반드시 한 번 호출해야 한다.
		/// </summary>
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			int windowWidth = ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth;
			int windowHeight = ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight;

			// Update Canvas
			_canvas.Update(gameTime, windowWidth, windowHeight);
		}

		/// <summary>
		/// 자식 클래스에서 Override 할 경우 반드시 한 번 호출해야 한다.
		/// </summary>
		public override void Draw(GameTime gameTime) {
			// Draw Canvas
			_canvas.Draw(gameTime);
		}
	}
}
