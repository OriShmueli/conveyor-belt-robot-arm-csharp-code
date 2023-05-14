using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class RobotEditingState : RobotArmBaseState
    {
        public RobotEditingState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }
        GetArmPositionsData _currentArmPositionData = null;
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            //    if(args is EditRequestData)
            //    {
            //        byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingEnterState);
            //        await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            //        await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Edit State");
            //    }

            //    if(args is EditEnterStateData)
            //    {                
            //        //await ElementsUtil.ArmSettings.EnableEditingBar();
            //        //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingExitState);
            //        //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            //    }

            //    if(args is EditExitStateData)
            //    {
            //        //await ElementsUtil.ArmSettings.DisableEditingBar();
            //        //await SwitchState(await _factory.ConveyorBelt());
            //    }
            if (args is GetArmPositionsData)
            {
                GetArmPositionsData armPositions = (GetArmPositionsData)args;
                //await ElementsUtil.ArmSettings.EnterRecordingState(true);
                ElementsUtil.ArmSettings.GetStartArmPositionsData = armPositions;
                if(_currentArmPositionData == null)
                {
                    _currentArmPositionData = new GetArmPositionsData();
                    _currentArmPositionData.ArmIdAndPositions[0] = 999;
                    _currentArmPositionData.ArmIdAndPositions[1] = 999;
                    _currentArmPositionData.ArmIdAndPositions[2] = 999;
                    _currentArmPositionData.ArmIdAndPositions[3] = 999;
                }

                if(_currentArmPositionData.ArmIdAndPositions == armPositions.ArmIdAndPositions)
                {

                }
                else
                {
                    await ElementsUtil.ArmSettings.WritePositionsToBars();
                    _currentArmPositionData = armPositions;
                }
                

                await ElementsUtil.RobotArm.EnabelingCurrentEditingButton();
                //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedOnData);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //await ElementsUtil.ArmSettings.SetArmStartingPositions(armPositions.ArmIdAndPositions);
            }
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            await ElementsUtil.ArmSettings.EnableEditingBar();
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            //draw line to stages box
            //await ElementsUtil.RobotArm.MagneticAndBlueToStagesArrowOn();
            //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingStateRequest);
            //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }
      
        public override async Task ExitState()
        {
            await ElementsUtil.ArmSettings.DisableOrEnableEditingPanel(false); //(for safety)
            //await ElementsUtil.RobotArm.StartPanelOn();
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
