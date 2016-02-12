using Microsoft.Xna.Framework;
using CatdogEngine.Playground;
using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework.Graphics;
using CatdogEngine.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CatdogEngine.ScreenSystem
{
	public class WorldTestScreen : GameScreen
	{
		World world;

		public override void LoadContent()
		{
			base.LoadContent();

			world = new World();
			world.Camera.Zoom = 1f;

			/*
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
			world.LoadTileMap(map);
			*/

			// Make some object
			EmptyObject object1 = new EmptyObject();
			object1.Transform.Scale = new Vector2(0.5f);
			object1.Transform.Position = new Vector2(-400, 0);
			object1.AddComponent(new SpriteRenderer("gojam"));
			Location location = new Location(300f, 300f);
			location.ON_TRIGGER_ENTER = delegate (Location other) 
			{
				object1.Transform.Position = new Vector2(-400, 0);
			};
			object1.AddComponent(location);
			object1.UPDATE = delegate (GameTime gameTime) 
			{
				object1.Transform.Translate(new Vector2(1, 0));
			};
			world.Instantiate(object1);

			// Add a existing object
			Behavior testObject = new TestObject();
			testObject.Transform.Scale = new Vector2(0.5f);
			testObject.Transform.Position = new Vector2(0, 0);
			world.Instantiate(testObject);

			// Register world
			SetWorld(world);
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
