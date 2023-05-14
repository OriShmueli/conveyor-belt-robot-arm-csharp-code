using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off
{
    internal class OnData : OnOffBaseData
    {
        public new static char DetermineByte { get; private set; } = 'n';

    }
}
