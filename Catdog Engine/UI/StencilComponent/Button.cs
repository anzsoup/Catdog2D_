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
	public delegate void BUTTON__CLICK();

	public enum ButtonType {
		Gray,
		Red,
		Yellow,
		Green,
		Blue,
		Purple,
		MintBlue,
		AshBlue,
		Black
	}

	/// <summary>
	/// 버튼 UI.
	/// 클릭하여 정해진 동작을 수행할 수 있는 사용자 인터페이스.
	/// Update와 Draw를 virtual로 정의했다. 해당 클래스를 상속하여 원하는 스타일의 버튼을 만들어 사용할 수 있다.
	/// </summary>
	public class Button : Stencil {
		private bool _mouseHover;                           // 커서가 영역 안에 있는가
		private bool _pressed;								// 버튼이 눌렸는가

		private Sprite _imageNormal;						// 버튼 이미지
		private Sprite _imageClicked;						// 버튼 이미지
		private TextLine _text;								// 텍스트

		private BUTTON__MOUSE_IN _onMouseIn;
		private BUTTON__MOUSE_OUT _onMouseOut;
		private BUTTON__LEFT_MOUSE_DOWN _onLeftMouseDown;
		private BUTTON__LEFT_MOUSE_UP _onLeftMouseUp;
		private BUTTON__CLICK _onClick;

		#region Properties
		public BUTTON__MOUSE_IN ON_MOUSE_IN { set { _onMouseIn = value; } }
		public BUTTON__MOUSE_OUT ON_MOUSE_OUT { set { _onMouseOut = value; } }
		public BUTTON__LEFT_MOUSE_DOWN ON_LEFT_MOUSE_DOWN { set { _onLeftMouseDown = value; } }
		public BUTTON__LEFT_MOUSE_UP ON_LEFT_MOUSE_UP { set { _onLeftMouseUp = value; } }
		public BUTTON__CLICK ON_CLICK { set { _onClick = value; } }

		public new Vector2 Position {
			get { return base.Position; }
			set {
				base.Position = value;
				_imageNormal.Position = value;
				_imageClicked.Position = value;
				BufferRegion = new Rectangle((int)value.X, (int)value.Y, BufferRegion.Width, BufferRegion.Height);

				// 텍스트의 위치 계산
				_text.Position = new Vector2(value.X + (BufferRegion.Width - _text.BufferRegion.Width) / 2f,
					value.Y + (BufferRegion.Height - _text.BufferRegion.Height) / 2f);
			}
		}

		public string Text {
			set {
				if (value == null) {
					_text.Text = "Button";
				}
				else {
					_text.Text = value;
				}

				// 텍스트 위치 갱신
				_text.Position = new Vector2(Position.X + (BufferRegion.Width - _text.BufferRegion.Width) / 2f,
					Position.Y + (BufferRegion.Height - _text.BufferRegion.Height) / 2f);
			}
		}

		public float Width {
			get { return BufferRegion.Width; }
			set {
				BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)value, BufferRegion.Height);

				// 텍스트 위치 갱신
				_text.Position = new Vector2(Position.X + (BufferRegion.Width - _text.BufferRegion.Width) / 2f,
					Position.Y + (BufferRegion.Height - _text.BufferRegion.Height) / 2f);

				// 버튼 이미지 크기 조절
				_imageNormal.Scale = new Vector2(value / _imageNormal.TextureWidth, _imageNormal.Scale.Y);
				_imageClicked.Scale = new Vector2(value / _imageClicked.TextureWidth, _imageClicked.Scale.Y);
			}
		}

		public float Height {
			get { return BufferRegion.Height; }
			set {
				BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, BufferRegion.Width, (int)value);

				// 텍스트 위치 갱신
				_text.Position = new Vector2(Position.X + (BufferRegion.Width - _text.BufferRegion.Width) / 2f,
					Position.Y + (BufferRegion.Height - _text.BufferRegion.Height) / 2f);

				// 버튼 이미지 크기 조절
				_imageNormal.Scale = new Vector2(_imageNormal.Scale.X, value / _imageNormal.TextureHeight);
				_imageClicked.Scale = new Vector2(_imageClicked.Scale.X, value / _imageClicked.TextureHeight);
			}
		}

		public Sprite NormalImage {
			get { return _imageNormal; }
			set {
				if(value == null) _imageNormal = new Sprite(Screen.Content.Load<Texture2D>("catdog/button_gray_1"));
				else _imageNormal = value;
				_imageNormal.Scale = new Vector2((float)BufferRegion.Width / (float)_imageNormal.TextureWidth, (float)BufferRegion.Height / (float)_imageNormal.TextureHeight);
				_imageNormal.Position = Position;
			}
		}
		public Sprite ClickedImage {
			get { return _imageClicked; }
			set {
				if(value == null) _imageClicked = new Sprite(Screen.Content.Load<Texture2D>("catdog/button_gray_2"));
				else _imageClicked = value;
				_imageClicked.Scale = new Vector2((float)BufferRegion.Width / (float)_imageClicked.TextureWidth, (float)BufferRegion.Height / (float)_imageClicked.TextureHeight);
				_imageClicked.Position = Position;
			}
		}
		#endregion

		public Button(GameScreen screen) : base(screen) {
			// 기본 버튼 이미지
			_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_gray_1"));
			_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_gray_2"));

			// 기본 글꼴 및 텍스트
			_text = new TextLine(screen, screen.Content.Load<SpriteFont>("catdog/default_button_text"), "Button");
			AddInnerStencil(_text);

			// 기본 영역.
			// 영역의 크기는 (클릭 된 상태가 아닌)평상시 이미지의 크기로 하는 것이 좋다.
			BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)_imageNormal.Width, (int)_imageNormal.Height);

			Position = new Vector2(0, 0);
		}

		public Button(GameScreen screen, ButtonType type) : base(screen) {
			// 버튼 이미지
			switch(type) {
				case ButtonType.Gray:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_gray_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_gray_2"));
					break;

				case ButtonType.Red:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_red_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_red_2"));
					break;

				case ButtonType.Yellow:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_yellow_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_yellow_2"));
					break;

				case ButtonType.Green:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_green_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_green_2"));
					break;

				case ButtonType.Blue:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_blue_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_blue_2"));
					break;

				case ButtonType.Purple:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_purple_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_purple_2"));
					break;

				case ButtonType.MintBlue:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_mintblue_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_mintblue_2"));
					break;

				case ButtonType.AshBlue:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_ashblue_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_ashblue_2"));
					break;

				case ButtonType.Black:
					_imageNormal = new Sprite(screen.Content.Load<Texture2D>("catdog/button_black_1"));
					_imageClicked = new Sprite(screen.Content.Load<Texture2D>("catdog/button_black_2"));
					break;
			}

			// 기본 글꼴 및 텍스트
			_text = new TextLine(screen, screen.Content.Load<SpriteFont>("catdog/default_button_text"), "Button");
			AddInnerStencil(_text);

			// 영역의 크기는 (클릭 된 상태가 아닌)평상시 이미지의 크기로 하는 것이 좋다.
			BufferRegion = new Rectangle((int)Position.X, (int)Position.Y, (int)_imageNormal.Width, (int)_imageNormal.Height);

			Position = new Vector2(0, 0);
		}

		public override void Update(GameTime gameTime) {
			
		}

		public override void Draw(GameTime gameTime) {
			if(_pressed) {
				if (_imageClicked != null) _imageClicked.Draw(ScreenManager.SpriteBatch);
			}
			else {
				if (_imageNormal != null) _imageNormal.Draw(ScreenManager.SpriteBatch);
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
				if (_onClick != null) _onClick();
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
