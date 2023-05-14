using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.SeralPortCommunication;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Connection;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.Conveyor_Belt;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.RotateFrom;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ConveyorBelt_RobotArm
{
    internal static class DataProtocol
    {
        //Data Events of classes
        //Function to take all recived data. And inform the componnents if thier data changed

        public static async Task ClearBuffer(SerialPort serialPort)
        {
            await Task.Yield();
            string a = serialPort.ReadExisting();
            await ElementsUtil.ApplicationConsole.WriteLine("clearing buffer: " + a);
            await WriteData(serialPort, "non");
            serialPort.DiscardInBuffer();
        }

        public static async Task Read(SerialPort serialPort)
        {
            await Task.Run(() => {
                
            });
        }

        public static async Task ReadFirstByte(SerialPort serialPort)
        {
           
        }

        public delegate bool ProcessDataAndInform(byte[] byteArray, int length);

        public static async Task<bool> ProcessOnOffLength(byte[] byteArray, int FirstTimeOnOffLength) //UnpredictableLength
        {//length = 3 (in this case)
            //if(byteArray.Length > 3 || byteArray.Length < 2) 
            return await Task.Run(() => {
                
                if (byteArray.Length > FirstTimeOnOffLength || byteArray.Length < ElementsUtil.OnOffBaseData.GetBufferLength()) //OnOffBaseData.BufferLength = 2
                {
                    return false;
                }
                else
                {
                    return true;
                }
            });
            
        }

        public static async Task<bool> CheckPredictableLength(byte[] byteArray, int length)
        {
            return await Task.Run(() =>
            {
                //MessageBox.Show("length: " + length);
                if (byteArray.Length < length || byteArray.Length > length) //length = 17
                {
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }

        public static async Task ReadAsync(SerialPort serialPort, int length, ReceiveBaseDataProtocol baseDataProtocol)
        {
            try
            {
                byte[] buffer = new byte[length]; //17
                var data = await serialPort.BaseStream?.ReadAsync(buffer, 0, buffer.Length);
                var byteArray = new byte[data];
                Array.Copy(buffer, byteArray, data);
                await ElementsUtil.ApplicationConsole.WriteLine("recived data from arduino: " + System.Text.Encoding.Default.GetString(byteArray));
                await processDataByClass(byteArray, data, baseDataProtocol);
            }catch(IOException err)
            {
                //MessageBox.Show(err);
                await ElementsUtil.ApplicationConsole.WriteLineRed("ReadingDataAsync [class] IOException: " + err.Message);
            }
           
        }

        public static async Task processDataByClass(byte[] byteArray, int length, ReceiveBaseDataProtocol baseDataProtocol)
        {
            //await Task.Yield();
            //MessageBox.Show("ReadAsync()");


            if (byteArray[0] == 'a' && length == 1)
            {
                await ElementsUtil.InformOnNewData(new ConveyorBeltStartData());
                return;
            }

            if (byteArray[0] == 'o' && length == 1)
            {
                if (ElementsUtil.StateMachine.CurrentState is OnState)
                {
                    await ElementsUtil.InformOnNewData(new OnData());
                    return;
                }
            }

            if((byteArray.Length == 1 && byteArray[0] == '#') || (byteArray.Length == 1 && byteArray[0] != '#'))
            {
                //ElementsUtil.MainSerialPort.DiscardInBuffer();
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol [# 0/1] [class], line[129], length: ["+length+"]. Requesting data again...");
                byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                return;
            }

            //if (byteArray[0] == 'a')
            //{
            //    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol [a] [class]. Requesting data again...");
            //    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ConveyorBeltStop);
            //    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);                
            //    return;
            //}
            
            if (baseDataProtocol is OnOffBaseData onOffBaseData)
            {
                //TODO: make delegates

                //bool chack = await ProcessOnOffLength(byteArray, data);
                //await ElementsUtil.ApplicationConsole.WriteLine("onOffBaseData");
                if(length == 2) // data
                {
                    await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: onOffBaseData"); // Length: " + data);
                    await onOffBaseData.ProcessData(byteArray, length); // data
                    return;
                }
                //else
                //{
                //    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (onOffBaseData) [class]. Requesting data again...");
                //    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                //    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                //}

                bool chack = await CheckPredictableLength(byteArray, length); //data
                if (chack)
                {
                    await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: onOffFirstTimeBaseData");
                    await onOffBaseData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (onOffFirstTimeBaseData) [class], line[171], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }

            if (baseDataProtocol is SensorBaseData sensorBaseData)
            {
                if (ElementsUtil.StateMachine.CurrentState is OnState)
                {
                    await ElementsUtil.InformOnNewData(new OnData());
                    return;
                }
                if (ElementsUtil.StateMachine.CurrentState is ActivatingState)
                {
                    await ElementsUtil.InformOnNewData(new OnData());
                    return;
                }
                await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: sensorBaseData");
                bool check = await CheckPredictableLength(byteArray, sensorBaseData.GetBufferLength());
                if (check)
                {
                    await sensorBaseData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (sensorBaseData) [class], line[200], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }

            if (baseDataProtocol is ConnectionBaseData connectionBaseData)
            {
                await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: connectionBaseData");
                bool check = await CheckPredictableLength(byteArray, length); //data
                if (check)
                {
                    await connectionBaseData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    //await Task.Delay(1000);
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (connectionBaseData) [class], line[220], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }

            if(baseDataProtocol is RobotArmData robotArmData)
            {
                await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: connectionRobotArm");
                bool check = await CheckPredictableLength(byteArray, length); //data
                if (check)
                {
                    //await Task.Delay(1000);
                    await robotArmData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    //await Task.Delay(1000);
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (connectionRobotArm) [class], line[241], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }

            if (baseDataProtocol is StateData StateData)
            {
                if(ElementsUtil.StateMachine.CurrentState is OnState)
                {
                    await ElementsUtil.InformOnNewData(new OnData());
                    return;
                }

                await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: connectionStates");
                bool check = await CheckPredictableLength(byteArray, length); //data
                if (check)
                {
                    //await Task.Delay(1000);
                    await StateData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (connectionStates) [class], line[267], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }
            
            if(baseDataProtocol is EditData EditData) {
                await ElementsUtil.ApplicationConsole.WriteLine("Data Protocol: Edit State");
                bool check = await CheckPredictableLength(byteArray, length); //data
                if (check)
                {
                    //await Task.Delay(1000);
                    await EditData.ProcessData(byteArray, length); //data
                    return;
                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol (Edit State) [class], line[286], length: ["+length+"]. Requesting data again...");
                    byte[] errpData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await WriteDataAsync(ElementsUtil.MainSerialPort, errpData);
                    return;
                }
                return;
            }

            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: DataProtocol [class]. Requesting data again...");
            byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            await WriteDataAsync(ElementsUtil.MainSerialPort, errData);
        }

        public static async Task WriteData(SerialPort serialPort, string text)
        {
            ElementsUtil.StateMachine.CurrentState.LastMessage = text;
            await Task.Run(() =>
            {
                serialPort.WriteLine(text);

            });
            serialPort.WriteLine(text);
            await ElementsUtil.ApplicationConsole.WriteLine("Send to port in Write Data: " + text);
        }

        public static async Task WriteDataAsync(SerialPort serialPort, byte[] buffer)
        {
            if(System.Text.Encoding.Default.GetString(buffer) == SendBaseDataProtocol.Error)
            {
                string s = serialPort.ReadExisting();
                //await Task.Delay(100);
                //await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.SendingErrorResponse, true);
                //await ElementsUtil.ProgramStates.ShowError(GraphicStateUtil.SendingErrorResponse);
            }
            //await Task.Delay(100);
            ElementsUtil.StateMachine.CurrentState.LastMessage = System.Text.Encoding.Default.GetString(buffer);
            //await Task.Yield();
            if (ElementsUtil.MainSerialPort.IsOpen)
            {
                await serialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length);
            }
            
            await ElementsUtil.ApplicationConsole.WriteLine("Sending data: " + System.Text.Encoding.Default.GetString(buffer) + ". To Port:" + serialPort.PortName);
        }

        public static async Task WriteDataAsyncWithNoMessage(SerialPort serialPort, byte[] buffer)
        {
            //await Task.Delay(50);
            //await Task.Yield();
            ElementsUtil.StateMachine.CurrentState.LastMessage = System.Text.Encoding.Default.GetString(buffer);
            if (ElementsUtil.MainSerialPort.IsOpen)
            {
                await serialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        //public static async Task<bool> ProcessData()
        //{

        //}

        //public static async Task<int> ExtractBufferLength()
        //{

        //}

    }
}
