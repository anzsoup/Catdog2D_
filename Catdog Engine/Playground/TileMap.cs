using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground {
	public class TileConfig
	{
		private string _spriteName;
		private bool _collidable;

		public TileConfig(string spriteName, bool collidable = true)
		{
			_spriteName = spriteName;
			_collidable = collidable;
		}

		#region Properties
		public string SpriteName { get { return _spriteName; } }
		public bool Collidable { get { return _collidable; } }
		#endregion
	}


	/// <summary>
	/// TileWorld에 배치할 Tile들을 저장하는 자료형
	/// </summary>
	public class TileMap
	{
		private TileConfig[,] _map;

		public TileMap(int width = 20, int height = 12)
		{
			_map = new TileConfig[width, height];
		}

		#region Properties
		public TileConfig[,] TileAt { get { return _map; } }
		#endregion
	}
}
