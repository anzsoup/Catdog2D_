using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CatdogEngine.ScreenSystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ScreenManager : Game
	{
        private GameScreen _activeScreen;					// 현재 활성화 된 스크린
        private GameScreen _nextScreen;                     // 스크린 전환이 진행 중일 때, 곧 전환 될 스크린

		private int _oldWindowWidth, _oldWindowHeight;

		// 어디서든 접근이 가능하도록 static으로 선언했다.
		// 초기화 시점보다 이른 시기에 접근 시도가 발생하면 안된다.
		private static SpriteBatch _spriteBatch;
		private static GameWindow _windowConfig;
		private static GraphicsDeviceManager _graphics;
		private static bool _isWindowSizeChanged;                  // 윈도우 사이즈가 변했는가


		public ScreenManager()
		{
            _graphics = new GraphicsDeviceManager(this);
			
			// static 변수에 윈도우 정보를 복사
			_windowConfig = this.Window;

			// 윈도우 설정 초기화
			_graphics.IsFullScreen = false;
			this.Window.AllowUserResizing = false;
			this.Window.Title = "Catdog Engine";
			this.IsMouseVisible = true;

			_oldWindowWidth = this.Window.ClientBounds.Width;
			_oldWindowHeight = this.Window.ClientBounds.Height;

			Content.RootDirectory = "Content";

			SetScreen(new LogoScreen());
        }

		#region Properties
		public static SpriteBatch SpriteBatch { get { return _spriteBatch; } }
		public static GameWindow WindowConfig { get { return _windowConfig; } }
		public static GraphicsDeviceManager GraphicsDeviceManager { get { return _graphics; } }
		public static bool IsWindowSizeChanged { get { return _isWindowSizeChanged; } }
		#endregion

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
		{
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			// Unload All of Contents had been loaded
			Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
		{
			/*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
			*/

            // TODO: Add your update logic here

            base.Update(gameTime);

			// 윈도우 사이즈가 변했는지 검사
			int windowWidth = this.Window.ClientBounds.Width;
			int windowHeight = this.Window.ClientBounds.Height;
			if(_oldWindowWidth != windowWidth || _oldWindowHeight != windowHeight)
			{
				_isWindowSizeChanged = true;
				_oldWindowWidth = windowWidth;
				_oldWindowHeight = windowHeight;
			}
			else
			{
				_isWindowSizeChanged = false;
			}

			// InputManager 작동
			InputManager.Update();
       
            if(_activeScreen == null && _nextScreen != null)
			{
                // Next Screen is now Active
                _activeScreen = _nextScreen;

                // Clear the Next Screen Reference
                _nextScreen = null;

                // Load Content
                _activeScreen.LoadContent();

                // Transitin Start
                _activeScreen.ScreenState = ScreenState.TransitionOn;

				// Resister Input Listener
				InputManager.SetListener(_activeScreen);
            }

            if (_nextScreen != null) _nextScreen.Update(gameTime);
            if (_activeScreen != null) _activeScreen.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
		{
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

			SpriteBatch.Begin();
            if (_nextScreen != null) _nextScreen.Draw(gameTime);
            if (_activeScreen != null) _activeScreen.Draw(gameTime);

			// 화면 효과 처리
			ScreenTransitionEffectPackage.PostProcess();

			SpriteBatch.End();
        }

        /// <summary>
        /// 스크린을 교체한다. 현재 스크린의 TransitionOff 이펙트가 진행된다.
        /// </summary>
        public void SetScreen(GameScreen nextScreen)
		{
            if(nextScreen == null)
			{
                Debug.WriteLine("### ScreenManager.SetScreen Failed : The argument can not be Null ###");
            }
            else
			{
                // Active Screen의 Transition이 끝나길 기다린다
                _nextScreen = nextScreen;

				_nextScreen.ScreenManager = this;

                // Active Screen은 Transition Off 시작
                if(_activeScreen != null) _activeScreen.ScreenState = ScreenState.TransitionOff;
            }
        }

        public void RemoveScreen(GameScreen screen)
		{
            if (screen == null)
                Debug.WriteLine("### ScreenManager.RemoveScreen Failed : The argument can not be Null ###");
            else
			{
                if(screen == _activeScreen)
				{
                    // Unload Content
                    _activeScreen.UnloadContent();

					// End Input Listening
					InputManager.RemoveListener(screen);

                    // Clear
                    _activeScreen = null;
                }
                else if(screen == _nextScreen)
				{
                    // Unload Content
                    _nextScreen.UnloadContent();

                    // Clear
                    _nextScreen = null;
                }
                else
				{
                    Debug.WriteLine("### ScreenManager.RemoveScreen Failed : This Screen does not exsist ###");
                }
            }
        }
    }
}
