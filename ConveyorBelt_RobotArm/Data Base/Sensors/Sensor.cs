using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Data_Base.Sensors
{
    public abstract class Sensor
    {
        public abstract Package GetPackage();
        public abstract void SetPackage(Package package);
        public abstract void Reset();
    }
}
