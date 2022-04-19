using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateController
{
    private class StateIdle : IStateBase
    {
        public void OnEnter(StateController controller)
        {
        }

        public void OnFixedUpdate(StateController controller)
        {
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
            if (controller.InputVector.x > 0 || controller.InputVector.x < 0)
            {
                controller.ChangeState(StateType.GroundMove);
            }
        }
    }
}