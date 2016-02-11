using Microsoft.Xna.Framework;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Playground;
using CatdogEngine.Playground.Object;

namespace MyGame
{
	public class MyScreen : GameScreen 
	{
		World world;

		public MyScreen()
		{
			// World 인스턴스 생성
			world = new World(this);
		}

		public override void LoadContent() 
		{
			base.LoadContent();

			// 스크린에 World를 등록한다.
			this.World = world;

			// 빈 오브젝트 생성
			EmptyObject emptyObject = new EmptyObject();

			emptyObject.START = delegate ()
			{
				// Start 메소드
			};

			emptyObject.UPDATE = delegate (GameTime gameTime)
			{
				// Update 메소드
			};

			// 정의한 오브젝트를 Instantiate
			world.Instantiate(emptyObject);
		}

		public override void Update(GameTime gameTime) 
		{
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) 
		{
			
		}
	}
}
