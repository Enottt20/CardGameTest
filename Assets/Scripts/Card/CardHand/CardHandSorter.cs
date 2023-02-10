using System;
using Card.CardHandComponent;
using Card.CardPile;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardHand
{
    [RequireComponent(typeof(CardHand))]
    public class CardHandSorter : MonoBehaviour
    {

        private const int OffsetZ = -1;
        private ICardPile CardHand { get; set; }

        private void Awake()
        {
            CardHand = GetComponent<CardHand>();
            CardHand.OnPileChanged += Sort;
        }

        public void Sort(ICard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var layerZ = 0;
            foreach (var card in cards)
            {
                var localCardPosition = card.transform.localPosition;
                localCardPosition.z = layerZ;
                card.transform.localPosition = localCardPosition;
                layerZ += OffsetZ;
            }
        }
    }
}