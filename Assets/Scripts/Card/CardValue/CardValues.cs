using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Card.CardValue
{
    public class CardValues
    {

        private CardAttackValue attackValue;
        private CardHealthValue healthValue;
        private CardManaValue manaValue;
        
        public CardAttackValue AttackValue
        {
            get => attackValue;
        }
        
        public CardHealthValue HealthValue
        {
            get => healthValue;
        }
        
        public CardManaValue ManaValue
        {
            get => manaValue;
        }

        public readonly List<BaseCardValue> values = new List<BaseCardValue>();

        public CardValues()
        {
            attackValue = new CardAttackValue();
            healthValue = new CardHealthValue();
            manaValue = new CardManaValue();

            values.Add(attackValue);
            values.Add(healthValue);
            values.Add(manaValue);
        }

        public void ChangeRandomValue()
        {
            values[Random.Range(0, values.Count)].RandomlyChangeValue();
        }
    }
}