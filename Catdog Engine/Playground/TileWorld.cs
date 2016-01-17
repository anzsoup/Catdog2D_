using CatdogEngine.Graphics;
using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatdogEngine.ScreenSystem;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using CatdogEngine.Playground.PhysicsSystem;

namespace CatdogEngine.Playground {
	public class TileWorld : World {
		private int _tileSize;                                      // 타일들의 사이즈. 불변하는 값.

		private readonly float PIXEL_PER_CENTIMETER = 40f;          // pixels/cm

		private Physics _physics;
		private SimpleAABB _simpleAABB;

		#region Properties
		public int TileSize { get { return _tileSize; } }
		public override Vector2 Gravity {
			get {
				return base.Gravity * PIXEL_PER_CENTIMETER;
			}

			set {
				base.Gravity = value;
			}
		}

		public new Physics Physics { get { return _physics; } set { _physics = value; } }
		public CollisionModule TileWorldCollisionModule { get { return _simpleAABB; } }
		#endregion

		public TileWorld(GameScreen currentScreen, int tileSize = 40) : base(currentScreen) {
			// 타일 사이즈 지정
			_tileSize = tileSize;

			// Tile World가 사용할 충돌 모듈
			_simpleAABB = new SimpleAABB();

			// Tile World의 모든 강체들은 회전하지 않는다.
			Physics = new Physics();
			
			// FixedAngle 옵션은 Physics가 충돌 모듈을 선택할 때 사용되는 옵션인데
			// 임의로 충돌 모듈 선택 알고리즘을 지정해 줄 경우에는 신경쓰지 않아도 된다.
			Physics.FixedAngle = true;
			Physics.SELECT_COLLISION_CHECK_ALGORITHM = delegate (Collider c1, Collider c2) {
				return TileWorldCollisionModule;
			};
		}

		/// <summary>
		/// 타일 맵에서 타일 Behavior들을 읽어들인다.
		/// GameScreen에서 맵 파일을 읽으면 TileMap 형식의 데이터가 나온다.
		/// </summary>
		public void LoadTileMap(TileMap map) {
			for(int i=0; i<map.TileAt.GetLength(0); ++i) {
				for(int j=0; j<map.TileAt.GetLength(1); ++j) {
					TileConfig config = map.TileAt[i, j];

					if (config != null) {
						// Read tile configuration.
						string spriteName = config.SpriteName;
						bool collidable = config.Collidable;

						// Load Sprite from Content manager of current screen.
						Sprite tileSprite = new Sprite(CurrentScreen.Content.Load<Texture2D>(spriteName));

						// Make tile object
						// (0, 0) 위치의 타일이 화면 좌측 하단 모서리에 놓인다.
						int halfWidth = map.TileAt.GetLength(0) / 2;
						int halfHeight = map.TileAt.GetLength(1) / 2;
                        Tile tile = new Tile(tileSprite, TileSize, i - halfWidth, j - halfHeight);
						tile.Collidable = collidable;
						Instantiate(tile);
					}
				}
			}
		}
	}
}
