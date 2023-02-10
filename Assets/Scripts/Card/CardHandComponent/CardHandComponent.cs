using Card.CardHand;
using Card.CardParameters;
using Card.CardStateMachine;
using Card.CardStateMachine.States;
using Card.CardTransform;
using Card.CardValue;
using Extensions;
using TMPro;
using Tools.Input;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardHandComponent
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class CardHandComponent : MonoBehaviour, ICard
    {

        #region Components

        SpriteRenderer[] ICardComponents.Renderers => MyRenderers;
        TextMeshPro[] ICardComponents.Texts => MyTexts;
        SpriteRenderer ICardComponents.MyRenderer => MyRenderer;
        Collider ICardComponents.Collider => MyCollider;
        Rigidbody ICardComponents.Rigidbody => MyRigidbody;
        CardUtils ICardComponents.CardUtils => MyCardUtils;

        UiCardValues ICardComponents.UiCardValues => MyUiCardValues;
        IMouseInput ICardComponents.Input => MyInput;
        ICardHand ICardComponents.Hand => Hand;

        #endregion

        #region Transform

        public MotionBaseCard Movement { get; private set; }
        public MotionBaseCard Rotation { get; private set; }
        public MotionBaseCard Scale { get; private set; }

        #endregion

        #region Properties

        public string Name => gameObject.name;
        public UiCardParameters CardConfigsParameters => cardConfigsParameters;
        [SerializeField] public UiCardParameters cardConfigsParameters;
        private CardHandFsm Fsm { get; set; }
        private Transform MyTransform { get; set; }
        private Collider MyCollider { get; set; }
        private SpriteRenderer[] MyRenderers { get; set; }
        private TextMeshPro[] MyTexts { get; set; }
        private CardUtils MyCardUtils { get; set; }
        private UiCardValues MyUiCardValues { get; set; }
        private SpriteRenderer MyRenderer { get; set; }
        private Rigidbody MyRigidbody { get; set; }
        private IMouseInput MyInput { get; set; }
        private ICardHand Hand { get; set; }
        public MonoBehaviour MonoBehavior => this;
        public Camera MainCamera => Camera.main;
        public bool IsDragging => Fsm.IsCurrent<CardDrag>();
        public bool IsHovering => Fsm.IsCurrent<CardHover>();
        public bool IsDisabled => Fsm.IsCurrent<CardDisable>();
        public bool IsPlayer => transform.CloserEdge(MainCamera, Screen.width, Screen.height) == 1;

        #endregion

        #region Transform

        public void RotateTo(Vector3 rotation, float speed)
        {
            Rotation.Execute(rotation, speed);
        }

        public void MoveTo(Vector3 position, float speed, float delay)
        {
            Movement.Execute(position, speed, delay, false);
        }

        public void MoveToWithZ(Vector3 position, float speed, float delay)
        {
            Movement.Execute(position, speed, delay, true);
        }

        public void ScaleTo(Vector3 scale, float speed, float delay)
        {
            Scale.Execute(scale, speed, delay);
        }

        #endregion

        #region Operations

        public void Init(CardUtils cardUtils)
        {
            MyCardUtils = cardUtils;
        }

        public void Hover()
        {
            Fsm.Hover();
        }

        public void Disable()
        {
            Fsm.Disable();
        }

        public void Enable()
        {
            Fsm.Enable();
        }

        public void Select()
        {
            // to avoid the player selecting enemy's cards
            if (!IsPlayer)
                return;

            Hand.SelectCard(this);
            Fsm.Select();
        }

        public void Unselect()
        { 
            Fsm.Unselect();
        }

        public void Draw()
        {
            Fsm.Draw();
        }

        public void Drop()
        {
            Fsm.Drop();
        }

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            //components
            MyTransform = transform;
            MyCollider = GetComponent<Collider>();
            MyRigidbody = GetComponent<Rigidbody>();
            MyInput = GetComponent<IMouseInput>();
            Hand = transform.parent.GetComponentInChildren<ICardHand>();
            MyRenderers = GetComponentsInChildren<SpriteRenderer>();
            MyTexts = GetComponentsInChildren<TextMeshPro>();
            MyRenderer = GetComponent<SpriteRenderer>();
            MyUiCardValues = GetComponent<UiCardValues>();

            //transform
            Scale = new MotionScaleCard(this);
            Movement = new MotionMovementCard(this);
            Rotation = new MotionRotationCard(this);


            //fsm
            Fsm = new CardHandFsm(MainCamera, CardConfigsParameters, this);
        }

        private void Update()
        {
            Fsm?.Update();
            Movement?.Update();
            Rotation?.Update();
            Scale?.Update();
        }

        #endregion
    }
}