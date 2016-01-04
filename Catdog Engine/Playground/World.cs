using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CatdogEngine.Playground {
	/// <summary>
	/// 플레이 가능한 실제 게임 세계의 기본단위. 모든 Behavior들의 로직을 관리한다.
	/// </summary>
	public abstract class World {
		private List<Behavior> _behaviors;
		private Camera _camera;

		#region Properties
		/// <summary>
		/// 새로운 카메라를 생성하고자 할 경우 SetCamera(Camera) 메소드 사용.
		/// </summary>
		public Camera Camera { get { return _camera; } }
		#endregion

		public World() {
			_behaviors = new List<Behavior>();
		}

		/// <summary>
		/// 스크린의 초기화 과정에서 반드시 한 번 호출돼야 한다.
		/// </summary>
		public virtual void Initialize() {
			SetCamera(new Camera());
		}

		/// <summary>
		/// Behavior를 World에 생성한다.
		/// </summary>
		public virtual void Instantiate(Behavior behavior) {
			// 매개변수가 Camera 일 경우 무시한다.
			if(behavior is Camera) {
				Debug.WriteLine("### World.Instantiate(Behavior) failed : Camera only can be instantiated by SetCamera(Camera) method. ###");
			}
			else {
				if (behavior != null) {
					// Start Behavior
					behavior.Start();

					// Initialize Components
					foreach (BehaviorComponent component in behavior.Components) {
						component.Initialize(this);
					}

					// Register Behavior
					_behaviors.Add(behavior);
				}
			}
		}

		public void SetCamera(Camera camera) {
			if (camera != null) {
				// Start Camera
				camera.Start();

				// Register Camera
				_camera = camera;
			}
		}
		
		/// <summary>
		/// 스크린의 Update 로직에 반드시 포함되어야 한다.
		/// </summary>
		public virtual void Update(GameTime gameTime) {
			// Behavior 로직 진행
			foreach(Behavior behavior in _behaviors) {
				// Update Components.
				// It is followed by Behavior's Update Logic.
				foreach(BehaviorComponent component in behavior.Components) {
					component.Update(gameTime);
				}

				// Update Behavior.
				behavior.Update(gameTime);
			}

			// Camera 로직 진행
			if (_camera != null) _camera.Update(gameTime);
		}

		/// <summary>
		/// 스크린의 Draw 로직에 반드시 포함되어야 한다.
		/// </summary>
		public virtual void Draw(GameTime gameTime) {
			// Component 그리기
			foreach (Behavior behavior in _behaviors) {
				// Draw Components.
				// Behaviors don't have Draw logic, but do Components have it.
				foreach (BehaviorComponent component in behavior.Components) {
					component.Draw(gameTime);
				}
			}
		}
	}
}
