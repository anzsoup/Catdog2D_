
namespace SampleGame
{
	/// <summary>
	/// 게임 진행에 필요한 모든 데이터들을 저장한다. 싱글톤이다.
	/// </summary>
	public class GameData
	{
		#region Singleton
		private GameData()
		{
			_maxScore = 0;
		}
		private static GameData _instance;
		public static GameData Instance
		{
			get
			{
				if (_instance == null) _instance = new GameData();
				return _instance;
			}
		}
		#endregion

		private int _maxScore;

		#region Properties
		public int MaxScore { get { return _maxScore; } set { _maxScore = value; } }
		#endregion
	}
}
