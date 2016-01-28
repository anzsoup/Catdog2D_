
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

		private float _maxEasyScore;
		private float _maxNormalScore;
		private float _maxHardScore;

		#region Properties
		public float MaxEasyScore { get { return _maxEasyScore; } set { _maxEasyScore = value; } }
		public float MaxNormalScore { get { return _maxNormalScore; } set { _maxNormalScore = value; } }
		public float MaxHardScore { get { return _maxHardScore; } set { _maxHardScore = value; } }
		#endregion
	}
}
