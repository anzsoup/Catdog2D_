using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.UI.StencilComponent;
using CatdogEngine.ScreenSystem;

namespace CatdogEngine.UI {

	/// <summary>
	/// 윈도우 크기에 대해 상대적으로 UI의 크기를 어떻게 처리할 것인지에 대한 옵션
	/// </summary>
	public enum ScaleMode {
		FIXED_SIZE,
		SCALE_WITH_WINDOW
	}

	/// <summary>
	/// UI를 관리한다.
	/// Canvas Screen의 Update, Draw 로직 내에서 동작한다.
	/// 캔버스의 사이즈는 항상 윈도우의 크기와 같다.
	/// 스텐실의 사이즈와 위치는 캔버스 사이즈에 대해 상대적으로 표현되며 0 이상 1 이하의 값을 갖는다.
	/// </summary>
	public class Canvas {
        private List<Stencil> _stencils;										// 스텐실들을 담고 있는 리스트
		private ScaleMode _scaleMode;											// 스케일모드
		private int _bufferWidth, _bufferHeight;								// 버퍼 사이즈. 한번 결정 되면 변하지 않으므로 기억해 둔다.
		private float _windowBufferWidthRate, _windowBufferHeightRate;			// 버퍼 사이즈와 윈도우 사이즈의 비율


		public Canvas() {
            _stencils = new List<Stencil>();

			// 스케일 모드 기본값은 SCALE_WITH_WINDOW
			ScaleMode = ScaleMode.SCALE_WITH_WINDOW;

			// 버퍼 사이즈와 윈도우 사이즈의 비율 초기화
			_windowBufferWidthRate = 1f;
			_windowBufferHeightRate = 1f;

			// 버퍼 사이즈 저장
			_bufferWidth = ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth;
			_bufferHeight = ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight;
		}



		#region Properties
		public ScaleMode ScaleMode { get { return _scaleMode; } set { _scaleMode = value; } }
		public float WindowBufferWidthRate { get { return _windowBufferWidthRate; } private set { _windowBufferWidthRate = value; } }
		public float WindowBufferHeightRate { get { return _windowBufferHeightRate; } private set { _windowBufferHeightRate = value; } }
		#endregion



		/// <summary>
		/// 스케일모드를 변경한다.
		/// </summary>
		/// <param name="scaleMode">변경할 스케일모드</param>
		public void SetScaleMode(ScaleMode scaleMode) {
			ScaleMode = scaleMode;
		}

		public void Add(Stencil stencil) {
			if (stencil != null) {
				stencil.Canvas = this;
				_stencils.Add(stencil);
			}
        }

		public void Update(GameTime gameTime) {
			foreach (Stencil stencil in _stencils) {
				// Update Stencils
				stencil.Update(gameTime);

				// Update Inner Stencils of each Stencil
				foreach(Stencil innerStencil in stencil.InnerStencils) {
					innerStencil.Update(gameTime);
				}
			}
			
			// 윈도우 사이즈가 변하면
			if (ScreenManager.IsWindowSizeChanged) {
				int windowWidth = ScreenManager.WindowConfig.ClientBounds.Width;
				int windowHeight = ScreenManager.WindowConfig.ClientBounds.Height;

				// 버퍼 사이즈와 윈도우 사이즈의 비율을 구한다.
				WindowBufferWidthRate = (float)windowWidth / (float)_bufferWidth;
				WindowBufferHeightRate = (float)windowHeight / (float)_bufferHeight;
			}
		}

		public void Draw(GameTime gameTime) {
			foreach(Stencil stencil in _stencils) {
				// Draw Stencils
				stencil.Draw(gameTime);

				// Draw Inner Stencils of each Stencil
				foreach(Stencil innerStencil in stencil.InnerStencils) {
					innerStencil.Draw(gameTime);
				}
			}
		}
    }
}
