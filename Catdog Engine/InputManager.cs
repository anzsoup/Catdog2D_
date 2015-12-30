using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatdogEngine {
	/// <summary>
	/// 윈도우 이벤트를 감지하고 적절한 타이밍에 함수를 호출해 주는 모듈.
	/// 윈도우 이벤트를 사용하고 싶은 클래스는 InputListener 인터페이스를 상속하고
	/// SetListener(InputListener) 함수를 통해 InputManager에 등록시켜야 한다.
	/// 싱글톤 인스턴스를 갖는다. 임의로 여러 인스턴스를 생성할 수 없다.
	/// </summary>
	public static class InputManager {
		private static List<InputListener> _listeners = new List<InputListener>();

		private static bool _leftMousePressed;
		private static int _oldMousePositionX, _lodMousePositionY;

		/// <summary>
		/// 리스너를 등록한다.
		/// 리스너로 등록한 클래스는 각 윈도우 이벤트의 콜백 함수를 사용할 수 있다.
		/// </summary>
		public static void SetListener(InputListener listener) {
			_listeners.Add(listener);
		}

		/// <summary>
		/// ScreenManager의 Update 로직에서 호출된다.
		/// </summary>
		public static void Update() {
			// 마우스 이벤트 감지
			ListenMouseEvent();

			// 키보드 이벤트 감지
			ListenKeyboardEvent();
		}

		private static void ListenMouseEvent() {
			// 현재 마우스 State를 확인한다.
			MouseState mouseState = Mouse.GetState();

			// 커서의 위치를 읽는다.
			int mousePositionX = mouseState.X;
			int mousePositionY = mouseState.Y;

			if(mouseState.LeftButton == ButtonState.Pressed) {
				if(!_leftMousePressed) {
					_leftMousePressed = true;
					// Call back OnLeftMouseDown
					for (int i = 0; i < _listeners.Count; ++i) _listeners[i].OnLeftMouseDown(mousePositionX, mousePositionY);
				}
			}
			else if(mouseState.LeftButton == ButtonState.Released) {
				if(_leftMousePressed) {
					_leftMousePressed = false;
					// Call back OnLeftMouseUp
					for (int i = 0; i < _listeners.Count; ++i) _listeners[i].OnLeftMouseUp(mousePositionX, mousePositionY);
				}
			}

			if(mousePositionX != _oldMousePositionX || mousePositionY != _lodMousePositionY) {
				// Call back OnMouseMove
				for (int i = 0; i < _listeners.Count; ++i) _listeners[i].OnMouseMove(mousePositionX, mousePositionY);
			}

			// 모든 이벤트 처리가 끝나면 현재 마우스 위치를 저장한다.
			_oldMousePositionX = mousePositionX;
			_lodMousePositionY = mousePositionY;
		}

		private static void ListenKeyboardEvent() {

		}
	}
}
