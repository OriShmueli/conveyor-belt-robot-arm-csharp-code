using ConveyorBelt_RobotArm.Components.Program_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Error
{
    internal class ErrorData : ReceiveBaseDataProtocol
    {
        private int _length =1;
        private char _startingByte = 'v';

        public override int GetBufferLength()
        {
            return _length;
        }

        public override char GetStatingByte()
        {
            return _startingByte;
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            return;
        }

        public async Task ProcessData()
        {
           
            await ElementsUtil.StateMachine.CurrentState.SendLastMessage();//TODO: Make an delegate of last message.
            
        }
    }
}
