using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CatdogEngine
{
	public class CatdogApplication : ScreenSystem.ScreenManager
	{

		private bool _bufferSizeInitialized;
		private GameScreen _startScreen;

		private static GameWindow _windowConfig;

		#region Properties
		public string Title { get { return Window.Title; } set { Window.Title = value; } }
		public GameScreen StartScreen { set { _startScreen = value; } }
		public static int PreferredBackBufferWidth { get { return GraphicsDeviceManager.PreferredBackBufferWidth; } }
		public static int PreferredBackBufferHeight { get { return GraphicsDeviceManager.PreferredBackBufferHeight; } }
		public new static GraphicsDevice GraphicsDevice { get { return GraphicsDeviceManager.GraphicsDevice; } } 
		public static GameWindow WindowConfig { get { return _windowConfig; } }
		#endregion

		public CatdogApplication()
		{
			// static 변수에 윈도우 정보를 복사
			_windowConfig = Window;
		}

		protected override void Initialize()
		{
			base.Initialize();

			Debug.WriteLine("### Game Started!!! ###");
			Debug.WriteLine("### Window Title : " + Title + " ###");
			Debug.WriteLine("### Resolution : " + GraphicsDeviceManager.PreferredBackBufferWidth + " x " + GraphicsDeviceManager.PreferredBackBufferHeight + " ###");

			LogoScreen logoScreen = new LogoScreen();
			logoScreen.StartScreen = _startScreen;
			SetScreen(logoScreen);
		}

		/// <summary>
		/// 버퍼의 크기를 초기화한다. 한번 초기화하면 이후로는 변경이 불가능하다.
		/// 윈도우에 그리기 전에 버퍼에 비트맵을 그릴 때 그 가상의 버퍼 공간의 크기를 설정한다.
		/// 실제로는 조금 다르지만 윈도우의 사이즈를 설정한다고 생각해도 당장은 큰 문제가 없다.
		/// </summary>
		public void SetPreferredBackBufferSize(int width, int height)
		{
			if (_bufferSizeInitialized)
			{
				Debug.WriteLine("### CatdogEngineApplication : Buffer Size is already initialized. ###");
			}
			else
			{
				GraphicsDeviceManager.PreferredBackBufferWidth = width;
				GraphicsDeviceManager.PreferredBackBufferHeight = height;

				_bufferSizeInitialized = true;
			}
		}
	}
}
