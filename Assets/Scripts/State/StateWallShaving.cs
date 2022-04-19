using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateWallShaving : IStateBase
    {
        public void OnEnter(StateController controller)
        {
            controller._currrentStateType = StateType.WallShaving;
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.AddGravity();
            controller._moveController.MoveControl();
            if (controller.IsGround())
            {
                controller.ChangeState(StateType.Landing);
                return;
            }
            if (!controller.IsFrontWalled())
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