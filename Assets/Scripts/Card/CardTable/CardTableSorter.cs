using System;
using Card.CardHandComponent;
using Card.CardParameters;
using Card.CardPile;
using UnityEngine;

namespace Card.CardTable
{
    [RequireComponent(typeof(CardTable))]
    public class CardTableSorter : MonoBehaviour
    {

        [SerializeField] private UiCardParameters parameters;

        private ICardPile CardTable { get; set; }
        

        private void Awake()
        {
            CardTable = GetComponent<CardTable>();
            CardTable.OnPileChanged += Sort;
        }
        

        public void Sort(ICard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var lastPos = cards.Length - 1;
            var lastCard = cards[lastPos];
            
            var position = lastCard.transform.position;
            position = new Vector3(position.x, position.y, -cards.Length);
            lastCard.transform.position = position;
            
        }
        
    }
}