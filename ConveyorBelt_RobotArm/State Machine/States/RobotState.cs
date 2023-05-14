using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.RobotState;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine.States
{
    internal class RobotState : BaseState
    {
        public RobotState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is RobotStateData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Arduino Robot State");
                

                if (ElementsUtil.RobotArmStateMachine == null)
                {
                    ElementsUtil.ActivateRobotArmStateMachine();
                }
                else
                {                   
                    await ElementsUtil.RobotArmStateMachine.SwitchState(await ElementsUtil.RobotArmStateMachine.StateFactory.ConveyorBelt());
                }                
            }

            if(args is EditEnterStateData)
            {
                await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ModifyingArmMovement);                
                await ElementsUtil.RobotArmStateMachine.SwitchState(await ElementsUtil.RobotArmStateMachine.StateFactory.ConveyorBelt());
            }           
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ModifyingArmMovement);
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestEnterRobotState);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        public override async Task ExitState()
        {
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
