using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using TMPro;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardStateMachine.States
{
    /// <summary>
    ///     Base UI Card State.
    /// </summary>
    public abstract class BaseCardState : IState
    {
        private const int LayerToRenderNormal = 0;
        private const int LayerToRenderTop = 1;

        #region Constructor

        protected BaseCardState(ICard handler, BaseStateMachine fsm, UiCardParameters parameters)
        {
            Fsm = fsm;
            Handler = handler;
            Parameters = parameters;
            IsInitialized = true;
        }

        #endregion

        protected ICard Handler { get; }
        protected UiCardParameters Parameters { get; }
        protected BaseStateMachine Fsm { get; }
        public bool IsInitialized { get; }

        #region Operations
        
        protected void MakeCardElementsFirst()
        {
            ChangeOrderOnLayer(Handler.Texts, LayerToRenderTop);
            ChangeOrderOnLayer(Handler.Renderers, LayerToRenderTop);
        }
        
        protected void MakeCardElementNormal()
        {
            ChangeOrderOnLayer(Handler.Texts, LayerToRenderNormal);
            ChangeOrderOnLayer(Handler.Renderers, LayerToRenderNormal);
        }
        
        
        protected void Enable()
        {
            if (Handler.Collider)
                EnableCollision();
            if (Handler.Rigidbody)
                Handler.Rigidbody.Sleep();

            MakeCardElementNormal();
            RemoveAllTransparency();
        }
        
        protected void Disable()
        {
            DisableCollision();
            Handler.Rigidbody.Sleep();
            MakeCardElementNormal();
            
            ChangeAlphaColor(Handler.Texts, Parameters.DisabledAlpha);
            ChangeAlphaColor(Handler.Renderers, Parameters.DisabledAlpha);
        }
        
        protected void DisableCollision()
        {
            Handler.Collider.enabled = false;
        }
        
        protected void EnableCollision()
        {
            Handler.Collider.enabled = true;
        }
        
        protected void RemoveAllTransparency()
        {
            ChangeAlphaColor(Handler.Texts, 1);
            ChangeAlphaColor(Handler.Renderers, 1);
        }

        private void ChangeAlphaColor(TextMeshPro[] textMeshPros, float alpha)
        {
            foreach (var text in textMeshPros)
                if (text)
                {
                    var myColor = text.color;
                    myColor.a = alpha;
                    text.color = myColor;
                }
        }
        
        private void ChangeAlphaColor(SpriteRenderer[] renderers, float alpha)
        {
            foreach (var renderer in renderers)
                if (renderer)
                {
                    var myColor = renderer.color;
                    myColor.a = alpha;
                    renderer.color = myColor;
                }
        }
        
        private void ChangeOrderOnLayer(TextMeshPro[] textMeshPros, int layer)
        {
            for (var i = 0; i < textMeshPros.Length; i++)
                if (textMeshPros[i])
                    textMeshPros[i].sortingOrder = layer;
        }
        
        private void ChangeOrderOnLayer(SpriteRenderer[] renderers, int layer)
        {
            for (var i = 0; i < renderers.Length; i++)
                if (renderers[i])
                    renderers[i].sortingOrder = layer;
        }

        #endregion
        

        #region FSM

        void IState.OnInitialize()
        {
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnNextState(IState next)
        {
        }

        public virtual void OnClear()
        {
        }

        #endregion
        
    }
}