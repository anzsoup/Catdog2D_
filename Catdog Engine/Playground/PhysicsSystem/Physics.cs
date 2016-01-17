using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground.PhysicsSystem {
	public class Collision {
		private Collider _c1, _c2;              // 두 충돌체

		public Collision(Collider c1, Collider c2) {
			_c1 = c1;
			_c2 = c2;
		}

		public override bool Equals(object obj) {
			if(obj is Collision) {
				Collision col = (Collision)obj;

				if (this.Collider1 == col.Collider1 && this.Collider2 == col.Collider2)
					return true;
				else if (this.Collider1 == col.Collider2 && this.Collider2 == col.Collider1)
					return true;
			}

			return false;
		}

		#region Properties
		public Collider Collider1 { get { return _c1; } }
		public Collider Collider2 { get { return _c2; } }
		#endregion
	}

	public static class CollisionCheckAlgorithmPack {
		public static Collision CollisionBoxtoBox(Collider c1, Collider c2) {
			return null;
		}
	}

	/// <summary>
	/// 두 충돌체를 보고 최적의 충돌 알고리즘을 선택하는 알고리즘
	/// 임의로 지정해 주지 않을 경우 Physics의 기본 알고리즘(<see cref="Physics.SelectAlgorithm(Collider, Collider)"/>)이 사용된다.
	/// </summary>
	/// <returns>선택 된 충돌 알고리즘</returns>
	public delegate CollisionModule PHYSICS__SELECT_COLLISION_CHECK_ALGORITHM(Collider c1, Collider c2);





	public class Physics {
		private List<Collision> _oldCollisions;                             // 이전에 일어난 충돌들

		private bool _fixedAngle;											// 회전을 고려할 지 여부. true이면 모든 Behavior의 회전이 무시된다.

		private readonly float COR;                                         // 반발계수

		// 충돌 모듈 선택 알고리즘
		private PHYSICS__SELECT_COLLISION_CHECK_ALGORITHM _selectCollisionCheckAlgorithm;

		// 충돌 모듈
		private readonly CollisionModule SIMPLE_AABB;

		#region Properties
		public bool FixedAngle { get { return _fixedAngle; } set { _fixedAngle = value; } }

		/// <summary>
		/// 두 충돌체를 보고 최적의 충돌 알고리즘을 선택하는 알고리즘
		/// 임의로 지정해 주지 않을 경우 Physics의 기본 알고리즘(<see cref="Physics.SelectAlgorithm(Collider, Collider)"/>)이 사용된다.
		/// </summary>
		public PHYSICS__SELECT_COLLISION_CHECK_ALGORITHM SELECT_COLLISION_CHECK_ALGORITHM { get { return _selectCollisionCheckAlgorithm; }
			set {
				if (value == null) _selectCollisionCheckAlgorithm = SelectAlgorithm;
				else _selectCollisionCheckAlgorithm = value;
			}
		}
		#endregion

		public Physics() {
			_oldCollisions = new List<Collision>();
			COR = 0.2f;
			SIMPLE_AABB = new SimpleAABB();
			SELECT_COLLISION_CHECK_ALGORITHM = SelectAlgorithm;
		}

		/// <summary>
		/// 충돌검사. 월드가 모든 Behavior들의 모든 Collider들을 수집하여 배열의 형태로 전달해 준다. 
		/// </summary>
		public void CollisionCheck(Collider[] colliders) {
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// 먼저 이전에 충돌했던 Collider들에 대해서 충돌 검사를 수행한다.
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// Old Collision 중에서 아직 충돌중인 Collision들만 모아두었다가
			// 다음 스텝에 다시 검사하기 위하여 마지막에 Old Collision에 도로 담는다.
			List<Collision> collisionNotExit = new List<Collision>();

			foreach(Collision oldCollision in _oldCollisions) {
				Collider c1 = oldCollision.Collider1;
				Collider c2 = oldCollision.Collider2;

				// 알고리즘 선택
				CollisionModule module = SELECT_COLLISION_CHECK_ALGORITHM(c1, c2);

				// 충돌 검사
				Collision collision = module.COLLISION_CHECK(c1, c2);

				// 충돌이 발생했다면
				if(collision != null) {
					// 둘 다 Trigger가 아니면 물리충돌
					// 하나의 충돌에 대해 APPLY_IMPULSE가 두번 이상 수행되는 이런 일은 CollisionModule이 책임지고 발생하지 않도록 해야한다.
					if(!c1.IsTrigger && !c2.IsTrigger) {
						module.APPLY_IMPULSE(collision);
					}
					collisionNotExit.Add(collision);
				}
				// 충돌이 발생하지 않았다면
				else {
					if (!c1.IsTrigger && !c2.IsTrigger) {
						// Call back OnCollisionExit
						c1.Owner.OnCollisionExit(oldCollision);
						c2.Owner.OnCollisionExit(oldCollision);
					}
					else {
						if (c1.IsTrigger) {
							// Call back OnTriggerExit.
							c1.Owner.OnTriggerExit(oldCollision);
						}

						if (c2.IsTrigger) {
							// Call back OnTriggerExit.
							c2.Owner.OnTriggerExit(oldCollision);
						}
					}
				}
			}

			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// 이제 월드에서 받은 Collider들의 충돌 검사를 수행한다.
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// 충돌 가능성이 있는 Collider만 추출하는 알고리즘을 추가할 수 있도록 해 두었다.
			Collider[] optimalColliderSet = ExtractOptimalColliderSet(colliders);
			
			// 모든 Collider 끼리 한번씩 검사
			for(int i=0; i<optimalColliderSet.Length; ++i) {
				for(int j=i+1; j<optimalColliderSet.Length; ++j) {
					// 이미 검사한 Collider쌍이면 충돌검사를 하지 않는다.
					bool isAlreadyChecked = false;
					Collision test = new Collision(optimalColliderSet[i], optimalColliderSet[j]);
					foreach(Collision oldCollision in _oldCollisions) {
						if (test.Equals(oldCollision)) {
							isAlreadyChecked = true;
							break;
						}
					}

					if (isAlreadyChecked) continue;

					// 알고리즘 선택
					CollisionModule module = SelectAlgorithm(optimalColliderSet[i], optimalColliderSet[j]);

					// 충돌 검사
					Collision collision = module.COLLISION_CHECK(optimalColliderSet[i], optimalColliderSet[j]);

					// 충돌이 발생했다면
					if(collision != null) {
						// 둘 다 Trigger가 아닐 경우 물리충돌
						if(!collision.Collider1.IsTrigger && !collision.Collider2.IsTrigger) {
							Rigidbody rb1 = (Rigidbody)collision.Collider1.Owner.GetComponent<Rigidbody>();
							Rigidbody rb2 = (Rigidbody)collision.Collider2.Owner.GetComponent<Rigidbody>();

							// 둘 중에 하나라도 Rigidbody가 없으면 물리충돌을 할 수 없다.
							if(rb1 != null && rb2 != null) {
								// 충격을 가한다.
								module.APPLY_IMPULSE(collision);

								// Old Collision들의 검사가 이미 끝났으므로 충돌이 발생했다면 그 충돌은 항상 새로운 충돌이다.
								// 새로운 충돌이면
								// Call back OnCollisionEnter.
								collision.Collider1.Owner.OnCollisionEnter(collision);
								collision.Collider2.Owner.OnCollisionEnter(collision);
							}
						}
						// 아니면 Trigger 발동
						else {
							if(collision.Collider1.IsTrigger) {
								// Call back OnTriggerEnter.
								collision.Collider1.Owner.OnTriggerEnter(collision);
							}

							if(collision.Collider2.IsTrigger) {
								// Call back OnTriggerEnter.
								collision.Collider2.Owner.OnTriggerEnter(collision);
							}
						}

			//////////////////////////////////////////////////////////////
			// 캐싱
			//////////////////////////////////////////////////////////////
						_oldCollisions.Add(collision);
					}
				}
			}

			// 아직 충돌중이었던 충돌들을 다시 캐시에 넣는다.
			foreach(Collision notExit in collisionNotExit) {
				// 중복이 발생하지 않도록 보장되므로 중복검사를 하지 않는다.
				_oldCollisions.Add(notExit);
			}
		}

		/// <summary>
		/// 충돌 가능성이 있는 Collider를 추출하는 최적화 알고리즘.
		/// </summary>
		private Collider[] ExtractOptimalColliderSet(Collider[] colliders) {
			return colliders;
		}

		/// <summary>
		/// 두 Collider의 충돌검사를 위해 필요한 최적의 알고리즘을 선택한다.
		/// </summary>
		private CollisionModule SelectAlgorithm(Collider c1, Collider c2) {
			if (FixedAngle) {
				if (c1 is BoxCollider && c2 is BoxCollider) return SIMPLE_AABB;
				else return null;
			}
			else {
				return null;
			}
		}

		
	}
}
