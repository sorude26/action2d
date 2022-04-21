using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateGroundMove : IStateBase
    {
        public void OnEnter(StateController controller)
        {
            controller._currrentStateType = StateType.GroundMove;
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.Move(controller.InputVector);
            controller._moveController.AddGravity();
            controller._moveController.MoveControl();
            controller._moveController.MoveDecelerate();
            if (!controller.IsGround())
            {
                controller.ChangeState(StateType.Fall);
            }
        }

        public void OnLeave(StateController controller)
        {
        }

        public void OnUpdate(StateController controller)
        {
            if (Mathf.Abs(controller.InputVector.x) <= 0 || controller.IsFrontWalled())
            {
                controller.ChangeState(StateType.Idle);
            }
        }
    }
}