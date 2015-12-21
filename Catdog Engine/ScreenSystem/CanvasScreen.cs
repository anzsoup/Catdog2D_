﻿using CatdogEngine.UI;
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
		private Canvas _canvas;					// 캔버스

		public CanvasScreen() {
			_canvas = new Canvas();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			// Update Canvas
			_canvas.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			// Draw Canvas
			_canvas.Draw(gameTime);
		}
	}
}
