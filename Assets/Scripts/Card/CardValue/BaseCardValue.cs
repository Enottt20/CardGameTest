using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Card.CardValue
{
    public class BaseCardValue
    {
        protected readonly int minValue = -2;
        protected readonly int maxValue = 9;

        public event Action OnValueChange;

        protected BaseCardValue()
        {
            RandomlyChangeValue();
        }
        
        private int value;

        public virtual int Value
        {
            get => value;

            set
            {

                if (minValue < value && value < maxValue)
                {
                    this.value = value;
                    OnValueChange?.Invoke();
                }
            }
        }

        public void RandomlyChangeValue()
        {
            Value = Random.Range(minValue, maxValue + 1);
        }
        
    }
}