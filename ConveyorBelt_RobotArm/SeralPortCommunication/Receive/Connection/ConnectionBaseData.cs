using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine.States;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Connection
{
    internal class ConnectionBaseData : ReceiveBaseDataProtocol
    {
        private char _connectedByte = 'c';
        private char _disconnectedByte = 'd';

        public override int GetBufferLength()
        {
            return 2;
        }

        public override char GetStatingByte()
        {
            return 'r';
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            await Task.Yield();

            if (length == 0)
            {
                return;
            }
            //MessageBox.Show("on off process data");
            for (int i = 0; i < length; i++) //
            {
                if (i == 0 && dataArray[i] != '#')
                {
                    return;
                }

                if(dataArray[i] == partition)
                {
                    if (dataArray[i + 1] == _connectedByte)
                    {
                        //await ElementsUtil.ApplicationConsole.WriteLine("InformOnNewData(new ConnectionData());");
                        await ElementsUtil.InformOnNewData(new ConnectedData());
                        return;
                    }

                    if (dataArray[i + 1] == _disconnectedByte)
                    {
                        await ElementsUtil.InformOnNewData(new DisconnectedData());
                        return;
                    }

                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Connection State. Requesting data again...");
                    byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                }
               
            }
        }
    }
}
