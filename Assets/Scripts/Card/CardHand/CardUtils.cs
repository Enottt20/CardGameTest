using System.Collections;
using System.Linq;
using Card.CardHandComponent;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Card.CardHand
{
    public class CardUtils : MonoBehaviour
    {

        #region Fields

        [SerializeField] private int minCards;

        [SerializeField] private int maxCards;

        [SerializeField] private GameObject cardPrefabCs;

        [SerializeField] private Transform deckPosition;

        [SerializeField] private Transform gameView;
        
        private int Count { get; set; }
        
        private int LastChangedCard { get; set; }

        public bool CanChangeCard { get; set; }

        public CardHand CardHand { get; private set; }

        #endregion

        #region Unitycallbacks

        private void Awake()
        {
            CardHand = transform.parent.GetComponentInChildren<CardHand>();
            CanChangeCard = true;
        }

        private IEnumerator Start()
        {
            var cardNumber = Random.Range(minCards, maxCards+1);
            //starting cards
            for (var i = 0; i < cardNumber; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DrawCard();
            }
        }

        #endregion

        #region Operations

        [Button]
        public void DrawCard()
        {
            var cardGo = Instantiate(cardPrefabCs, new Vector3(111, 111, 0), Quaternion.identity, gameView);
            cardGo.name = "Card_" + Count;
            var card = cardGo.GetComponent<ICard>();
            card.Init(this);
            Count++;

            StartCoroutine(DrawCard(card));
        }

        private IEnumerator DrawCard(ICard card)
        {
            var cardCharacterPicker = card.gameObject.GetComponentInChildren<SpritePicker>();

            while (!cardCharacterPicker.Ready)
            {
                yield return null;
            }
            
            card.transform.position = deckPosition.position;
            CardHand.AddCard(card);
        }

        public void ChangeValueCard()
        {
            if(!CardHand.Cards.Any() || !CanChangeCard) return;

            if (CardHand.Cards.Count > LastChangedCard)
            {
                CardHand.Cards[LastChangedCard].UiCardValues.CardValues.ChangeRandomValue();
            }
            else
            {
                LastChangedCard = 0;
                
                CardHand.Cards[0].UiCardValues.CardValues.ChangeRandomValue();
            }

            LastChangedCard++;
        }

        public void DrawExistingCard(ICard card)
        {
            card.transform.position = deckPosition.position;
            CardHand.AddCard(card);
        }

        [Button]
        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        #endregion
        
    }
}