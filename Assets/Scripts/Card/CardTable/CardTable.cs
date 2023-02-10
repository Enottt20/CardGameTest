using System;
using Card.CardHand;
using Card.CardHandComponent;
using UnityEngine;

namespace Card.CardTable
{
    public class CardTable : CardPile.CardPile
    {
        [SerializeField] [Tooltip("World point where the graveyard is positioned")]
        private Transform graveyardPosition;

        public ICardHand CardHand { get; private set; }

        
        #region Unitycallbacks

        protected override void Awake()
        {
            base.Awake();
            CardHand = transform.parent.GetComponentInChildren<CardHand.CardHand>();
            CardHand.OnCardPlayed += AddCard;
        }

        #endregion

        #region Operations
        
        public override void AddCard(ICard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(graveyardPosition);
            card.Drop();
            NotifyPileChange();
        }

        
        public override void RemoveCard(ICard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);
            NotifyPileChange();
        }

        #endregion
    }
}