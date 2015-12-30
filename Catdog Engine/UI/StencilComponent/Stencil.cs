using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatdogEngine.UI.StencilComponent {
	/// <summary>
	/// 화면 최 상단에 그려지는 UI는 모두 이 인터페이스를 상속해야 한다.
	/// Game Screen 내부에서 동작하는 Update와 Draw 로직을 갖는다.
	/// </summary>
	public abstract class Stencil {
		// 스텐실의 화면상 위치. 좌측 상단이 원점이다.
		protected Vector2 _position;

		// 현재 캔버스가 속해있는 스크린
		protected GameScreen _screen;

		public Vector2 Position { get { return _position; } set { _position = value; } }
		public GameScreen Screen { get { return _screen; } private set { _screen = value; } }

		public Stencil(GameScreen screen) {
			Position = new Vector2(0, 0);
			Screen = screen;
		}

		/// <summary>
		/// 스텐실의 로직을 진행시킨다.
		/// </summary>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// 해당 스텐실을 화면에 그려야 할 때 호출된다.
		/// </summary>
		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
