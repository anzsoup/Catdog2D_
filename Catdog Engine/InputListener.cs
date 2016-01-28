using Microsoft.Xna.Framework.Input;

namespace CatdogEngine
{
	public interface InputListener
	{

		/// <summary>
		/// 마우스 왼쪽 버튼을 눌렀을 때
		/// </summary>
		/// <param name="x">커서의 x좌표</param>
		/// <param name="y">커서의 y좌표</param>
		void OnLeftMouseDown(int x, int y);

		/// <summary>
		/// 마우스 왼쪽 버튼을 뗐을 때
		/// </summary>
		/// <param name="x">커서의 x좌표</param>
		/// <param name="y">커서의 y좌표</param>
		void OnLeftMouseUp(int x, int y);

		/// <summary>
		/// 마우스 커서의 위치가 변할 때
		/// </summary>
		/// <param name="x">현재 커서의 x좌표</param>
		/// <param name="y">현재 커서의 y좌표</param>
		void OnMouseMove(int x, int y);

		/// <summary>
		/// 키보드 버튼을 눌렀을 때
		/// </summary>
		/// <param name="key">새롭게 눌린 버튼</param>
		void OnKeyDown(Keys key);

		/// <summary>
		/// 키보드 버튼을 뗐을 때
		/// </summary>
		/// <param name="key">놓은 버튼</param>
		void OnKeyUp(Keys key);
	}
}
