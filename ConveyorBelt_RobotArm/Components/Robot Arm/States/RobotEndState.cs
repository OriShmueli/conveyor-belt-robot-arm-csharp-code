using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.Conveyor_Belt;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.End;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class RobotEndState : RobotArmBaseState
    {
        public RobotEndState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is EndStateData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed End State");
                //TODO: write here the requests logic but for now it is all ways on
                if (ElementsUtil.Request.GetRequest() is RequestOn)
                { await ElementsUtil.RobotArm.EndBackToStartOn();
                 await ElementsUtil.RobotArm.StartPanelOn();
                    await Task.Delay(2000);
                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestingToEnterConveyorBeltState);
                // await DataProtocol.WriteDataAsyncWithNoMessage(ElementsUtil.MainSerialPort, data);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                   


                    return;
                }

                if(ElementsUtil.Request.GetRequest() is RequestOff)
                {
                    
                    return;
                }

                if (ElementsUtil.Request.GetRequest() is RequestEditing)
                {
                    await ElementsUtil.RobotArm.EndEditingToStartOn();
                    await ElementsUtil.RobotArm.StartPanelEditing();
                    byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingStateRequest);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                    return;
                }
            }

            if(args is ConveyorBeltData)
            {                
                await SwitchState(await _factory.ConveyorBelt());
            }

            if(args is EditRequestData)
            {
                await ElementsUtil.StateMachine.SwitchState(await ElementsUtil.StateMachine.StateFactory.EditingRobotArm());
                //await SwitchState(await _factory.Editing());
            }
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            await ElementsUtil.RobotArm.RotateFromArrowOn();
            byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.EndState);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
        }

        

        public override async Task ExitState()
        {
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ArmMovement);

            await ElementsUtil.RobotArm.ClearForNextPackage();
            await ElementsUtil.RobotArm.EndBackToStartClear();
            await ElementsUtil.RobotArm.EndClear();
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
