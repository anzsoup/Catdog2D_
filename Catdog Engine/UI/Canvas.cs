using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CatdogEngine.UI {

	/// <summary>
	/// 윈도우 크기에 대해 상대적으로 UI의 크기를 어떻게 처리할 것인지에 대한 옵션
	/// </summary>
	public enum ScaleMode {
		MAINTAIN_INITIAL_SIZE,
		SCALE_WITH_WINDOW
	}

	/// <summary>
	/// UI를 관리한다.
	/// Canvas Screen의 Update, Draw 로직 내에서 동작한다.
	/// </summary>
	public class Canvas {
        private List<IStencil> _stencils;               // 스텐실들을 담고 있는 리스트
		private ScaleMode _scaleMode;					// 스케일모드

		public Canvas() {
            _stencils = new List<IStencil>();
			ScaleMode = ScaleMode.SCALE_WITH_WINDOW;
		}

		#region Properties
		public ScaleMode ScaleMode { get { return _scaleMode; } set { _scaleMode = value; } }
		#endregion

		/// <summary>
		/// 스케일모드를 변경한다.
		/// </summary>
		/// <param name="scaleMode">변경할 스케일모드</param>
		public void SetScaleMode(ScaleMode scaleMode) {
			ScaleMode = scaleMode;
		}

		public void Add(IStencil stencil) {
            if(stencil == null) {
                Debug.WriteLine("### Canvas.Add Failed : The Argument can not be Null. ###");
            }
            else {
                _stencils.Add(stencil);
            }
        }

		public void Update(GameTime gameTime) {
			for(int i=0; i<_stencils.Count; ++i) {
				IStencil stencil = _stencils[i];
				stencil.Update(gameTime);
			}
		}

		public void Draw(GameTime gameTime) {
			for(int i=0; i<_stencils.Count; ++i) {
				IStencil stencil = _stencils[i];
				stencil.Draw(gameTime);
			}
		}
    }
}
