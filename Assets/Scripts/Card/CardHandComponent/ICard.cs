using Card.CardHand;
using Card.CardTransform;
using StateMachine;
using Tools.UI.Card;

namespace Card.CardHandComponent
{
    public interface ICard : IStateMachineHandler, ICardComponents, ICardTransform
    {
        bool IsDragging { get; }
        bool IsHovering { get; }
        bool IsDisabled { get; }
        bool IsPlayer { get; }
        void Init(CardUtils cardUtils);
        void Disable();
        void Enable();
        void Select();
        void Unselect();
        void Hover();
        void Draw();
        void Drop();
    }
}