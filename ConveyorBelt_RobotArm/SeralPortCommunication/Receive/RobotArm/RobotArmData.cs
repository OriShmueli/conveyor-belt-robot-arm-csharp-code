using ConveyorBelt_RobotArm.Components.Robot_Arm.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.Conveyor_Belt;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.RobotState;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Black_And_Yellow;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.End;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Magnetic_And_Blue;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Pickup;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.RotateFrom;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.RotateTo;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Sensor2;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.White;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm
{
    public class RobotArmData : ReceiveBaseDataProtocol
    {
        private int _bufferLength = 30;
        private char _startingByte = 'a';
        private bool _pickupStage = true;
        public override int GetBufferLength()
        {
            return _bufferLength;
        }

        public override char GetStatingByte()
        {
            return _startingByte;
        }

        private async Task Error(int length)
        {
            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class] {data length is [" + length + "]}. Requesting data again...");
            byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            string dataMessage = System.Text.Encoding.Default.GetString(dataArray);

            if (dataMessage == "#a")
            {
                await ElementsUtil.InformOnNewData(new RobotStateData());
                return;
            }

            //await Task.Delay(100);
            #region End State
            if(dataMessage == "#s#e#n")
            {
                
                await ElementsUtil.InformOnNewData(new EndStateData());
                return;
            }
            #endregion
            if (dataMessage == "a#s#e#n")
            {
                await ElementsUtil.InformOnNewData(new EndStateData());
                return;
            }
            #region Black And Yellow

            if (dataMessage == "#s#u#p#y")
            {
                await ElementsUtil.InformOnNewData(new BlackAndYellowPathData());
                return;
            }

            if (dataMessage == "#s#u#t#a#p#y")
            {
                await ElementsUtil.InformOnNewData(new BlackAndYellowStage1StartData());
                return;
            }

            if (dataMessage == "#s#u#n#a#p#y")
            {
                await ElementsUtil.InformOnNewData(new BlackAndYellowStage1EndData());
                return;
            }

            if (dataMessage == "#s#u#t#b#p#y")
            {
                await ElementsUtil.InformOnNewData(new BlackAndYellowStage2StartData());
                return;
            }

            if (dataMessage == "#s#u#n#b#p#y")
            {
                await ElementsUtil.InformOnNewData(new BlackAndYellowStage2EndData());
                return;
            }

            #endregion

            #region Magnetic And Blue

            if (dataMessage == "#s#u#p#m")
            {
                await ElementsUtil.InformOnNewData(new MagneticAndBluePathData());
                return;
            }

            if (dataMessage == "#s#u#t#a#p#m")
            {
                await ElementsUtil.InformOnNewData(new MagneticAndBlueStage1StartData());
                return;
            }

            if (dataMessage == "#s#u#n#a#p#m")
            {
                await ElementsUtil.InformOnNewData(new MagneticAndBlueStage1EndData());
                return;
            }

            if (dataMessage == "#s#u#t#b#p#m")
            {
                await ElementsUtil.InformOnNewData(new MagneticAndBlueStage2StartData());
                return;
            }

            if (dataMessage == "#s#u#n#b#p#m")
            {
                await ElementsUtil.InformOnNewData(new MagneticAndBlueStage2EndData());
                return;
            }

            #endregion

            #region White Path

            if (dataMessage == "#s#u#p#w")
            {
                await ElementsUtil.InformOnNewData(new WhitePathData());
                return;
            }

            if(dataMessage == "#s#u#t#a#p#w")
            {
                await ElementsUtil.InformOnNewData(new WhiteStage1StartData());
                return;
            }

            if(dataMessage == "#s#u#n#a#p#w")
            {
                await ElementsUtil.InformOnNewData(new WhiteStage1EndData());
                return;
            }

            if(dataMessage == "#s#u#t#b#p#w")
            {
                await ElementsUtil.InformOnNewData(new WhiteStage2StartData());
                return;
            }

            if(dataMessage == "#s#u#n#b#p#w")
            {
                await ElementsUtil.InformOnNewData(new WhiteStage2EndData());
                return;
            }
            #endregion

            #region Rotate From
            if(dataMessage == "#r#f")
            {
                await ElementsUtil.InformOnNewData(new RotateFromData());
                return;
            }

            if(dataMessage == "#r#f#n")
            {
                await ElementsUtil.InformOnNewData(new RotateFromEndData());
                return;
            }


            if (dataMessage == "#r#f#w")
            {
                await ElementsUtil.InformOnNewData(new RotateFromWhiteData());
                return;
            }


            if (dataMessage == "#r#f#m")
            {
                await ElementsUtil.InformOnNewData(new RotateFromMagneticAndBlueData());
                return;
            }


            if (dataMessage == "#r#f#y")
            {
                await ElementsUtil.InformOnNewData(new RotateFromBlackAndYellowData());
                return;
            }
            #endregion

            #region Rotate To
            if (dataMessage == "#r#t#m")
            {
                await ElementsUtil.InformOnNewData(new RotateToMagneticAndBlueData());
                return;
            }

            if (dataMessage == "#r#t#y")
            {

                await ElementsUtil.ApplicationConsole.WriteLineRed("R T Y");
                await ElementsUtil.InformOnNewData(new RotateToBlackAndYellowData());
                return;
            }

            if (dataMessage == "#r#t#w")
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("R T W");
                await ElementsUtil.InformOnNewData(new RotateToWhiteData());
                return;
            }

            if (dataMessage == "#r#t")
            {
                await ElementsUtil.InformOnNewData(new RotateToData());
                return;
            }

            #endregion

            if (dataMessage == "#s#s")
            {
                await ElementsUtil.InformOnNewData(new Sensor2Data());
                return;
            }

            if(dataMessage == "#s#e")
            {
                await ElementsUtil.InformOnNewData(new Sensor2EnterData());
                return;
            }

            if (dataMessage == "#s#l")
            {
                await ElementsUtil.InformOnNewData(new Sensor2LeaveData());
                return;
            }


            if (dataMessage == "#c#c") //dataArray[1] == 'c' && dataArray[3] == 'c'
            {
                await ElementsUtil.InformOnNewData(new ConveyorBeltData());
                return;
            }

            if (dataMessage == "#c#t") //dataArray[1] == 'c' && dataArray[3] == 't'
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: #c#t");
                await ElementsUtil.InformOnNewData(new ConveyorBeltStartData());
                return;
            }

            if (dataMessage == "#c#s") //dataArray[1] == 'c' && dataArray[3] == 's'
            {
                await ElementsUtil.InformOnNewData(new ConveyorBeltStopData());
                return;
            }

            if (length == 0 || length == 1)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class] {data length is [" + length + "]}. Requesting data again...");
                byte[] cerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, cerrData);
                return;
            }

            if(length < 4)
            {
                if (dataArray[1] == 'c')
                {
                    
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class] {data length is [" + length + "]}. Requesting data again...");
                    byte[] cerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, cerrData);
                    return;
                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class] {data length is [" + length + "]}. Requesting data again...");
                    byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
                    return;
                }
             
            }

            if(dataMessage == "s#u#")
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class] {data length is [" + length + "]}. Requesting data again...");
                byte[] cerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, cerrData);
                return;
            }
            //#s#u#t#a
            if (dataMessage == "#s#u")
            {
                if(ElementsUtil.RobotArmStateMachine.CurrentState is RobotPickupState)
                {
                    await ElementsUtil.InformOnNewData(new PickupStage());
                    //_pickupStage = false;
                    return;
                }
                //if (_pickupStage)
                //{
                   
                //}
                //else
                //{
                //    await Error(length);
                //    return;
                //}
            }

            if(dataMessage == "#s#u#t#a")
            {
                if(ElementsUtil.RobotArmStateMachine.CurrentState is RobotPickupState)
                {
                    await ElementsUtil.InformOnNewData(new PickupStage1Start());
                    return;
                }
               
            }

            if(dataMessage == "#s#u#n#a")
            {
                if (ElementsUtil.RobotArmStateMachine.CurrentState is RobotPickupState)
                {

                    await ElementsUtil.InformOnNewData(new PickupStage1End());
                    return;
                }
            }

            if(dataMessage == "#s#u#t#b")
            {
                await ElementsUtil.InformOnNewData(new PickupStage2Start());
                return;
            }

            if(dataMessage == "#s#u#n#b")
            {
                await ElementsUtil.InformOnNewData(new PickupStage2End());
                return;
            }
            #region pickupstates
            //if (dataArray[1] == 's' && dataArray[3] == 'u')
            //{
            //    if (length == 4)
            //    {
            //        if (_pickupStage)
            //        {
            //            await ElementsUtil.InformOnNewData(new PickupStage());
            //            _pickupStage = false;
            //            return;
            //        }
            //        else
            //        {
            //            await Error(length);
            //            return;
            //        }                   
            //    }
            //    if (length < 5)
            //    {
            //        await Error(length);
            //        return;
            //    }

            //    if (dataArray[5] == 't')
            //    {
            //        if (length < 8)
            //        {
            //            await Error(length);
            //            return;
            //        }
            //        if (dataArray[7] == 'a')
            //        {
            //            await ElementsUtil.InformOnNewData(new PickupStage1Start());
            //            return;
            //        }

            //        if (dataArray[7] == 'b')
            //        {
            //            await ElementsUtil.InformOnNewData(new PickupStage2Start());
            //            return;
            //        }
            //        await Error(length);
            //        return;
            //    }


            //    if (dataArray[5] == 'n')
            //    {
            //        if(length < 8)
            //        {
            //            await Error(length);
            //            return;
            //        }
            //        if (dataArray[7] == 'a')
            //        {
            //            await ElementsUtil.InformOnNewData(new PickupStage1End());
            //            return;
            //        }

            //        if (dataArray[7] == 'b')
            //        {
            //            await ElementsUtil.InformOnNewData(new PickupStage2End());
            //            return;
            //        }
            //        await Error(length);
            //        return;
            //    }

            //    await Error(length);
            //    return;

            //}

            #endregion


            //await Task.Yield();
            
            if (length < 28 || length > 28)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class]. Requesting data again...");
                byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error); //RequestArmStartPositionsError
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
                return;
            }
            GetArmPositionsData getArmPositionsData = new GetArmPositionsData();
            for (int i = 0; i < length; i++)
            {
                if (dataArray[i] == partition)
                {
                    
                    if(dataArray[i + 2] == '<')
                    {
                        await ElementsUtil.ApplicationConsole.WriteLine("Length: " + length + "id: " + ((int)(char)dataArray[i + 1] - '0').ToString() + ". position: " + ConvertByteArrayToIntByOffset(dataArray, i + 3).ToString());
                        getArmPositionsData.initialize((int)(char)dataArray[i + 1] - '0', ConvertByteArrayToIntByOffset(dataArray, i + 3));
                    }
                }
            }

            if (await getArmPositionsData.chackIfAllDataPass())
            {
                await ElementsUtil.ApplicationConsole.WriteLine("ReadingRobotActivatedPositionsByte: " + System.Text.Encoding.Default.GetString(dataArray));
                //await Task.Delay(100);
                await ElementsUtil.InformOnNewData(getArmPositionsData);
                return;
            }
            else
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class]. Requesting data again...");
                byte[] merrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, merrData);
                return;
            }

            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: RobotArmData [class]. Requesting data again...");
            byte[] mainerrData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, mainerrData);
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
