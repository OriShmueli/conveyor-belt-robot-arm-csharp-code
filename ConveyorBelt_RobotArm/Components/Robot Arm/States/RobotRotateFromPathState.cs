using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.RotateFrom;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.RotateTo;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class RobotRotateFromPathState : RobotArmBaseState
    {
        public RobotRotateFromPathState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }

        CancellationTokenSource minusBlink = new CancellationTokenSource();
        
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is RotateFromData)
            {
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.BlinkMinus(minusBlink.Token));
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }

                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Rotate From State");
               
                if (ElementsUtil.RobotArm.Sensor2.GetPackage() is WhitePackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromWhite);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }

                if (ElementsUtil.RobotArm.Sensor2.GetPackage() is BlueAndMagneticPackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromMagneticAndBlue);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }

                if (ElementsUtil.RobotArm.Sensor1.GetPackage() is BlackAndYellowPackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromBlackAndYellow);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }
            }

            if (args is RotateFromBlackAndYellowData)
            {
                minusBlink.Cancel();
                await SwitchState(await _factory.End());
                //byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromEnd);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);                
            }

            if (args is RotateFromMagneticAndBlueData)
            {
                minusBlink.Cancel();
                await SwitchState(await _factory.End());
                //byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromEnd);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);                
            }

            if (args is RotateFromWhiteData)
            {
                minusBlink.Cancel();
                await SwitchState(await _factory.End());
                //byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFromEnd);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);                
            }

            //if (args is RotateFromEndData)
            //{
            //    await SwitchState(await _factory.End());
            //}
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            //await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ArmMovement);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });

            byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateFrom);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
        }

        public override async Task ExitState()
        {
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
