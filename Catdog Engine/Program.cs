using System;

using CatdogEngine.ScreenSystem;

namespace CatdogEngine {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			// 게임의 몸체가 될 인스턴스 생성
			CatdogApplication application = new CatdogApplication();

			// 윈도우 타이틀
			application.Title = "Catdog Engine";

			// 버퍼의 너비와 높이 설정
			// 이후로는 변경이 불가능하다.
			application.SetPreferredBackBufferSize(800, 480);

			// 게임이 실행되면 가장 먼저 띠울 스크린 지정
			application.StartScreen = new WorldTestScreen();

            using (var game = application)
                game.Run();
        }
    }
#endif
}
