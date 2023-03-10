using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.Input
{
    public interface IMouseInput :
        IPointerClickHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        //clicks
        new Action<PointerEventData> OnPointerClick { get; set; }
        new Action<PointerEventData> OnPointerDown { get; set; }
        new Action<PointerEventData> OnPointerUp { get; set; }

        //drag
        new Action<PointerEventData> OnBeginDrag { get; set; }
        new Action<PointerEventData> OnDrag { get; set; }
        new Action<PointerEventData> OnEndDrag { get; set; }
        new Action<PointerEventData> OnDrop { get; set; }

        //enter
        new Action<PointerEventData> OnPointerEnter { get; set; }
        new Action<PointerEventData> OnPointerExit { get; set; }

        Vector2 MousePosition { get; }
        DragDirection DragDirection { get; }
    }

    public enum DragDirection
    {
        None,
        Down,
        Left,
        Top,
        Right
    }
    
    [RequireComponent(typeof(Collider))]
    public class MouseInputProvider : MonoBehaviour, IMouseInput
    {
        #region Unity Callbacks

        private void Awake()
        {
            // Currently using PhysicsRaycaster, but can be also considered PhysicsRaycaster2D.
            if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
                throw new Exception(GetType() + " needs an " + typeof(PhysicsRaycaster) + " on the MainCamera");
        }

        #endregion

        #region Drag Direction
        
        private DragDirection GetDragDirection()
        {
            var currentPosition = UnityEngine.Input.mousePosition;
            var normalized = (currentPosition - oldDragPosition).normalized;
            oldDragPosition = currentPosition;

            if (normalized.x > 0)
                return DragDirection.Right;

            if (normalized.x < 0)
                return DragDirection.Left;

            if (normalized.y > 0)
                return DragDirection.Top;

            return normalized.y < 0 ? DragDirection.Down : DragDirection.None;
        }

        #endregion

        #region Delegates 
        
        Action<PointerEventData> IMouseInput.OnPointerDown { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerUp { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerClick { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnBeginDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnEndDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrop { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerEnter { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerExit { get; set; } = eventData => { };

        #endregion

        #region Properties and Fields

        private Vector3 oldDragPosition;
        DragDirection IMouseInput.DragDirection => GetDragDirection();
        Vector2 IMouseInput.MousePosition => UnityEngine.Input.mousePosition;

        #endregion

        #region Unity Mouse Events

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            ((IMouseInput) this).OnBeginDrag.Invoke(eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            ((IMouseInput) this).OnDrag.Invoke(eventData);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            ((IMouseInput) this).OnDrop.Invoke(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            ((IMouseInput) this).OnEndDrag.Invoke(eventData);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            ((IMouseInput) this).OnPointerClick.Invoke(eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            ((IMouseInput) this).OnPointerDown.Invoke(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            ((IMouseInput) this).OnPointerUp.Invoke(eventData);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            ((IMouseInput) this).OnPointerEnter.Invoke(eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ((IMouseInput) this).OnPointerExit.Invoke(eventData);
        }

        #endregion
    }
}