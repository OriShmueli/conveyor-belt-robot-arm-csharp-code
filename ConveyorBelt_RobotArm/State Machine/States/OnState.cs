using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class OnState : BaseState
    {
        public OnState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if (args is OffData)
            {
                await SwitchState(await _factory.Off());
            }

            if(args is OnData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Arduino On State");
                //await ElementsUtil.ProgramStates.FirstTimeOnChangeStateName();
                await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.FirstTimeOnOff);


                //ElementsUtil.ActivateRobotArmStateMachine();
                if (ElementsUtil.Request.GetRequest() is RequestEditing)
                {
                    //await ElementsUtil.RobotArm.EndEditingToStartOn();
                    //await ElementsUtil.ArmSettings.EnterRecordingState(true);
                    await ElementsUtil.ApplicationConsole.WriteLineBlue("Switch state to: Editing State");
                    await SwitchState(await _factory.EditingRobotArm());

                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: Robot State");
                    await SwitchState(await _factory.Robot());
                }
                //await SwitchState(await _factory.Activating());
                //byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestEnterRobotState);
                //await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            }

            /*if(args is ActivatingStateData)
            {
                await SwitchState(await _factory.Activating());
            }*/

        }

        public override async Task ApplyingData()
        {
        }

        public override async Task EnterState()
        {
            await ElementsUtil.ProgramStates.OnChangeStateName();
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.OnOff);
            await ElementsUtil.ProgramStates.ActivateStatePath(GraphicStateUtil.FirstTimeOnOff);
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedOnData);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        public override async Task ExitState()
        {
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.WritingDataToArduino, true);
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.ActivatingDeactivating);

            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
