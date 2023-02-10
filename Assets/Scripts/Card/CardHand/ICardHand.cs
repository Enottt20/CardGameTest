using System;
using Card.CardHandComponent;
using Card.CardPile;
using Tools.UI.Card;

namespace Card.CardHand
{
    public interface ICardHand : ICardPile
    {
        void PlaySelected();
        void Unselect();
        void PlayCard(ICard card);
        void SelectCard(ICard card);
        void UnselectCard(ICard card);
        Action<ICard> OnCardPlayed { get; set; }
        Action<ICard> OnCardSelected { get; set; }
    }
}