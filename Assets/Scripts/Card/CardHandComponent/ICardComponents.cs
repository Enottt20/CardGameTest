using Card.CardHand;
using Card.CardParameters;
using Card.CardValue;
using TMPro;
using Tools.Input;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardHandComponent
{
    public interface ICardComponents
    {
        UiCardParameters CardConfigsParameters { get; }
        Camera MainCamera { get; }
        ICardHand Hand { get; }
        SpriteRenderer[] Renderers { get; }
        CardUtils CardUtils { get; }
        UiCardValues UiCardValues { get; }
        TextMeshPro[] Texts { get; }
        SpriteRenderer MyRenderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        IMouseInput Input { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
        MonoBehaviour MonoBehavior { get; }
    }
}