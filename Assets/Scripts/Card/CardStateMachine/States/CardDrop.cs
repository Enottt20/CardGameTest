using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card.CardStateMachine.States
{
    public class CardDrop : BaseCardState
    {
        public CardDrop(ICard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
        }

        private Vector3 StartScale { get; set; }

        #region Operations

        public override void OnEnterState()
        {
            SetScaleOnEnterState();
            SetRotation();
            EnableCollision();
            SubscribeInput();
        }

        public override void OnExitState()
        {
            UnsubscribeInput();
            SetScaleOnExitState();
        }
        
        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this) && Handler.CardUtils.CardHand.SelectedCard == null)
                Handler.CardUtils.DrawExistingCard(Handler);
        }

        private void SubscribeInput()
        {
            Handler.Input.OnPointerDown += OnPointerDown;
        }

        private void UnsubscribeInput()
        {
            Handler.Input.OnPointerDown -= OnPointerDown;
        }

        private void SetScaleOnEnterState()
        {
            var scale = Handler.transform.localScale * Parameters.DiscardedSize;
            Handler.ScaleTo(scale, Parameters.ScaleSpeed);
        }
        
        private void SetScaleOnExitState()
        {
            Handler.transform.localScale *= 2;
        }

        private void SetRotation()
        {
            var speed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;
            Handler.RotateTo(Vector3.zero, speed);
        }

        #endregion
    }
}