using CatdogEngine.Graphics;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;

namespace CatdogEngine.Playground.Object {
	/// <summary>
	/// TileWorld의 지형을 구성하는 기본요소. 차지하는 영역은 항상 정사각형이다.
	/// </summary>
	public class Tile : Behavior {
		private int _size;									// 가로 세로 길이. 월드가 지정해 준다. 기본은 40
		private int _gridX, _gridY;                         // 격자 위치. x, y값은 항상 정수 값을 갖는다.

		// Components
		private SpriteRenderer _renderer;
		private Rigidbody _rigidBody;
		private BoxCollider _collider;

		public Tile(Sprite sprite, int size = 40, int x = 0, int y = 0) {
			if (size <= 0) _size = 40;
			else _size = size;
			X = x;
			Y = y;

			_renderer = new SpriteRenderer(sprite);
			_rigidBody = new Rigidbody();
			_collider = new BoxCollider();

			_rigidBody.IsFixed = true;
			
			AddComponent(_renderer);
			AddComponent(_rigidBody);
			AddComponent(_collider);
		}

		#region Properties
		public int X { get { return _gridX; }
			set {
				_gridX = value;
				Transform.Position = new Vector2(_gridX * _size, Transform.Position.Y);
			}
		}
		public int Y { get { return _gridY; }
			set {
				_gridY = value;
				Transform.Position = new Vector2(Transform.Position.X, (_gridY + 1) * _size);
			}
		}

		/// <summary>
		/// 충돌 가능성 여부.
		/// Collidable이 false이면 충돌하지 않는다. 내부적으로는 Collider가 Trigger로 설정된다.
		/// </summary>
		public bool Collidable { get { return !_collider.IsTrigger; } set { _collider.IsTrigger = !value; } }
		#endregion

		public override void Start() {
			
		}

		public override void Update(GameTime gameTime) {
			
		}
	}
}
