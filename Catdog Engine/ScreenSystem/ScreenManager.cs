using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace CatdogEngine.ScreenSystem {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ScreenManager : Game {
        private GameScreen _activeScreen;                // 현재 활성화 된 스크린
        private GameScreen _nextScreen;                  // 스크린 전환이 진행 중일 때, 곧 전환 될 스크린
		private SpriteBatch spriteBatch;

		// 어디서든 접근이 가능하도록 static으로 선언했다.
		// 초기화 시점은 Main에서 ScreenManager가 생성될 때이다.
		// 초기화 시점보다 이른 시기에 접근 시도가 발생하면 안된다.
		private static GraphicsDeviceManager _graphics;
        

        public ScreenManager() {
            _graphics = new GraphicsDeviceManager(this);
			_graphics.IsFullScreen = false;
			this.IsMouseVisible = true;

			// ContentManager 및 ResourceManager 초기화
			Content.RootDirectory = "Content";
			ResourceManager.Instance.Initialize(Content);

			SetScreen(new LogoScreen());
        }

		#region Properties
		public SpriteBatch SpriteBatch { get { return spriteBatch; } }
		public static GraphicsDeviceManager GraphicsDeviceManager { get { return _graphics; } }
		#endregion

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
			// Unload All of Contents had been loaded
			ResourceManager.Instance.Unload();						// Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
       
            if(_activeScreen == null && _nextScreen != null) {
                // Next Screen is now Active
                _activeScreen = _nextScreen;

                // Clear the Next Screen Reference
                _nextScreen = null;

                // Load Content
                _activeScreen.LoadContent();

                // Transitin Start
                _activeScreen.ScreenState = ScreenState.TransitionOn;
            }

            if (_nextScreen != null) _nextScreen.Update(gameTime);
            if (_activeScreen != null) _activeScreen.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

			SpriteBatch.Begin();
            if (_nextScreen != null) _nextScreen.Draw(gameTime);
            if (_activeScreen != null) _activeScreen.Draw(gameTime);
			SpriteBatch.End();
        }

        /// <summary>
        /// 스크린을 교체한다. 현재 스크린의 TransitionOff 이펙트가 진행된다.
        /// </summary>
        public void SetScreen(GameScreen nextScreen) {
            if(nextScreen == null) {
                Debug.WriteLine("### ScreenManager.SetScreen Failed : The argument can not be Null ###");
            }
            else {
                // Active Screen의 Transition이 끝나길 기다린다
                _nextScreen = nextScreen;

				_nextScreen.ScreenManager = this;

                // Active Screen은 Transition Off 시작
                if(_activeScreen != null) _activeScreen.ScreenState = ScreenState.TransitionOff;
            }
        }

        public void RemoveScreen(GameScreen screen) {
            if (screen == null)
                Debug.WriteLine("### ScreenManager.RemoveScreen Failed : The argument can not be Null ###");
            else {
                if(screen == _activeScreen) {
                    // Unload Content
                    _activeScreen.UnloadContent();

                    // Clear
                    _activeScreen = null;
                }
                else if(screen == _nextScreen) {
                    // Unload Content
                    _nextScreen.UnloadContent();

                    // Clear
                    _nextScreen = null;
                }
                else {
                    Debug.WriteLine("### ScreenManager.RemoveScreen Failed : This Screen does not exsist ###");
                }
            }
        }
    }
}
