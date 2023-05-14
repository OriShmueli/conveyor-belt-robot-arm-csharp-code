using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.Conveyor_Belt;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Pickup;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Distance_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Magnetic_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Tracking_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class ConveyorBeltState : RobotArmBaseState
    {
        public ConveyorBeltState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }

        private bool _detectedOnStart = false;
        private bool _trackingSensorActivated = false;
        private bool _packageDetected = false;
        private bool _confimeConveyorBeltState = false;

        //private async Task RunConveyorBelt(SeralPortCommunication.ReceiveBaseDataProtocol args)
        //{
        //    while (!_packageDetected)
        //    {
        //        if (args is ConveyorBeltStartData)
        //        {

        //        }
        //        await Task.Delay(100);
        //    }
        //}

        int distance = 0;
        bool chackFirstDistance = true;
        private async Task ElementsUtil_onNewDistanceData(DistanceSensorsProcessorData args)
        {
            if (!_detectedOnStart)
            {
                _detectedOnStart = true;
                if (args.DistnaceSensorEnd <= 7)
                {
                    if (_trackingSensorActivated)
                    {
                        await ElementsUtil.RobotArm.Sensor1WhiteOrMagnetic();                        
                    }
                    else
                    {
                        await ElementsUtil.RobotArm.Sensor1BlackAndYellow();
                    }
                    await SwitchState(await _factory.Pickup());
                    return;
                }
                else
                {
                    await Task.Delay(3000);
                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestingToEnterConveyorBeltState);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                }
                
                return;
                
            }
            
            //if (chackFirstDistance)
            //{
            //    distance = args.DistnaceSensorEnd;
            //    chackFirstDistance = false;
            //}
            
            if (_confimeConveyorBeltState)
            {
                if (!_packageDetected)
                {
                    if (args.DistnaceSensorEnd <= 7)
                    {
                        
                        //stop conveyor belt
                        //await ElementsUtil.RobotArm.
                        _packageDetected = true;
                        await ElementsUtil.RobotArm.StopConveyorBeltGifAnimation();
                        await ElementsUtil.RobotArm.ConveyorBeltPathOff();

                        if (_trackingSensorActivated)
                        {
                            await ElementsUtil.RobotArm.Sensor1WhiteOrMagnetic();
                        }
                        else
                        {
                            await ElementsUtil.RobotArm.Sensor1BlackAndYellow();
                        }
                        byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ConveyorBeltStop);
                        await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);

                        return;
                    }
                }
            }                        
        }

        private bool _conveyorBeltstartedMessage = false;
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if (_detectedOnStart)
            {
                if (args is ConveyorBeltData)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Conveyor Belt State");

                    await ElementsUtil.RobotArm.ConveyorBeltPathOn();
                    await ElementsUtil.RobotArm.StartConveyorBeltGifAnimation();

                    //if (distance <= 13)
                    //{
                    //    _confimeConveyorBeltState = true;
                    //    return;
                    //}
                    //else
                    //{
                    //    byte[] adata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ConveyorBeltStart);
                    //    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, adata);
                    //}

                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ConveyorBeltStart);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);

                    //await Task.Factory.StartNew(() => RunConveyorBelt(args));

                }
            }
            

            if (args is TrackingSensorActivated)
            {

                _trackingSensorActivated = true;
            }

            if (args is TrackingSensorDeactivated)
            {
                _trackingSensorActivated = false;
            }

            if(args is ConveyorBeltStartData)
            {
                if (!_conveyorBeltstartedMessage)
                {

                    await ElementsUtil.ApplicationConsole.WriteLineGreen("Conveyor Belt Started");
                    _conveyorBeltstartedMessage =true;
                    _confimeConveyorBeltState = true;
                }

                /*if (!_packageDetected)
                {
                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ConveyorBeltStart);
                    await DataProtocol.WriteDataAsyncWithNoMessage(ElementsUtil.MainSerialPort, data);
                    await Task.Delay(100);
                }*/
            }

            if (args is ConveyorBeltStopData)
            {
                 
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Conveyor Belt Stoped");
                await SwitchState(await _factory.Pickup());

                //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestingToEnterConveyorBeltState);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            }

            if(args is PickupStage)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("PickupStage ERRRRRR");
            }
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            //if (ElementsUtil.Request.GetRequest() is RequestEditing)
            //{
            //    //await ElementsUtil.RobotArm.EndEditingToStartOn();
            //    //await ElementsUtil.ArmSettings.EnterRecordingState(true);
            //    await SwitchState(await _factory.Editing());
            //    return;
            //}
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.Sensores, true);
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ConveyerBelt, true);
            
            //await ElementsUtil.ProgramStates.AddActivatedComponent(GraphicStateUtil.Sensores);
            //await ElementsUtil.ProgramStates.ActivateStatePath(GraphicStateUtil.Sensores);
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.UpdatePackagesAmounts);
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ArmMovement);
            
            await ElementsUtil.ArmSettings.EnterRecordingState(true);
            
            //await ElementsUtil.RobotArm.ClearForNextPackage(); 
            //await ElementsUtil.RobotArm.EndBackToStartClear(); 
            await ElementsUtil.RobotArm.EndClear(); 
            await Task.Run(() => {
                ElementsUtil.onNewDistanceData += ElementsUtil_onNewDistanceData;
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            await ElementsUtil.RobotArm.Sensor1PathOn();

            //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestingToEnterConveyorBeltState);
            //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }


        public override async Task ExitState()
        {
            await Task.Run(() => {
                ElementsUtil.onNewDistanceData -= ElementsUtil_onNewDistanceData;
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
            await ElementsUtil.ApplicationConsole.WriteLineGreen("EXIT Belt Starte");           
        }
    }
}
