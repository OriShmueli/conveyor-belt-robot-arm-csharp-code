using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Data_Base.Sensors
{
    public class Sensor1 : Sensor
    {
        private Package _currentPackage;
       
        public override Package GetPackage()
        {
            return _currentPackage;
        }

        public override void Reset()
        {
            _currentPackage = null;
        }

        public override void SetPackage(Package package)
        {
            _currentPackage = package;
        }
    }
}
