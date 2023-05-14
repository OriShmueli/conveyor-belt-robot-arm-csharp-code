using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
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
    internal class RobotRotateToPathState : RobotArmBaseState
    {
        public RobotRotateToPathState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }
        CancellationTokenSource plusBlink = new CancellationTokenSource();
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is RotateToData)
            {
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.BlinkPlus(plusBlink.Token));
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Rotate To State");
                if(ElementsUtil.RobotArm.Sensor2.GetPackage() is WhitePackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateToWhite);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }

                if (ElementsUtil.RobotArm.Sensor2.GetPackage() is BlueAndMagneticPackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateToBlueAndMagnetic);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }

                if(ElementsUtil.RobotArm.Sensor1.GetPackage() is BlackAndYellowPackage)
                {
                    byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateToblackAndYellow);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                    return;
                }
            }

            if(args is RotateToBlackAndYellowData) {
                plusBlink.Cancel();
                await SwitchState(await _factory.BlackAndYellowState());
            }
            
            if(args is RotateToMagneticAndBlueData) {
                plusBlink.Cancel();
                await SwitchState(await _factory.MagneticAndBlue());
            }

            if(args is RotateToWhiteData) {
                plusBlink.Cancel();
                await SwitchState(await _factory.White());
            }
        }

        public override async Task ApplyingData()
        {
            
        }

        public override async Task EnterState()
        {
            //await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ArmMovement);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            if(ElementsUtil.RobotArm.Sensor1.GetPackage() is WhiteOrMagneticPackage)
            {

                await ElementsUtil.RobotArm.RotateToArrowOn();
            }

            byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RotateTo);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
            /*recived data from arduino: #s#u
Data Protocol: connectionRobotArm
Confirmed Pickup State
Sending data: a#s#u#t#a
. To Port:COM4
Confirmed (Send) Robot Start Stage 1
recived data from arduino: #s#u#n#a
Data Protocol: connectionRobotArm
Confirmed Robot End Stage 1
Sending data: a#s#u#t#b
. To Port:COM4
Confirmed (Send) Robot Start Stage 2
recived data from arduino: #s#u#n#b
Data Protocol: connectionRobotArm
Confirmed Robot End Stage 2
[Robot Arm] Switch state to: RobotRotateToPathState*/
        }

        public override async Task ExitState()
        {
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
