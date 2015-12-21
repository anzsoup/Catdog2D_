using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CatdogEngine.UI {
    /// <summary>
    /// UI를 관리한다.
    /// Canvas Screen의 Update, Draw 로직 내에서 동작한다.
    /// </summary>
    public class Canvas {
        private List<IStencil> _stencils;                // 스텐실들을 담고 있는 리스트

        public Canvas() {
            _stencils = new List<IStencil>();
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
