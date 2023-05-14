using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off
{
    internal class FirstTimeOffData : FirstTimeOnOffBaseData
    {
        public static new char FirstTimeDetermineByte { get; private set; } = 'f';
    }
}