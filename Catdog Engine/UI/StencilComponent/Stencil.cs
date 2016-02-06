using CatdogEngine.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatdogEngine.UI.StencilComponent
{
	/// <summary>
	/// 화면 최 상단에 그려지는 UI는 모두 이 인터페이스를 상속해야 한다.
	/// Game Screen 내부에서 동작하는 Update와 Draw 로직을 갖는다.
	/// </summary>
	public abstract class Stencil : InputListener
	{
		// 버퍼에서의 스텐실 위치. 좌측 상단이 원점이다.
		protected Vector2 _position;

		// 스텐실이 차지하는 윈도우 화면 영역. 윈도우 사이즈가 변하면 캔버스가 갱신한다.
		private Rectangle _region;

		// 현재 속해있는 캔버스
		private Canvas _canvas;

		// 캔버스가 현재 속해있는 스크린
		protected GameScreen _screen;

		// 스텐실은 여러개의 내부 스텐실을 포함할 수 있다.
		protected List<Stencil> _innerStencils;
		private Stencil _parent;

		// 윈도우 사이즈가 변했을 때 캔버스에서 계산한 이전 사이즈와 현재 사이즈의 비율
		float _changedWindowWidthRate, _changedWindowHeightRate;

		#region Properties
		public Vector2 Position
		{
			get { return _position; }
			set
			{
				_position = value;

				// 기본적으로는 Region의 좌측 상단이 Position이 되도록 자동으로 갱신한다.
				BufferRegion = new Rectangle((int)_position.X, (int)_position.Y, BufferRegion.Width, BufferRegion.Height);
			}
		}

		/// <summary>
		/// 버퍼에 그려지는 실제 위치
		/// </summary>
		public Rectangle BufferRegion {
			get
			{
				return _region;
			}
			set { _region = value; } }

		/// <summary>
		/// 윈도우 좌표로 환산 된 값. 입력 처리 등의 작업 시 실제로 사용하는 값.
		/// </summary>
		public Rectangle WindowRegion
		{
			get
			{
				if(Canvas != null)
				{
					float widthRate = Canvas.WindowBufferWidthRate;
					float heightRate = Canvas.WindowBufferHeightRate;
					Rectangle temp = new Rectangle((int)(BufferRegion.X * widthRate), (int)(BufferRegion.Y * heightRate), 
						(int)(BufferRegion.Width * widthRate), (int)(BufferRegion.Height * heightRate));

					return temp;
				}
				else
				{
					return BufferRegion;
				}
			}
		}
		public GameScreen CurrentScreen { get { return _screen; } set { _screen = value; } }
		public Canvas Canvas { get { return _canvas; } set { _canvas = value; } }
		public List<Stencil> InnerStencils { get { return _innerStencils; } }
		public float ChangedWindowWidthRate { get { return _changedWindowWidthRate; } set { _changedWindowWidthRate = value; } }
		public float ChangedWindowHeightRate { get { return _changedWindowHeightRate; } set { _changedWindowHeightRate = value; } }

		public Stencil Parent { get { return _parent; } set { _parent = value; } }
		#endregion

		public Stencil(GameScreen currentScreen)
		{
			Position = new Vector2(0, 0);
			BufferRegion = new Rectangle();

			CurrentScreen = currentScreen;

			_innerStencils = new List<Stencil>();

			// 초기값을 1로 하는 것이 타당하다.
			ChangedWindowWidthRate = 1f;
			ChangedWindowHeightRate = 1f;
		}

		protected void AddInnerStencil(Stencil stencil)
		{
			if (stencil != null)
			{
				stencil.Parent = this;
				_innerStencils.Add(stencil);
			}
		}

		/// <summary>
		/// 스텐실의 로직을 진행시킨다.
		/// </summary>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// 해당 스텐실을 화면에 그려야 할 때 호출된다.
		/// </summary>
		public abstract void Draw(GameTime gameTime);


		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Input Events
		// 필요한 함수를 재정의 하여 사용
		///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public abstract void OnLeftMouseDown(int x, int y);
		public abstract void OnLeftMouseUp(int x, int y);
		public abstract void OnMouseMove(int x, int y);
		public abstract void OnKeyDown(Keys key);
		public abstract void OnKeyUp(Keys key);
	}
}
