using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateGroundMove : IStateBase
    {
        public void OnEnter(StateController controller)
        {
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.Move(controller.InputVector);
            controller._moveController.AddGravity();
            controller._moveController.MoveControl();
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
        }
    }
}