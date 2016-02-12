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
			// 월드 등록
			world = new World();
			SetWorld(world);
		}

		public override void LoadContent() 
		{
			base.LoadContent();

			// GojamChan을 생성하고 월드에 추가
			Behavior gojamchan = new GojamChan();
			gojamchan.Transform.Position = new Vector2(400f, 0f);
			world.Instantiate(gojamchan);
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
