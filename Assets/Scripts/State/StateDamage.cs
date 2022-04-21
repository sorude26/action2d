using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateDamage : IStateBase
    {
        public void OnEnter(StateController controller)
        {
            controller._currrentStateType = StateType.Damage;
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.AddGravity();
            controller._moveController.MoveControl();
            controller._moveController.MoveDecelerate();
        }

        public void OnLeave(StateController controller)
        {
        }

        public void OnUpdate(StateController controller)
        {
            controller._stateTimer -= Time.deltaTime;
            if (controller._stateTimer < 0)
            {
                controller.ChangeState(StateType.Idle);
            }
        }
    }
}