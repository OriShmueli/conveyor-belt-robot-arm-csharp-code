using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off
{
    //TODO: clean up these classes from static 
    internal class OnOffBaseData : ReceiveBaseDataProtocol
    {
        public new static char StartingByte { get; private set; } = 'o';
        public new static int BufferLength { get; private set; } = 4;
        public static char DetermineByte { get; private set; } = 'l';

        public char OnStartingByte { get; private set; } = 'n';
        public char OffStartingByte { get; private set; } = 'f';

        public char FirsTime { get; private set; } = 'f';

        public override int GetBufferLength()
        {
            return BufferLength;
        }

        public override char GetStatingByte()
        {
            return StartingByte;
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            if (length == 2)
            {
                if (dataArray[1] == 'n')
                {
                    await ElementsUtil.ApplicationConsole.WriteLine("OnStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                    await ElementsUtil.InformOnNewData(new OnData());
                    return;
                }

                if (dataArray[1] == 'f')
                {
                    await ElementsUtil.ApplicationConsole.WriteLine("OffStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                    await ElementsUtil.InformOnNewData(new OffData());
                    return;
                }
            }
            //#n#
            if (dataArray[0] == partition && (dataArray[1] == 'f'|| dataArray[1] == 'n') && dataArray[2] == partition && length == 3)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                return;
            }

            if(length == 0 || dataArray[1] == ' ')
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                return;
            }
            //await ElementsUtil.ApplicationConsole.WriteLine("onOffProcessDataFunction " + System.Text.Encoding.Default.GetString(dataArray));

            if (length == 0 && dataArray[0] != '#')
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                return;
            }

            if (length == 1 && dataArray[0] == '#')
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                return;
            }


            //MessageBox.Show("on off process data");
            for (int i = 0; i < length; i++) //
            {
                if (i == 0 && dataArray[i] != '#')
                {
                    break;
                }

                if (i == 1 && dataArray[i] == '#')
                {
                    break;
                }


                if (dataArray[i] == '#')
                {
                    if (dataArray[i + 1] == OnStartingByte)
                    {
                        if (dataArray[i + 3] == FirsTime)
                        {
                            //MessageBox.Show("first time on (BaseData)");
                            await ElementsUtil.ApplicationConsole.WriteLine("OnStartingByte (first fime): " + System.Text.Encoding.Default.GetString(dataArray));
                            await ElementsUtil.InformOnNewData(new FirstTimeOnData());
                            return;
                        }
                        //await ElementsUtil.ApplicationConsole.WriteLine("OnStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                        //await ElementsUtil.InformOnNewData(new OnData());
                        //return;
                    }

                    if (dataArray[i + 1] == OffStartingByte)
                    {
                        
                        if (dataArray[i + 3] == FirsTime)
                        {
                            //MessageBox.Show("first time off (BaseData)");
                            await ElementsUtil.ApplicationConsole.WriteLine("OffStartingByte (first fime): " + System.Text.Encoding.Default.GetString(dataArray));
                            await ElementsUtil.InformOnNewData(new FirstTimeOffData());
                            return;
                        }
                        //else if(length == 2)
                        //{
                        //    await ElementsUtil.ApplicationConsole.WriteLine("OffStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                        //    await ElementsUtil.InformOnNewData(new OffData());
                        //    return;
                        //}
                       
                    }

                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
                    byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                    return;
                }
            }

            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: OnOffBaseData [class]. Requesting data again...");
            byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
        }
    }
}
