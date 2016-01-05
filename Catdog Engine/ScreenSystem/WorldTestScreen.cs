using Microsoft.Xna.Framework;
using CatdogEngine.Playground;
using CatdogEngine.Playground.Object;

namespace CatdogEngine.ScreenSystem {
	public class WorldTestScreen : GameScreen {
		World tileWorld;

		public override void LoadContent() {
			base.LoadContent();

			tileWorld = new TileWorld();
			tileWorld.Initialize(this);
			tileWorld.Camera.Zoom = 1f;

			Behavior testObject = new TestObject();
			testObject.Transform.Scale = new Vector2(0.5f);
			testObject.Transform.Position = new Vector2(-400, 0);
			tileWorld.Instantiate(testObject);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if(tileWorld != null) tileWorld.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			if(tileWorld != null) tileWorld.Draw(gameTime);
		}
	}
}
