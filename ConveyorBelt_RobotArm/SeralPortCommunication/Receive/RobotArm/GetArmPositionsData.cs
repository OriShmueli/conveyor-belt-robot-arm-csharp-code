using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm
{
    public class GetArmPositionsData : RobotArmData
    {
        public static char DetermineByte { get; private set; } = 'r';

        private Dictionary<int, int> _armIdAndPositions = new Dictionary<int, int>();
        public Dictionary<int, int> ArmIdAndPositions { get { return _armIdAndPositions; } }

        public void initialize(int id, int position)
        {
            _armIdAndPositions.Add(id, position);
        }

        public async Task<bool> chackIfAllDataPass()
        {
            return await Task.Run(() => {
                if (_armIdAndPositions.Count == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
           
        }

    }
}
