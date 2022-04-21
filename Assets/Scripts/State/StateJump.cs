using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateJump : IStateBase
    {
        public void OnEnter(StateController controller)
        {
            controller._currrentStateType = StateType.Jump;
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.AddGravityForJump();
            controller._moveController.MoveControl();
            controller._moveController.FlyDecelerate();
            if (controller.IsTopWalled())
            {
                controller.ChangeState(StateType.Fall);
                return;
            }
        }

        public void OnLeave(StateController controller)
        {
        }

        public void OnUpdate(StateController controller)
        {
            controller._stateTimer -= Time.deltaTime;
            if (controller._stateTimer < 0)
            {
                controller.ChangeState(StateType.Fall);
            }
        }
    }
}