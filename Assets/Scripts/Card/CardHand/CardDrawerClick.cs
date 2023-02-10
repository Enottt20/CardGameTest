using Tools.Input;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card.CardHand
{
    [RequireComponent(typeof(IMouseInput))]
    public class CardDrawerClick : MonoBehaviour
    {
        private CardUtils CardDrawer { get; set; }
        private IMouseInput Input { get; set; }

        private void Awake()
        {
            CardDrawer = transform.parent.GetComponentInChildren<CardUtils>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += DrawCard;
        }

        private void DrawCard(PointerEventData obj)
        {
            CardDrawer.DrawCard();
        }
    }
}