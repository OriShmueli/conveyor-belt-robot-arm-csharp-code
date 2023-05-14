using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States
{
    internal class StateData : ReceiveBaseDataProtocol
    {
        private int _bufferLength = 20;
        private char _statingByte = 's';

        public override int GetBufferLength()
        {
            return _bufferLength;
        }

        public override char GetStatingByte()
        {
            return _statingByte;
        }

        //private async Task<bool> sendEvent(char determineByte, string consoleMessage, StateData stateData, char currentByte, List<>)
        //{
        //    for (int i = 0; i < length; i++)
        //    {

        //    }
        //}

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            //List<char> stateData = new List<char>();
            //stateData.Add(ActivatingStateData.DetermineByte);
            //stateData.Add(DeactivatingStateData.DetermineByte);
            //stateData.Add(EditingStateData.DetermineByte);
           
            //if(SendBaseDataProtocol.requ)

            if (length == 0)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: States (Activating/Deactivating) [class] [" + length + "]. Requesting data again...");
                byte[] aerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, aerrData);
                return;
            }

            string dataMessage = System.Text.Encoding.Default.GetString(dataArray);
            if (dataMessage == "#a")
            {
                await ElementsUtil.ApplicationConsole.WriteLine("ActivatingStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                await ElementsUtil.InformOnNewData(new ActivatingStateData());
                return;
            }

            if (dataMessage == "#d")
            {
                await ElementsUtil.ApplicationConsole.WriteLine("DeactivatingStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                await ElementsUtil.InformOnNewData(new DeactivatingStateData());
                return;
            }

            if (length == 2)
            {
                for (int i = 0; i < length; i++)
                {
                    if (dataArray[i] == partition)
                    {
                        if (dataArray[i+1] == ActivatingStateData.DetermineByte)
                        {
                            //if(ElementsUtil.StateMachine.CurrentState is ActivatingState)
                            //{
                            //    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: States (Activating/Deactivating) [class]. Requesting data again...");
                            //    byte[] aerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                            //    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, aerrData);
                            //    return;
                            //}
                            await ElementsUtil.ApplicationConsole.WriteLine("ActivatingStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                            await ElementsUtil.InformOnNewData(new ActivatingStateData());
                            return;
                        }

                        //if (dataArray[i+1] == EditingStateData.DetermineByte)
                        //{
                        //    await ElementsUtil.ApplicationConsole.WriteLine("EditingStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                        //    await ElementsUtil.InformOnNewData(new EditingStateData());
                        //    return;
                        //}

                        if (dataArray[i + 1] == DeactivatingStateData.DetermineByte)
                        {
                            await ElementsUtil.ApplicationConsole.WriteLine("DeactivatingStartingByte: " + System.Text.Encoding.Default.GetString(dataArray));
                            await ElementsUtil.InformOnNewData(new DeactivatingStateData());
                            return;
                        }
                    }
                }

                //for (int i = 0; i < stateData.Count; i++)
                //{
                //    //if(dataArray[1] == stateData[i])
                //    //{
                //    //    await ElementsUtil.ApplicationConsole.WriteLine(stateData[i].GetType().Name + ": " + System.Text.Encoding.Default.GetString(dataArray));
                //    //    await ElementsUtil.InformOnNewData(new OnData());
                //    //    return;
                //    //}


                //}
                return;
            }
            else
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: States (Activating/Deactivating) [class]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
            }

        }
    }
}
