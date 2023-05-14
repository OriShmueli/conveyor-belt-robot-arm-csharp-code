using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.SeralPortCommunication;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Distance_Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Pickup;
using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using System.Threading;
using ConveyorBelt_RobotArm.Components.Program_States;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class RobotPickupState : RobotArmBaseState
    {
        public RobotPickupState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }
        private bool _pickupStage1EndFirstTime = false; 
        private bool _pickupStage2EndFirstTime = false;
        CancellationTokenSource StageblinkCancellationToken1 = new CancellationTokenSource();
        CancellationTokenSource StageblinkCancellationToken2 = new CancellationTokenSource();
        private async Task ElementsUtil_onNewDataRecieved(object sender, ReceiveBaseDataProtocol args)
        {
            if(args is PickupStage)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Pickup State");
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupStartStage1);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.PickupStateStage1Blink(StageblinkCancellationToken1.Token));
                    //await ElementsUtil.RobotArm.PickupStateStage1Blink(StageblinkCancellationToken.Token);
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }
                //await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed (Send) Robot Start Stage 1");
                return;
            }

            if (args is PickupStage1Start)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot End Stage 1");
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupEndStage1);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                return;
            }

            if (args is PickupStage1End)
            {

                //await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot Start Stage 2");
                StageblinkCancellationToken1.Cancel();    
                await ElementsUtil.RobotArm.PickupStateStage1On();
                await ElementsUtil.RobotArm.PickupStateArrowOn();
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.PickupStateStage2Blink(StageblinkCancellationToken2.Token));
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }

                //stage 1 stop blink
                //draw green arrow to stage 2
                //StageblinkCancellationToken1 = new CancellationTokenSource();
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupStartStage2);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed (Send) Robot Start Stage 2");
                //if (!_pickupStage1EndFirstTime)
                //{
                //    StageblinkCancellationToken1.Cancel();
                //    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupEndStage1);
                //    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //    await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot End Stage 1");
                //    _pickupStage1EndFirstTime = true;
                //}
                //else
                //{

                //    StageblinkCancellationToken1 = null;
                //    await ElementsUtil.RobotArm.PickupStateStage1On();
                //    await ElementsUtil.RobotArm.PickupStateArrowOn();
                //    //stage 1 stop blink
                //    //draw green arrow to stage 2
                //    StageblinkCancellationToken1 = new CancellationTokenSource();
                //    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupStartStage2);
                //    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //}
                //return;
            }

            if (args is PickupStage2Start)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot Start Stage 2");
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupEndStage2);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                return;
            }

            if (args is PickupStage2End)
            {
                
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot End Stage 2");
                StageblinkCancellationToken2.Cancel();
                //await Task.Delay(100);
                //await ElementsUtil.RobotArm.PickupStateStage2On();
                //stage 2 stop blink
                //draw green arrow to next
                //check sensore data 
                if (ElementsUtil.RobotArm.Sensor1.GetPackage() is BlackAndYellowPackage)
                {
                    //switch to black package path
                    
                    await ElementsUtil.RobotArm.BlackAndYellowStartPathOn();
                    await SwitchState(await _factory.RotateTo());
                    return;
                }

                if (ElementsUtil.RobotArm.Sensor1.GetPackage() is WhiteOrMagneticPackage)
                {
                    //switch to sensore 2 state
                    
                    await ElementsUtil.RobotArm.Sensore2ArrowOn();
                    await SwitchState(await _factory.Sensor2());
                    return;
                }

                if(args is PickupStage)
                {
                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.EnterRobotArmPickupState);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                }
                //if (!_pickupStage2EndFirstTime)
                //{
                //    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RobotArmPickupEndStage2);
                //    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //    await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Robot End Stage 2");
                //    _pickupStage2EndFirstTime = true;
                //}
                //else
                //{
                //    StageblinkCancellationToken2.Cancel();
                //    await Task.Delay(100);
                //    await ElementsUtil.RobotArm.PickupStateStage2On();
                //    //stage 2 stop blink
                //    //draw green arrow to next
                //    //check sensore data 
                //    if (ElementsUtil.RobotArm.Sensor1.GetPackage() is BlackAndYellowPackage)
                //    {
                //        //switch to black package path
                //        await ElementsUtil.RobotArm.BlackAndYellowStartPathOn();
                //    }

                //    if(ElementsUtil.RobotArm.Sensor1.GetPackage() is WhiteOrMagneticPackage)
                //    {
                //        //switch to sensore 2 state
                //        await ElementsUtil.RobotArm.Sensore2ArrowOn();
                //    }
                //}
                return;
            }

            //await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Pickup State. Requesting data again...");
            //byte[] errdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errdata);
        }

        public override async Task EnterState()
        {
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ArmMovement);

            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved; 
            });

            await ElementsUtil.RobotArm.PickupToArrowOn();
            await ElementsUtil.RobotArm.StopConveyorBeltGifAnimation();
            await ElementsUtil.RobotArm.ConveyorBeltPathOff();
            //await Task.Delay(1000);
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.EnterRobotArmPickupState);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        public override async Task ApplyingData()
        {

        }

        public override async Task ExitState()
        {
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ConveyerBelt);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
