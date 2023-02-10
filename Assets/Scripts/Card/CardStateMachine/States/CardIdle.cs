using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card.CardStateMachine.States
{
    public class CardIdle : BaseCardState
    {

        public CardIdle(ICard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
            DefaultSize = Handler.transform.localScale;
        }

        private Vector3 DefaultSize { get; }
        

        public override void OnEnterState()
        {
            Handler.Input.OnPointerDown += OnPointerDown;
            Handler.Input.OnPointerEnter += OnPointerEnter;

            if (Handler.Movement.IsOperating)
            {
                DisableCollision();
                Handler.Movement.OnFinishMotion += Enable;
            }
            else
            {
                Enable();
            }

            MakeCardElementNormal();
            Handler.ScaleTo(DefaultSize, Parameters.ScaleSpeed);
        }

        public override void OnExitState()
        {
            Handler.Input.OnPointerDown -= OnPointerDown;
            Handler.Input.OnPointerEnter -= OnPointerEnter;
            Handler.Movement.OnFinishMotion -= Enable;
        }
        

        private void OnPointerEnter(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Handler.Hover();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Handler.Select();
        }
        
    }
}