using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace CatdogEngine.ScreenSystem {
    /// <summary>
    /// GameScreen의 현재 상태를 표현하는 열거형
    /// </summary>
    public enum ScreenState {
        Hidden,
        TransitionOn,
        TransitionOff,
        Active
    }

    /// <summary>
    /// GameScreen이 전환 되는 이펙트를 Update 로직에서 진행시키는 메소드를 받는다.
    /// </summary>
    /// <param name="gameTime">Delta Time. Update 메소드의 gameTime을 넘겨주면 된다.</param>
    /// <param name="transitionTime">현재까지 진행된 시간을 기록하고 있는 버퍼</param>
    /// <param name="direction">-1 : TransitionOff, 1 : TransitionOn</param>
    /// <returns>true : 스크린 전환이 진행중, false : 스크린 전환이 완료됨</returns>
    public delegate bool SCREEN_TRANSITION_EFFECT(GameTime gameTime, TimeSpan transitionTime, int direction);

    /// <summary>
    /// Update 와 Draw 로직을 포함하는 기본 단위 레이어.
    /// 모든 Screen은 이 클래스를 상속한다.
    /// </summary>
    public abstract class GameScreen {

        SCREEN_TRANSITION_EFFECT Screen_Transition_Effect;
        ScreenState _screenState;

        public GameScreen(SCREEN_TRANSITION_EFFECT screen_transition_effect) {
            _screenState = ScreenState.Hidden;
            Screen_Transition_Effect = screen_transition_effect;
        }

        #region Properties
        public ScreenState ScreenState { get { return _screenState; } set { _screenState = value; } }

        public ScreenManager ScreenManager { get; protected set; }

        public TimeSpan TransitionTime { get; protected set; }
        #endregion

        /// <summary>
        /// GameScreen이 생성된 후 리소스를 할당하는 타이밍에 한 번 호출된다.
        /// 리소스 할당 작업을 여기서 한다.
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// GameScreen이 소멸하기 직전 리소스를 해제하는 타이밍에 한 번 호출된다.
        /// 리소스 해제 작업을 여기서 한다.
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        /// 게임 로직을 진행시킨다.
        /// GameScreen의 현재 상태와 상관없이 무조건 호출된다.
        /// </summary>
        /// <param name="gameTime">Delta Time</param>
        public virtual void Update(GameTime gameTime) { 

            // 스크린 전환 이펙트 처리
            if(ScreenState == ScreenState.TransitionOff) {
                ScreenState = Screen_Transition_Effect(gameTime, TransitionTime, -1) ? ScreenState.TransitionOff : ScreenState.Hidden;
            }
            else if(ScreenState == ScreenState.TransitionOn) {
                ScreenState = Screen_Transition_Effect(gameTime, TransitionTime, 1) ? ScreenState.TransitionOn : ScreenState.Active;
            }
        }

        /// <summary>
        /// GameScreen을 그려야 할 때 호출 된다.
        /// 그리기 작업을 여기서 한다.
        /// </summary>
        /// <param name="gameTime">Delta Time</param>
        public virtual void Draw(GameTime gameTime) { }
    }
}
