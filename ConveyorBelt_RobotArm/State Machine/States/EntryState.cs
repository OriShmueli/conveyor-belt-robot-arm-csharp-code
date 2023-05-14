using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.State_Machine.States
{
    internal class EntryState : BaseState
    {
        public EntryState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        public override async Task ApplyingData()
        {
        }

        public override async Task EnterState()
        {
            await Task.Yield();
        }

        public override async Task ExitState()
        {
            await Task.Yield();
        }
    }
}
