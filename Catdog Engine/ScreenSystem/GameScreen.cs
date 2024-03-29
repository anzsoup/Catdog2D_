﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CatdogEngine.UI;
using CatdogEngine.Playground;

namespace CatdogEngine.ScreenSystem
{
    /// <summary>
    /// GameScreen의 현재 상태를 표현하는 열거형
    /// </summary>
    public enum ScreenState
	{
        Waiting,
        TransitionOn,
        TransitionOff,
        Active,
		Dead
    }




	/// <summary>
	/// GameScreen이 전환 되는 이펙트를 진행시키는 메소드를 받는다.
	/// 전달받은 메소드는 false를 반환할 때까지 Update 로직에서 매 프레임당 한 번씩 호출된다.
	/// 현재 상태가 TransitionOn인지 TransitionOff인지 알기 위해서는 direction 인수의 값을 확인하면 된다.
	/// </summary>
	/// <param name="gameTime">Delta Time</param>
	/// <param name="transitionTime">화면전환효과 지속시간</param>
	/// <param name="direction">-1 : TransitionOff, 1 : TransitionOn</param>
	/// <returns>true : 스크린 전환이 진행중, false : 스크린 전환이 완료됨</returns>
	public delegate bool SCREEN_TRANSITION_EFFECT(GameTime gameTime, TimeSpan transitionTime, int direction);




	/// <summary>
	/// 몇 가지 기본 제공 화면 전환 효과
	/// </summary>
	public static class ScreenTransitionEffectPackage
	{

		#region Fields
		private static float _transitionPosition;
		#endregion

		#region Properties
		public static float FadeAlpha { get { return 1f - _transitionPosition; } }
		#endregion

		#region Functions
		/// <summary>
		/// ScreenManager의 Draw 로직에서 호출되는 화면 전환 효과 처리를 위한 메소드.
		/// </summary>
		public static void PostProcess()
		{
			// For Fade Effect. I don't like this :(
			// I will find better solution soon.
			Color[] colors = new Color[] { Color.Black };
			Texture2D texture = new Texture2D(CatdogApplication.GraphicsDevice, 1, 1);
			texture.SetData<Color>(colors);
			ScreenManager.SpriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(0, 0, CatdogApplication.WindowConfig.ClientBounds.Width, CatdogApplication.WindowConfig.ClientBounds.Height),
				color: new Color(Color.Black, ScreenTransitionEffectPackage.FadeAlpha)
				);
		}
		#endregion


		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// 화면 전환 효과 모음
		///////////////////////////////////////////////////////////////////////////////////////////////////////////

		// 1. 효과 없음
		public static SCREEN_TRANSITION_EFFECT None =
			delegate (GameTime gameTime, TimeSpan transitionTime, int direction) 
			{
				return false;
			};

		// 2. Fade in & out
		public static SCREEN_TRANSITION_EFFECT Fading =
			delegate (GameTime gameTime, TimeSpan transitionTime, int direction) 
			{
				// How much should we move by?
				float transitionDelta;

				if (transitionTime == TimeSpan.Zero)
					transitionDelta = 1f;
				else
					transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / transitionTime.TotalMilliseconds);

				// Update the transition position.
				// direction == 1; Fade out
				// direction == -1; Fade in
				_transitionPosition += transitionDelta * direction;

				// Did we reach the end of the transition?
				if (((direction < 0) && (_transitionPosition <= 0)) || ((direction > 0) && (_transitionPosition >= 1)))
				{
					_transitionPosition = MathHelper.Clamp(_transitionPosition, 0, 1);
					return false;
				}

