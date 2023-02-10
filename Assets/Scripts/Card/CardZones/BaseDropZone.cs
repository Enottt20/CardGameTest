using Card.CardHand;
using Tools.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card.CardZones
{
    [RequireComponent(typeof(IMouseInput))]
    public abstract class BaseDropZone : MonoBehaviour
    {
        protected ICardHand CardHand { get; set; }
        protected IMouseInput Input { get; set; }

        protected virtual void Awake()
        {
            CardHand = transform.parent.GetComponentInChildren<ICardHand>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerUp += OnPointerUp;
        }

        protected virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}