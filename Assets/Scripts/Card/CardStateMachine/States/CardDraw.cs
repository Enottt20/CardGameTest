using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardStateMachine.States
{
    public class CardDraw : BaseCardState
    {
        public CardDraw(ICard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
        }

        private Vector3 StartScale { get; set; }
        

        #region Operations

        public override void OnEnterState()
        {
            CachePreviousValue();
            DisableCollision();
            SetScale();
            Handler.Movement.OnFinishMotion += GoToIdle;
        }

        public override void OnExitState()
        {
            Handler.Movement.OnFinishMotion -= GoToIdle;
        }

        private void GoToIdle()
        {
            Handler.Enable();
        }

        private void CachePreviousValue()
        {
            StartScale = Handler.transform.localScale;
            Handler.transform.localScale *= Parameters.StartSizeWhenDraw;
        }

        private void SetScale()
        {
            Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);
        }

        #endregion
    }
}