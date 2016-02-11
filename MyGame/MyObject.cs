using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
	public class MyObject : Behavior
	{
		public override void Start()
		{
			// Instantiate 된 직후 호출된다.
		}

		public override void Update(GameTime gameTime)
		{
			// 매 프레임마다(World의 Update로직에서) 호출된다.
		}

		public override void OnKeyDown(Keys key)
		{
			// 키보드 키를 눌렀을 때
			// key : 현재 누른 키
		}

		public override void OnKeyUp(Keys key)
		{
			// 키보드 키를 놓았을 때
			// key : 방금 누르고 있었던 키
		}

		public override void OnLeftMouseDown(int x, int y)
		{
			// 마우스 왼쪽버튼을 눌렀을 때
			// x, y : 현재 커서 좌표
		}

		public override void OnLeftMouseUp(int x, int y)
		{
			// 마우스 왼쪽버튼을 놓았을 때
			// x, y : 현재 커서 좌표
		}

		public override void OnMouseMove(int x, int y)
		{
			// 마우스를 움직였을 때
			// x, y : 현재 커서 좌표
		}

		public override void OnTriggerEnter(Location mine, Location other)
		{
			// 다른 Location이 내 Location 영역으로 들어왔을 때
			// mine : 내 Location, other : 상대방의 Location
		}

		public override void OnTriggerExit(Location mine, Location other)
		{
			// 내 Location 영역에 있던 Location이 빠져나갔을 때
			// mine : 내 Location, other : 상대방의 Location
		}
	}
}
