using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatdogEngine.ScreenSystem {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ScreenManager : Game {
        private GameScreen _activeScreen;                // 현재 활성화 된 스크린
        private GameScreen _nextScreen;                  // 스크린 전환이 진행 중일 때, 곧 전환 될 스크린
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ScreenManager() {
            graphics = new GraphicsDeviceManager(this);
			graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";

			//SetScreen(new LogoScreen());
        }

        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

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

            if (_nextScreen != null) _nextScreen.Draw(gameTime);
            if (_activeScreen != null) _activeScreen.Draw(gameTime);
        }

        /// <summary>
        /// 스크린을 교체한다. 현재 스크린의 TransitionOff 이펙트가 진행된다.
        /// </summary>
        protected void SetScreen(GameScreen nextScreen) {
            if(nextScreen == null) {
                Console.WriteLine("ScreenManager.SetScreen Failed : The argument can not be Null");
            }
            else {
                _nextScreen = nextScreen;
				_nextScreen.ScreenManager = this;
				_nextScreen.LoadContent();
                _activeScreen.ScreenState = ScreenState.TransitionOff;
            }
        }

		public void RemoveScreen(GameScreen screen) {
			if(screen == _activeScreen) {
				_activeScreen.UnloadContent();
				_activeScreen = null;
			}
			else if(screen == _nextScreen) {
				_nextScreen.UnloadContent();
				_nextScreen = null;
			}
			else {
				if(screen == null) {
					Console.WriteLine("ScreenManager.RemoveScreen : The argument can not be Null");
				}
				else {
					Console.WriteLine("ScreenManager.RemoveScreen : this screen is not exsist");
				}
			}
		}
    }
}
