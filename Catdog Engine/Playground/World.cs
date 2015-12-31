using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground {
	/// <summary>
	/// 플레이 가능한 실제 게임 세계의 기본단위.
	/// Behavior들로 이루어지며, 모든 Behavior들의 로직을 관리한다.
	/// </summary>
	public abstract class World {
		private List<Behavior> _behaviors;
		private Camera _camera;

		public World() {
			_behaviors = new List<Behavior>();
			_camera = new Camera();
		}

		/// <summary>
		/// 스크린의 초기화 과정에서 반드시 한 번 호출돼야 한다.
		/// </summary>
		public virtual void Initialize() {

		}

		public virtual void Instantiate(Behavior behavior) {
			// Start Behavior
			behavior.Start();

			// Initialize Components
			foreach(BehaviorComponent component in behavior.Components) {
				component.Initialize(this);
			}

			// Register Behavior
			_behaviors.Add(behavior);
		}
		
		/// <summary>
		/// 스크린의 Update 로직에 반드시 포함되어야 한다.
		/// </summary>
		public virtual void Update(GameTime gameTime) {
			// Behavior 로직 진행
			foreach(Behavior behavior in _behaviors) {
				// 각 Behavior 에게 스크린의 UpdateTime을 넘겨준다.
				behavior.UpdateTime = gameTime;

				// Update Components.
				// It is followed by Behavior's Update Logic.
				foreach(BehaviorComponent component in behavior.Components) {
					component.Update();
				}

				// Update Behavior.
				behavior.Update();
			}
		}

		/// <summary>
		/// 스크린의 Draw 로직에 반드시 포함되어야 한다.
		/// </summary>
		public virtual void Draw(GameTime gameTime) {
			// Behavior 로직 진행
			foreach (Behavior behavior in _behaviors) {
				// 각 Behavior 에게 스크린의 DrawTime을 넘겨준다.
				behavior.DrawTime = gameTime;

				// Draw Components.
				// Behaviors don't have Draw logic, but do Components have it.
				foreach (BehaviorComponent component in behavior.Components) {
					component.Draw();
				}
			}
		}
	}
}
