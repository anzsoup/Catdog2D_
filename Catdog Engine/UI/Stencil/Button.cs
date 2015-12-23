using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.Graphics2D;
using Microsoft.Xna.Framework.Graphics;

namespace CatdogEngine.UI.Stencil {

	/////////////////////////////////////////////////////////////////
	// 이벤트 콜백을 위한 대리자
	/////////////////////////////////////////////////////////////////
	public delegate void BUTTON__MOUSE_IN();
	public delegate void BUTTON__MOUSE_OUT();
	public delegate void BUTTON__LEFT_MOUSE_DOWN(int x, int y);
	public delegate void BUTTON__LEFT_MOUSE_UP(int x, int y);

	/// <summary>
	/// 버튼 UI
	/// 클릭하여 정해진 동작을 수행할 수 있는 사용자 인터페이스
	/// </summary>
	public class Button : IStencil {
		private Rectangle _region;							// 버튼의 영역
		private bool _mouseHover;                           // 커서가 영역 안에 있는가
		private bool _leftMousePressed;                     // 마우스 왼쪽 버튼이 눌려있는가

		private Sprite _pressedImage;						// 눌린 상태의 이미지
		private Sprite _normalImage;						// 평상시 이미지

		private BUTTON__MOUSE_IN _onMouseIn;
		private BUTTON__MOUSE_OUT _onMouseOut;
		private BUTTON__LEFT_MOUSE_DOWN _onLeftMouseDown;
		private BUTTON__LEFT_MOUSE_UP _onLeftMouseUp;

		#region Properties
		public BUTTON__MOUSE_IN OnMouseIn { set { _onMouseIn = value; } }
		public BUTTON__MOUSE_OUT OnMouseOut { set { _onMouseOut = value; } }
		public BUTTON__LEFT_MOUSE_DOWN OnLeftMouseDown { set { _onLeftMouseDown = value; } }
		public BUTTON__LEFT_MOUSE_UP OnLeftMouseUp { set { _onLeftMouseUp = value; } }

		public Sprite PressedImage {
			get { return _pressedImage; }
			set {
				_pressedImage = value;
				if(_pressedImage != null) {

				}
			}
		}
		#endregion

		public Button() {
			//기본 버튼 이미지
		}

		public void Update(GameTime gameTime) {
			// 현재 마우스 State를 본다.
			MouseState mouseState = Mouse.GetState();

			// 커서의 위치를 읽는다.
			int mousePositionX = mouseState.X;
			int mousePositionY = mouseState.Y;

			if(_mouseHover) {
				if(!_region.Contains(mousePositionX, mousePositionY)) {
					// 현재 커서는 영역 밖에 있다.
					_mouseHover = false;

					// BUTTON__MOUSE_OUT 이벤트 발생.
					if(_onMouseOut != null) _onMouseOut();
				}
			}
			else {
				if (_region.Contains(mousePositionX, mousePositionY)) {
					// 현재 커서는 영역 안에 있다.
					_mouseHover = true;

					// BUTTON__MOUSE_IN 이벤트 발생.
					if(_onMouseIn != null) _onMouseIn();
				}
			}

			if(mouseState.LeftButton == ButtonState.Pressed) {
				if(!_leftMousePressed) {
					// 현재 왼쪽 마우스는 클릭 된 상태다.
					_leftMousePressed = true;

					// BUTTON__LEFT_MOUSE_DOWN 이벤트 발생
					if(_onLeftMouseDown != null) _onLeftMouseDown(mousePositionX, mousePositionY);
				}
			}
			else if(mouseState.LeftButton == ButtonState.Released) {
				if(_leftMousePressed) {
					// 현재 왼쪽 마우스는 클릭 되지 않은 상태다.
					_leftMousePressed = false;

					// BUTTON__LEFT_MOUSE_UP 이벤트 발생
					if(_onLeftMouseUp != null) _onLeftMouseUp(mousePositionX, mousePositionY);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			if(_leftMousePressed) {
				if (_pressedImage != null) _pressedImage.Draw(spriteBatch);
			}
			else {
				if (_normalImage != null) _normalImage.Draw(spriteBatch);
			}
		}
	}
}
