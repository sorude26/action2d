using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateController
{
    private class StateFall : IStateBase
    {
        public void OnEnter(StateController controller)
        {
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
            if (controller.IsFrontWalled())
            {
                controller.ChangeState(StateType.WallShaving);
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