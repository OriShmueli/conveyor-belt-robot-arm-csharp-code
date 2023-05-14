using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class DeactivatingState : BaseState
    {
        public DeactivatingState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {

        }

        public override async Task ApplyingData()
        {
        }

        public override async Task EnterState()
        {
        }

        public override async Task ExitState()
        {
        }
    }
}