				// Otherwise we are still busy transitioning.
				return true;
			};
	}




	/// <summary>
	/// Update 와 Draw 로직을 포함하는 기본 단위 레이어. 모든 Screen은 이 클래스를 상속한다.
	/// 한 스크린 안에서 로드 된 리소스는 해당 스크린이 죽을 때 함께 해제된다.
	/// 여러 스크린이 공유하는 리소스는 ScreenManager에서 로드해야 한다.
	/// </summary>
	public abstract class GameScreen : InputListener
	{

        private SCREEN_TRANSITION_EFFECT Screen_Transition_Effect;
        private ScreenState _screenState;
        private ScreenManager _screenManager;

		private TimeSpan _transitionTime;

		// 스크린마다 ContentManager를 하나씩 갖는다.
		// 스크린 내에서의 모든 리소스 할당과 해제는 이 ContentManager를 통해 이루어진다.
		// 스크린의 수명이 다하면 모든 리소스를 해제한다.
		protected ContentManager _content;

		// 캔버스와 월드
		private Canvas _canvas;
		private World _world;

        public GameScreen()
		{
            _screenState = ScreenState.Waiting;
			// 화면전환효과 지속시간 초기값 : 1초
			TransitionTime = new TimeSpan(0, 0, 1);
			// 화면전환효과 초기값 : 페이드
			Screen_Transition_Effect = ScreenTransitionEffectPackage.Fading;
        }

        #region Properties
        public ScreenState ScreenState { get { return _screenState; } set { _screenState = value; } }

        public ScreenManager ScreenManager { get { return _screenManager; } set { _screenManager = value; } }

        public TimeSpan TransitionTime { get { return _transitionTime; } protected set { _transitionTime = value; } }

		public SCREEN_TRANSITION_EFFECT ScreenTransitionEffect { get; set; }

		public ContentManager Content { get { return _content; } private set { _content = value; } }
		#endregion

		public void SetWorld(World world)
		{
			world.CurrentScreen = this;
			_world = world;
		}

		public void SetCanvas(Canvas canvas)
		{
			_canvas = canvas;
		}

        /// <summary>
        /// GameScreen이 생성된 후 리소스를 할당하는 타이밍에 한 번 호출된다.
        /// 리소스 할당 작업을 여기서 한다.
        /// </summary>
        public virtual void LoadContent()
		{
			// 스크린이 활성화 되고 리소스 할당 및 초기화가 이루어질 때 Content Manager를 생성한다.
			Content = new ContentManager(ScreenManager.Services, ScreenManager.Content.RootDirectory);

			// Input Listener 등록
			InputManager.SetListener(this);
		}

        /// <summary>
        /// GameScreen이 소멸하기 직전 리소스를 해제하는 타이밍에 한 번 호출된다.
        /// 리소스 해제 작업을 여기서 한다.
        /// </summary>
        public virtual void UnloadContent()
		{
			// Content Manager clears the resources loaded by this screen.
			Content.Unload();

			// Input Listener 해제
			InputManager.RemoveListener(this);
		}

        /// <summary>
        /// 게임 로직을 진행시킨다.
        /// GameScreen의 현재 상태와 상관없이 무조건 호출된다.
        /// </summary>
        /// <param name="gameTime">Delta Time</param>
        public virtual void Update(GameTime gameTime)
		{ 

            // 스크린 전환 이펙트 처리
            if(ScreenState == ScreenState.TransitionOff)
			{
                ScreenState = Screen_Transition_Effect(gameTime, TransitionTime, -1) ? ScreenState.TransitionOff : ScreenState.Dead;
            }
            else if(ScreenState == ScreenState.TransitionOn)
			{
                ScreenState = Screen_Transition_Effect(gameTime, TransitionTime, 1) ? ScreenState.TransitionOn : ScreenState.Active;
            }

			if(ScreenState == ScreenState.Dead)
			{
				ScreenManager.RemoveScreen(this);
			}

			// Update Canvas & World
			if (_world != null) _world.Update(gameTime);
			if (_canvas != null) _canvas.Update(gameTime);
		}

        /// <summary>
        /// GameScreen을 그려야 할 때 호출 된다.
        /// 그리기 작업을 여기서 한다.
        /// </summary>
		/// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="gameTime">Delta Time</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }

		/// <summary>
		/// Canvas를 그려야 할 때 ScreenManager가 호출한다. Canvas는 가장 마지막에 그려진다.
		/// </summary>
		public void DrawCanvas(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(_canvas != null) _canvas.Draw(spriteBatch, gameTime);
		}

		/// <summary>
		/// World의 Draw로직을 ScreenManager가 호출한다.
		/// </summary>
		public void DrawWorld(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(_world != null) _world.Draw(spriteBatch, gameTime);
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Input Event
		// 필요한 함수를 재정의 하여 사용
		///////////////////////////////////////////////////////////////////////////////////////////////////////////

		public virtual void OnLeftMouseDown(int x, int y)
		{
			if(_world != null) _world.OnLeftMouseDown(x, y);
			if(_canvas != null) _canvas.OnLeftMouseDown(x, y);
		}

		public virtual void OnLeftMouseUp(int x, int y)
		{
			if (_world != null) _world.OnLeftMouseUp(x, y);
			if (_canvas != null) _canvas.OnLeftMouseUp(x, y);
		}

		public virtual void OnMouseMove(int x, int y)
		{
			if (_world != null) _world.OnMouseMove(x, y);
			if (_canvas != null) _canvas.OnMouseMove(x, y);
		}

		public virtual void OnKeyDown(Keys key)
		{
			if (_world != null) _world.OnKeyDown(key);
			if (_canvas != null) _canvas.OnKeyDown(key);
		}

		public virtual void OnKeyUp(Keys key)
		{
			if (_world != null) _world.OnKeyUp(key);
			if (_canvas != null) _canvas.OnKeyUp(key);
		}
	}
}
