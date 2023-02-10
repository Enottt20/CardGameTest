using Card.CardHandComponent;
using Card.CardParameters;
using Card.CardStateMachine.States;
using StateMachine;
using UnityEngine;

namespace Card.CardStateMachine
{
    public class CardHandFsm : BaseStateMachine
    {

        #region Constructor

        public CardHandFsm(Camera camera, UiCardParameters cardConfigsParameters, ICard handler = null) :
            base(handler)
        {
            CardConfigsParameters = cardConfigsParameters;

            IdleState = new CardIdle(handler, this, CardConfigsParameters);
            DisableState = new CardDisable(handler, this, CardConfigsParameters);
            DragState = new CardDrag(handler, camera, this, CardConfigsParameters);
            HoverState = new CardHover(handler, this, CardConfigsParameters);
            DrawState = new CardDraw(handler, this, CardConfigsParameters);
            DropState = new CardDrop(handler, this, CardConfigsParameters);

            RegisterState(IdleState);
            RegisterState(DisableState);
            RegisterState(DragState);
            RegisterState(HoverState);
            RegisterState(DrawState);
            RegisterState(DropState);

            Initialize();
        }

        #endregion

        #region Properties

        private CardIdle IdleState { get; }
        private CardDisable DisableState { get; }
        private CardDrag DragState { get; }
        private CardHover HoverState { get; }
        private CardDraw DrawState { get; }
        private CardDrop DropState { get; }
        private UiCardParameters CardConfigsParameters { get; }

        #endregion

        #region Operations

        public void Hover()
        {
            PushState<CardHover>();
        }

        public void Disable()
        {
            PushState<CardDisable>();
        }

        public void Enable()
        {
            PushState<CardIdle>();
        }

        public void Select()
        {
            PushState<CardDrag>();
        }

        public void Unselect()
        {
            Enable();
        }

        public void Draw()
        {
            PushState<CardDraw>();
        }

        public void Drop()
        {
            PushState<CardDrop>();
        }

        #endregion
        
    }
}