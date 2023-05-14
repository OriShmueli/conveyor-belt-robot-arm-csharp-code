using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.End;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit
{
    internal class EditData : ReceiveBaseDataProtocol
    {
        private int _bufferLength = 30; //12
        private char _startByte = 'e';
        public override int GetBufferLength()
        {
            return _bufferLength;
        }

        public override char GetStatingByte()
        {
            return _startByte;
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            string dataMessage = System.Text.Encoding.Default.GetString(dataArray);
            //await Task.Delay(100);

            if(dataMessage == "#m")
            {
                await ElementsUtil.InformOnNewData(new EditContinueSendingData());
                return;
            }

            if (dataMessage == "#e")
            {
                await ElementsUtil.InformOnNewData(new EditRequestData());
                return;
            }

            if(dataMessage == "#e#n")
            {
                await ElementsUtil.InformOnNewData(new EditEnterStateData());
                return;
            }

            if(dataMessage == "#e#t")
            {
                await ElementsUtil.InformOnNewData(new EditExitStateData());
                return;
            }

            GetArmPositionsData getArmPositionsData = new GetArmPositionsData();
            if (length < 28 || length > 28)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Edit Data [class]. Requesting data again...");
                byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error); //RequestArmStartPositionsError
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
                return;
            }

            for (int i = 0; i < length; i++)
            {
                if (dataArray[i] == partition)
                {

                    if (dataArray[i + 2] == '<')
                    {
                        await ElementsUtil.ApplicationConsole.WriteLine("Length: " + length + "id: " + ((int)(char)dataArray[i + 1] - '0').ToString() + ". position: " + ConvertByteArrayToIntByOffset(dataArray, i + 3).ToString());
                        getArmPositionsData.initialize((int)(char)dataArray[i + 1] - '0', ConvertByteArrayToIntByOffset(dataArray, i + 3));
                    }
                }
            }

            if (await getArmPositionsData.chackIfAllDataPass())
            {
                await ElementsUtil.ApplicationConsole.WriteLine("ReadingRobotEditingPositionsByte: " + System.Text.Encoding.Default.GetString(dataArray));
                //await Task.Delay(100);
                await ElementsUtil.InformOnNewData(getArmPositionsData);
                return;
            }
            else
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Edit Data [class]. Requesting data again...");
                byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
                return;
            }

            //await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Edit Data [class]. Requesting data again...");
            //byte[] mainerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, mainerrData);
        }

        private int ConvertByteArrayToIntByOffset(byte[] array, int offset)
        {
            int distance = 0;
            List<int> newListNumber = new List<int>();
            for (int i = offset; i < 3 + offset; i++)
            {
                if (array[i] == '-')
                {
                }
                else
                {
                    newListNumber.Add((int)(char)array[i] - '0');
                }
            }

            for (int i = 0; i < newListNumber.Count; i++)
            {
                distance += newListNumber[i] * Convert.ToInt32(Math.Pow(10, newListNumber.Count - i - 1));
            }

            return distance;
        }
    }
}
