using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using Tools.UI.Card;

namespace Card.CardStateMachine.States
{
    /// <summary>
    ///     This state disables the collider of the card.
    /// </summary>
    public class CardDisable : BaseCardState
    {
        public CardDisable(ICard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
        }

        public override void OnEnterState()
        {
            Disable();
        }
    }
}