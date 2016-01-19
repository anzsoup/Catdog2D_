using CatdogEngine.Playground.Object.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground {
	public class Collision {
		private Location _l1, _l2;              // 충돌한 두 로케이션

		public Collision(Location l1, Location l2) {
			_l1 = l1;
			_l2 = l2;
		}

		public override bool Equals(object obj) {
			if (obj is Collision) {
				Collision col = (Collision)obj;

				if (this.Location1 == col.Location1 && this.Location2 == col.Location2)
					return true;
				else if (this.Location1 == col.Location2 && this.Location2 == col.Location1)
					return true;
			}

			return false;
		}

		#region Properties
		public Location Location1 { get { return _l1; } }
		public Location Location2 { get { return _l2; } }
		#endregion
	}



	/// <summary>
	/// 로케이션 컴포넌트들의 충돌여부를 검사하고 OnTriggerEnter, OnTriggerExit 이벤트를 발생시킨다.
	/// </summary>
	public class TriggerManager {
		private List<Collision> _oldCollisions;

		public TriggerManager() {
			_oldCollisions = new List<Collision>();
		}

		/// <summary>
		/// 충돌검사. 월드가 모든 Behavior들의 모든 Location들을 수집하여 배열의 형태로 전달해 준다. 
		/// </summary>
		public void CollisionCheck(Location[] locations) {
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// 먼저 이전에 충돌했던 Location들에 대해서 충돌 검사를 수행한다.
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// 이번 스텝에서 충돌한 모든 Collsion들을 담아두었다가
			// 다음 스텝에 다시 검사하기 위하여 마지막에 Old Collision에 도로 담는다.
			List<Collision> newCollisions = new List<Collision>();

			foreach (Collision oldCollision in _oldCollisions) {
				Location l1 = oldCollision.Location1;
				Location l2 = oldCollision.Location2;

				// 충돌 검사
				Collision collision = CollisionCheck(l1, l2);

				// 충돌이 발생했다면
				if (collision != null) {
					newCollisions.Add(collision);
				}
				// 충돌이 발생하지 않았다면
				else {
					// Call back OnTriggerExit
					if (l1 != null && l2 != null) {
						if(l1.ON_TRIGGER_EXIT != null) l1.ON_TRIGGER_EXIT(l2);
						if(l2.ON_TRIGGER_EXIT != null) l2.ON_TRIGGER_EXIT(l1);
						l1.Owner.OnTriggerExit(l1, l2);
						l2.Owner.OnTriggerExit(l2, l1);
					}
				}
			}

			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// 이제 월드에서 받은 Location들의 충돌 검사를 수행한다.
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// 충돌 가능성이 있는 Location만 추출하는 알고리즘을 추가할 수 있도록 해 두었다.
			Location[] optimalLocationSet = ExtractOptimalLocationSet(locations);

			// 모든 Location 끼리 한번씩 검사
			for (int i = 0; i < optimalLocationSet.Length; ++i) {
				for (int j = i + 1; j < optimalLocationSet.Length; ++j) {
					// 이미 검사한 Location쌍이면 충돌검사를 하지 않는다.
					bool isAlreadyChecked = false;
					Collision test = new Collision(optimalLocationSet[i], optimalLocationSet[j]);
					foreach (Collision oldCollision in _oldCollisions) {
						if (test.Equals(oldCollision)) {
							isAlreadyChecked = true;
							break;
						}
					}

					if (isAlreadyChecked) continue;
					
					// 충돌 검사
					Collision collision = CollisionCheck(optimalLocationSet[i], optimalLocationSet[j]);

					// 충돌이 발생했다면
					if (collision != null) {
						// Call back OnTriggerEnter.
						if(collision.Location1.ON_TRIGGER_ENTER != null) collision.Location1.ON_TRIGGER_ENTER(collision.Location2);
						if(collision.Location2.ON_TRIGGER_ENTER != null) collision.Location2.ON_TRIGGER_ENTER(collision.Location1);
						collision.Location1.Owner.OnTriggerEnter(collision.Location1, collision.Location2);
						collision.Location2.Owner.OnTriggerEnter(collision.Location2, collision.Location1);

			//////////////////////////////////////////////////////////////
			// 캐싱
			//////////////////////////////////////////////////////////////
						newCollisions.Add(collision);
					}
				}
			}

			// 아직 충돌중이었던 충돌들을 다시 캐시에 넣는다.
			_oldCollisions.Clear();
			foreach (Collision notExit in newCollisions) {
				// 중복이 발생하지 않도록 보장되므로 중복검사를 하지 않는다.
				_oldCollisions.Add(notExit);
			}

			newCollisions.Clear();
		}

		/// <summary>
		/// 충돌 가능성이 있는 로케이션만 추출하는 알고리즘
		/// </summary>
		private Location[] ExtractOptimalLocationSet(Location[] locations) {
			return locations;
		}

		/// <summary>
		/// 충돌검사
		/// </summary>
		private Collision CollisionCheck(Location l1, Location l2) {
			float left1 = l1.WorldPosition.X;
			float right1 = l1.WorldPosition.X + l1.Width;
			float top1 = l1.WorldPosition.Y;
			float bottom1 = l1.WorldPosition.Y - l1.Height;
			float left2 = l2.WorldPosition.X;
			float right2 = l2.WorldPosition.X + l2.Width;
			float top2 = l2.WorldPosition.Y;
			float bottom2 = l2.WorldPosition.Y - l2.Height;

			if (left1 > right2 || right1 < left2) return null;
			if (top1 < bottom2 || bottom1 > top2) return null;

			return new Collision(l1, l2);
		}
	}
}
