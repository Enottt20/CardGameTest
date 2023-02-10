using Card.CardHandComponent;
using Card.CardParameters;
using StateMachine;
using Tools.UI.Card;
using UnityEngine;

namespace Card.CardStateMachine.States
{
    public class CardDrag : BaseCardState
    {
        public CardDrag(ICard handler, Camera camera, BaseStateMachine fsm, UiCardParameters parameters) : base(
            handler, fsm, parameters)
        {
            MyCamera = camera;
        }

        private Vector3 StartEuler { get; set; }
        private Camera MyCamera { get; }

        private Vector3 WorldPoint()
        {
            var mousePosition = Handler.Input.MousePosition;
            var worldPoint = MyCamera.ScreenToWorldPoint(mousePosition);
            return worldPoint;
        }

        private void FollowCursor()
        {
            var myZ = Handler.transform.position.z;
            Handler.transform.position = new Vector3(WorldPoint().x, WorldPoint().y, myZ);
        }

        #region Operations

        public override void OnUpdate()
        {
            FollowCursor();
        }

        public override void OnEnterState()
        {
            //stop any movement
            Handler.Movement.StopMotion();

            //cache old values
            StartEuler = Handler.transform.eulerAngles;

            Handler.RotateTo(Vector3.zero, Parameters.RotationSpeed);
            MakeCardElementsFirst();
            RemoveAllTransparency();
        }

        public override void OnExitState()
        {
            //reset position and rotation
            if (Handler.transform)
            {
                Handler.RotateTo(StartEuler, Parameters.RotationSpeed);
                MakeCardElementNormal();
            }

            DisableCollision();
        }

        #endregion
    }
}