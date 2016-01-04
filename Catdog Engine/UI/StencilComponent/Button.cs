using Microsoft.Xna.Framework;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace CatdogEngine.UI.StencilComponent {

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	// 이벤트 콜백을 위한 대리자
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public delegate void BUTTON__MOUSE_IN();
	public delegate void BUTTON__MOUSE_OUT();
	public delegate void BUTTON__LEFT_MOUSE_DOWN(int x, int y);
	public delegate void BUTTON__LEFT_MOUSE_UP(int x, int y);

	/// <summary>
	/// 버튼 UI.
	/// 클릭하여 정해진 동작을 수행할 수 있는 사용자 인터페이스.
	/// Update와 Draw를 virtual로 정의했다. 해당 클래스를 상속하여 원하는 스타일의 버튼을 만들어 사용할 수 있다.
	/// </summary>
	public class Button : Stencil {
		private bool _mouseHover;                           // 커서가 영역 안에 있는가
		private bool _pressed;								// 버튼이 눌렸는가

		private Sprite _defaultImageNormal;                 // 기본 버튼 이미지
		private Sprite _defaultImageClicked;                // 기본 버튼 이미지
		private TextLine _defaultText;						// 기본 텍스트

		private BUTTON__MOUSE_IN _onMouseIn;
		private BUTTON__MOUSE_OUT _onMouseOut;
		private BUTTON__LEFT_MOUSE_DOWN _onLeftMouseDown;
		private BUTTON__LEFT_MOUSE_UP _onLeftMouseUp;

		#region Properties
		public BUTTON__MOUSE_IN ON_MOUSE_IN { set { _onMouseIn = value; } }
		public BUTTON__MOUSE_OUT ON_MOUSE_OUT { set { _onMouseOut = value; } }
		public BUTTON__LEFT_MOUSE_DOWN ON_LEFT_MOUSE_DOWN { set { _onLeftMouseDown = value; } }
		public BUTTON__LEFT_MOUSE_UP ON_LEFT_MOUSE_UP { set { _onLeftMouseUp = value; } }

		public new Vector2 Position {
			get { return base.Position; }
			set {
				base.Position = value;
				_defaultImageNormal.Position = value;
				_defaultImageClicked.Position = value;
				BufferRegion = new Rectangle((int)value.X, (int)value.Y, BufferRegion.Width, BufferRegion.Height);

				// 텍스트의 위치 계산
				_defaultText.Position = new Vector2(value.X + (BufferRegion.Width - _defaultText.BufferRegion.Width) / 2f,
					value.Y + (BufferRegion.Height - _defaultText.BufferRegion.Height) / 2f);
			}
		}
		#endregion

		public Button(GameScreen screen) : base(screen) {
			// 기본 버튼 이미지
			_defaultImageNormal = new Sprite(Screen.Content.Load<Texture2D>("Default_Button_1"));
			_defaultImageNormal.Position = Position;
			_defaultImageClicked = new Sprite(Screen.Content.Load<Texture2D>("Default_Button_2"));
			_defaultImageClicked.Position = Position;

			// 기본 글꼴 및 텍스트
			_defaultText = new TextLine(Screen, Screen.Content.Load<SpriteFont>("DefaultButtonText"), "Button");
			AddInnerStencil(_defaultText);

			// 기본 영역.
			// 기본 영역의 크기는 (클릭 된 상태가 아닌)평상시 이미지의 크기로 하는 것이 좋다.
			BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, _defaultImageNormal.Width, _defaultImageNormal.Height);
		}

		public override void Update(GameTime gameTime) {
			
		}

		public override void Draw(GameTime gameTime) {
			if(_pressed) {
				if (_defaultImageClicked != null) _defaultImageClicked.Draw(ScreenManager.SpriteBatch);
			}
			else {
				if (_defaultImageNormal != null) _defaultImageNormal.Draw(ScreenManager.SpriteBatch);
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Input Events
		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public override void OnLeftMouseDown(int x, int y) {
			if(_mouseHover) {
				// 버튼은 눌린 상태로 전환
				_pressed = true;

				// BUTTON__LEFT_MOUSE_DOWN 이벤트 발생
				if (_onLeftMouseDown != null) _onLeftMouseDown(x, y);
			}
		}

		public override void OnLeftMouseUp(int x, int y) {
			if(_pressed) {
				// 버튼은 눌리지 않은 상태로 전환
				_pressed = false;

				// BUTTON__LEFT_MOUSE_UP 이벤트 발생
				if (_onLeftMouseUp != null) _onLeftMouseUp(x, y);
			}
		}

		public override void OnMouseMove(int x, int y) {
			if (_mouseHover) {
				if (!WindowRegion.Contains(x, y)) {
					// 현재 커서는 영역 밖에 있다.
					_mouseHover = false;

					// BUTTON__MOUSE_OUT 이벤트 발생.
					if (_onMouseOut != null) _onMouseOut();
				}
			}
			else {
				if (WindowRegion.Contains(x, y)) {
					// 현재 커서는 영역 안에 있다.
					_mouseHover = true;

					// BUTTON__MOUSE_IN 이벤트 발생.
					if (_onMouseIn != null) _onMouseIn();
				}
			}
		}

		public override void OnKeyDown(Keys key) {
			
		}

		public override void OnKeyUp(Keys key) {
			
		}
	}
}
