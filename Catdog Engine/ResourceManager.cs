using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatdogEngine {
	public class ResourceManager {
		#region Singleton
		private static ResourceManager _instance;
		private ResourceManager() { }
		public static ResourceManager Instance {
			get {
				if (_instance == null) _instance = new ResourceManager();
				return _instance;
			}
		}
		#endregion

		private ContentManager _content;

		/// <summary>
		/// 매니저를 초기화 한다. 반드시 한 번 호출되어야 한다.
		/// </summary>
		public void Initialize(ContentManager content) {
			_content = content;
		}

		/// <summary>
		/// ContentManager.Load(String)
		/// 리소스를 로드하고 해당 인스턴스의 참조값을 반환한다.
		/// </summary>
		public T Load<T>(String assetName) {
			return _content.Load<T>(assetName);
		}

		/// <summary>
		/// ContentManager.Unload()
		/// </summary>
		public void Unload() {
			_content.Unload();
		}
	}
}
