using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors
{
    internal class MagneticSensorData : SensorBaseData
    {
        public static new char DetermineByte { get; private set; } = 'm';
    }
}
