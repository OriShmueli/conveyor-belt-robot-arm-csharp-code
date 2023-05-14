using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.RobotState;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class EditingArmSettingsState : BaseState
    {
        public EditingArmSettingsState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if (args is EditRequestData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Arduino Editing State");
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingEnterState);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                
            }
            
            if (args is EditEnterStateData)
            {
                
                if (ElementsUtil.RobotArmStateMachine == null)
                {
                    ElementsUtil.ActivateRobotArmStateMachine();
                }
                else
                {
                    await ElementsUtil.RobotArmStateMachine.SwitchState(await ElementsUtil.RobotArmStateMachine.StateFactory.Editing());
                }
            }

            if(args is EditContinueSendingData)
            {
                ElementsUtil.ArmSettings.ContinueSending = true;
            }

            if (args is EditExitStateData)
            {
                await SwitchState(await _factory.Robot());
            }
            //usless
            if(args is RobotStateData)
            {
                await SwitchState(await _factory.Robot());
            }
        }

        public override async Task ApplyingData()
        {

        }

        public override async Task EnterState()
        {
            
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ModifyingArmMovement, true);
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ActivatingDeactivating);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingStateRequest);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        public override async Task ExitState()
        {
            await ElementsUtil.RobotArm.StartPanelOn();
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
