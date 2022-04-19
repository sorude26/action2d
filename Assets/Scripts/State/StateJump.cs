using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StateController
{
    private class StateJump : IStateBase
    {
        private float _stateTimer = default;
        public void OnEnter(StateController controller)
        {
            _stateTimer = controller._stateTimer;
        }

        public void OnFixedUpdate(StateController controller)
        {
            controller._moveController.AddGravityForJump();
            controller._moveController.MoveControl();
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
            _stateTimer -= Time.deltaTime;
            if (_stateTimer < 0)
            {
                controller.ChangeState(StateType.Fall);
            }
        }
    }
}