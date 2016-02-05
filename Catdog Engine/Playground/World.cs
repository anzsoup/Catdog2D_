using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CatdogEngine.Playground
{
	/// <summary>
	/// 플레이 가능한 실제 게임 세계의 기본단위. 모든 Behavior들의 로직을 관리한다.
	/// </summary>
	public class World : InputListener
	{
		private List<Behavior> _behaviors;
		private List<Behavior> _newBehaviors;
		private List<Behavior> _deadBehaviors;
		private Camera _camera;

		private GameScreen _currentScreen;                      // 현재 속한 스크린

		private Vector2 _gravity;                               // 중력

		private TriggerManager _trigger;                        // 트리거

		private bool _isPaused;									// 일시정지

		#region Properties
		/// <summary>
		/// 새로운 카메라를 생성하고자 할 경우 SetCamera(Camera) 메소드 사용.
		/// </summary>
		public Camera Camera { get { return _camera; } }

		public GameScreen CurrentScreen { get { return _currentScreen; } set { _currentScreen = value; } }
		#endregion

		public World(GameScreen currentScreen)
		{
			_behaviors = new List<Behavior>();
			_newBehaviors = new List<Behavior>();
			_deadBehaviors = new List<Behavior>();
			_gravity = new Vector2(0, -9.81f);
			_trigger = new TriggerManager();
			SetCamera(new Camera());
			CurrentScreen = currentScreen;

			_isPaused = false;
		}

		/// <summary>
		/// Behavior를 World에 생성한다.
		/// </summary>
		public virtual void Instantiate(Behavior behavior)
		{
			// 매개변수가 Camera 일 경우 무시한다.
			if(behavior is Camera)
			{
				Debug.WriteLine("### World.Instantiate(Behavior) failed : Camera only can be instantiated by SetCamera(Camera) method. ###");
			}
			else
			{
				if (behavior != null)
				{
					behavior.World = this;

					// Start Behavior
					behavior.Start();

					// Initialize Components
					foreach (BehaviorComponent component in behavior.Components)
					{
						component.Initialize(this);
					}

					// Cache Behavior
					_newBehaviors.Add(behavior);
				}
			}
		}

		/// <summary>
		/// Behavior를 없앤다.
		/// </summary>
		public virtual void Destroy(Behavior behavior)
		{
			if(behavior != null)
			{
				_deadBehaviors.Add(behavior);
			}
		}

		public void SetCamera(Camera camera)
		{
			if (camera != null)
			{
				// Start Camera
				camera.Start();

				// Register Camera
				_camera = camera;
			}
		}

		public void Pause()
		{
			_isPaused = true;
		}

		public void Unpause()
		{
			_isPaused = false;
		}
		
		public virtual void Update(GameTime gameTime)
		{
			List<Location> locationBucket = new List<Location>();

			// Instantiate 된 Behavior들을 리스트에 추가
			foreach(Behavior behavior in _newBehaviors)
			{
				_behaviors.Add(behavior);
			}

			// Destroy 된 Behavior들을 리스트에서 제거
			foreach(Behavior behavior in _deadBehaviors)
			{
				if (_behaviors.Contains(behavior))
				{
					_behaviors.Remove(behavior);
				}
			}

			// 리스트 비우기
			_newBehaviors.Clear();
			_deadBehaviors.Clear();

			// Behavior 로직 진행
			if (!_isPaused)
			{

				foreach (Behavior behavior in _behaviors)
				{
					// Update Components.
					// It is followed by Behavior's Update Logic.
					foreach (BehaviorComponent component in behavior.Components)
					{
						component.Update(gameTime);
						if (component is Location)
						{
							locationBucket.Add((Location)component);
						}
					}

					// Update Behavior.
					Vector2 worldVelocity = (behavior.Transform.Right * behavior.Transform.Velocity.X) + (behavior.Transform.Up * behavior.Transform.Velocity.Y);
					behavior.Transform.Position += worldVelocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
					behavior.Update(gameTime);
				}

				// Listen Trigger Event
				Location[] allLocations = new Location[locationBucket.Count];
				locationBucket.CopyTo(allLocations);
				_trigger.CollisionCheck(allLocations);

				// Camera 로직 진행
				if (_camera != null) _camera.Update(gameTime);
			}
		}

		public virtual void Draw(GameTime gameTime)
		{
			// Component 그리기
			foreach (Behavior behavior in _behaviors)
			{
				// Draw Components.
				// Behaviors don't have Draw logic, but do Components have it.
				foreach (BehaviorComponent component in behavior.Components)
				{
					component.Draw(gameTime);
				}
			}
		}

		public void OnLeftMouseDown(int x, int y)
		{
			foreach(Behavior behavior in _behaviors)
			{
				if(!behavior.HasSeparateInputProcess) behavior.OnLeftMouseDown(x, y);
			}
		}

		public void OnLeftMouseUp(int x, int y)
		{
			foreach (Behavior behavior in _behaviors)
			{
				if (!behavior.HasSeparateInputProcess) behavior.OnLeftMouseUp(x, y);
			}
		}

		public void OnMouseMove(int x, int y)
		{
			foreach (Behavior behavior in _behaviors)
			{
				if (!behavior.HasSeparateInputProcess) behavior.OnMouseMove(x, y);
			}
		}

		public void OnKeyDown(Keys key)
		{
			foreach (Behavior behavior in _behaviors)
			{
				if (!behavior.HasSeparateInputProcess) behavior.OnKeyDown(key);
			}
		}

		public void OnKeyUp(Keys key)
		{
			foreach (Behavior behavior in _behaviors)
			{
				if (!behavior.HasSeparateInputProcess) behavior.OnKeyUp(key);
			}
		}
	}
}
