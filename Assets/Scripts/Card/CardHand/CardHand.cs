using System;
using System.Collections.Generic;
using Card.CardHandComponent;
using Tools.UI.Card;

namespace Card.CardHand
{
    public class CardHand : CardPile.CardPile, ICardHand
    {

        #region Properties
        
        public ICard SelectedCard { get; private set; }

        private event Action<ICard> OnCardSelected = card => { };

        private event Action<ICard> OnCardPlayed = card => { };
        
        Action<ICard> ICardHand.OnCardPlayed { get => OnCardPlayed; set => OnCardPlayed = value; }
        
        Action<ICard> ICardHand.OnCardSelected{ get => OnCardSelected; set => OnCardSelected = value; }

        #endregion

        #region Operations
        
        public void SelectCard(ICard card)
        {
            SelectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

            //disable all cards
            DisableCards();
            NotifyCardSelected();
        }

        public void DeleteCard(ICard card)
        {
            Destroy(card.gameObject);

            Cards.Remove(card);
            
            NotifyPileChange();
        }

        public void PlaySelected()
        {
            if (SelectedCard == null)
                return;

            PlayCard(SelectedCard);
        }
        
        public void PlayCard(ICard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            SelectedCard = null;
            RemoveCard(card);
            OnCardPlayed?.Invoke(card);
            EnableCards();
            NotifyPileChange();
        }
        
        public void UnselectCard(ICard card)
        {
            if (card == null)
                return;

            SelectedCard = null;
            card.Unselect();
            NotifyPileChange();
            EnableCards();
        }
        
        public void Unselect()
        {
            UnselectCard(SelectedCard);
        }
        
        public void DisableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Disable();
        }
        
        public void EnableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Enable();
        }

        [Button]
        private void NotifyCardSelected()
        {
            OnCardSelected?.Invoke(SelectedCard);
        }

        #endregion
    }
}