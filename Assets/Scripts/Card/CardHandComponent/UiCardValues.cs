using System;
using System.Collections;
using Card.CardValue;
using TMPro;
using UnityEngine;

namespace Card.CardHandComponent
{
    public class UiCardValues : MonoBehaviour
    {
        [SerializeField] private TextMeshPro attackText;
        [SerializeField] private TextMeshPro manaText;
        [SerializeField] private TextMeshPro healthText;

        private ICard Card { get; set; }

        public CardValues CardValues { get; private set; }

        private void Awake()
        {
            CardValues = new CardValues();
            
            Card = GetComponentInParent<ICard>();
        }

        private void Start()
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            StartCoroutine(CountText(int.Parse(attackText.text), CardValues.AttackValue.Value, attackText));
                
            StartCoroutine(CountText(int.Parse(healthText.text), CardValues.HealthValue.Value, healthText));
                
            StartCoroutine(CountText(int.Parse(manaText.text), CardValues.ManaValue.Value, manaText));
        }
        
        private IEnumerator CountText(int startValue, int newValue, TMP_Text text)
        {
            Card.CardUtils.CanChangeCard = false;
            
            var currentValue = startValue;
            
            float time = 0;

            while (time < 1 || currentValue != newValue)
            {
                currentValue = (int)Mathf.Lerp(startValue, newValue, time);
                time += Time.deltaTime;
                text.text = currentValue.ToString();
                yield return null;
            }
            
            Card.CardUtils.CanChangeCard = true;
        }

        private void RemoveCard()
        {
            Card.CardUtils.CanChangeCard = true;
            Card.CardUtils.CardHand.DeleteCard(Card);
        }

        private void OnEnable()
        {
            foreach (var value in CardValues.values)
            {
                value.OnValueChange += UpdateUi;
            }
            
        }

        private void OnDisable()
        {
            foreach (var value in CardValues.values)
            {
                value.OnValueChange -= UpdateUi;
            }
            
        }
    }
}