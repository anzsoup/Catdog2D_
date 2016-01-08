using Microsoft.Xna.Framework;
using CatdogEngine.Playground;
using CatdogEngine.Playground.Object;

namespace CatdogEngine.ScreenSystem {
	public class WorldTestScreen : GameScreen {
		TileWorld tileWorld;

		public override void LoadContent() {
			base.LoadContent();

			tileWorld = new TileWorld(this, 40);
			tileWorld.Camera.Zoom = 1f;

			// Tile Map
			TileMap map = new TileMap(20, 12);

			// Define Tiles
			TileConfig tile1 = new TileConfig("tile01");
			TileConfig tile2 = new TileConfig("tile02");

			// Design Tile Map
			map.TileAt[0, 0] = tile1;
			map.TileAt[19, 11] = tile2;
			map.TileAt[10, 5] = tile1;

			// Load Map
			tileWorld.LoadTileMap(map);

			// Add some Object
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
