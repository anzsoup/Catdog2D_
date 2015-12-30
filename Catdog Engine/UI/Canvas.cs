using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.UI.StencilComponent;

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
        private List<Stencil> _stencils;                // 스텐실들을 담고 있는 리스트
		private ScaleMode _scaleMode;                   // 스케일모드
		private int _width, _height;                    // 캔버스 사이즈 (절대적 크기)

		private SpriteBatch _spriteBatch;               // ScreenManager의 SpriteBatch

		public Canvas(SpriteBatch spriteBatch, int width, int height) {
            _stencils = new List<Stencil>();

			// 스케일 모드 기본값은 SCALE_WITH_WINDOW
			ScaleMode = ScaleMode.SCALE_WITH_WINDOW;

			// 캔버스 사이즈 초기화
			SetCanvasSize(width, height);

			SpriteBatch = spriteBatch;
		}

		#region Properties
		public ScaleMode ScaleMode { get { return _scaleMode; } set { _scaleMode = value; } }
		public SpriteBatch SpriteBatch { get { return _spriteBatch; } set { _spriteBatch = value; } }
		#endregion

		/// <summary>
		/// 스케일모드를 변경한다.
		/// </summary>
		/// <param name="scaleMode">변경할 스케일모드</param>
		public void SetScaleMode(ScaleMode scaleMode) {
			ScaleMode = scaleMode;
		}

		public void Add(Stencil stencil) {
            if(stencil == null) {
                Debug.WriteLine("### Canvas.Add Failed : The Argument can not be Null. ###");
            }
            else {
                _stencils.Add(stencil);
            }
        }

		public void SetCanvasSize(int width, int height) {
			_width = width;
			_height = height;
		}

		public void Update(GameTime gameTime, int windowWidth, int windowHeight) {
			for (int i = 0; i < _stencils.Count; ++i) {
				Stencil stencil = _stencils[i];
				stencil.Update(gameTime);
			}

			// 윈도우 사이즈가 변하면 캔버스의 사이즈도 그에 맞추어 갱신한다.
			if (windowWidth != _width || windowHeight != _height) SetCanvasSize(windowWidth, windowHeight);
		}

		public void Draw(GameTime gameTime) {
			for(int i=0; i<_stencils.Count; ++i) {
				Stencil stencil = _stencils[i];
				stencil.Draw(SpriteBatch, gameTime);
			}
		}
    }
}
