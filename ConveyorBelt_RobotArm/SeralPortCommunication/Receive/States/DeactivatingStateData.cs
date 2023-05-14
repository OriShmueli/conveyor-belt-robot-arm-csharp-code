using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States
{
    internal class DeactivatingStateData : StateData
    {
        public static char DetermineByte { get; private set; } = 'd';
    }
}
