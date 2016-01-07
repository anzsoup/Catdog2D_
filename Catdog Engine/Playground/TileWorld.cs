﻿using CatdogEngine.Graphics;
using CatdogEngine.Playground.Object;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatdogEngine.ScreenSystem;
using CatdogEngine.Playground.Object.Component;

namespace CatdogEngine.Playground {
	public class TileWorld : World {
		private int _tileSize;                                      // 타일들의 사이즈. 불변하는 값.

		#region Properties
		public int TileSize { get { return _tileSize; } }
		#endregion

		public TileWorld(GameScreen currentScreen, int tileSize = 40) : base(currentScreen) {
			_tileSize = tileSize;
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
