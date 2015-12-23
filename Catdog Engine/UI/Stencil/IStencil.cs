using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.UI.Stencil {
    /// <summary>
    /// 화면 최 상단에 그려지는 UI는 모두 이 인터페이스를 상속해야 한다.
    /// Game Screen 내부에서 동작하는 Update와 Draw 로직을 갖는다.
    /// </summary>
    public interface IStencil {
        /// <summary>
        /// 스텐실의 로직을 진행시킨다.
        /// </summary>
        void Update(GameTime gameTime);

        /// <summary>
        /// 해당 스텐실을 화면에 그려야 할 때 호출된다.
        /// </summary>
        void Draw(GameTime gameTime);
    }
}
