using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off
{
    internal class FirstTimeOnOffBaseData : OnOffBaseData
    {
        public new static int BufferLength { get; private set; } = 4; //dont know way i put there 3
        public new static char DetermineByte { get; private set; } = 'f';
        public static char FirstTimeDetermineByte { get; private set; } = '0';

        public override int GetBufferLength()
        {
            return BufferLength;
        }
    }
}
