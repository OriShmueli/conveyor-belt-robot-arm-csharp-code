using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication
{
    public abstract class ReceiveBaseDataProtocol
    {
        public static char StartingByte { get; private set; } = '0';
        public static int BufferLength { get; private set; } = 0;
        public abstract char GetStatingByte();
        public abstract int GetBufferLength();
        public abstract Task ProcessData(byte[] dataArray, int length);
        protected char partition = '#';

    }
}
