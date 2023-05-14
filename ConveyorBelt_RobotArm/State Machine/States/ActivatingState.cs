using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class ActivatingState : BaseState
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public ActivatingState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
            Task.Run(async () =>
            {
                
                try
                {
                    await ElementsUtil.RobotArm.BlinkOnOffIcon(cancellationTokenSource.Token);
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }
            });
        }
        
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is ActivatingStateData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Arduino Activating State");
                await ElementsUtil.ApplicationConsole.WriteLineGreen(" ");
                await ElementsUtil.ApplicationConsole.WriteLineGreen("          Arduino Activated        ");
                await ElementsUtil.ApplicationConsole.WriteLineGreen(" ");
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.RequestArmStartPositions);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            } 

            if(args is GetArmPositionsData)
            {
                GetArmPositionsData armPositions = (GetArmPositionsData)args;
                //await ElementsUtil.ArmSettings.EnterRecordingState(true);
                ElementsUtil.ArmSettings.GetStartArmPositionsData = armPositions;
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedOnData);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                //await ElementsUtil.ArmSettings.SetArmStartingPositions(armPositions.ArmIdAndPositions);
            }

            if(args is OnData)
            {
                cancellationTokenSource.Cancel();
                await ElementsUtil.RobotArm.ApplicationOn();
                if (ElementsUtil.Request.GetRequest() is RequestEditing)
                {
                    await ElementsUtil.RobotArm.StartPanelEditing();
                }
                else
                {
                    await ElementsUtil.RobotArm.StartPanelOn();
                    await ElementsUtil.ArmSettings.EnterRecordingState(false);
                }

                //await ElementsUtil.ProgramStates.HideAfterExitState(GraphicStateUtil.FirstTimeOnOff);
                await SwitchState(await _factory.On());
            }
        }

        public override async Task ApplyingData()
        {
            
        }

        public override async Task EnterState()
        {
            List<Task> eventAndBlinkTasks = new List<Task>();

            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendActivatingState);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            await ElementsUtil.ApplicationConsole.WriteLineRed(" ");
            await ElementsUtil.ApplicationConsole.WriteLineRed("        Wait For Arduino To Activate    ");
            await ElementsUtil.ApplicationConsole.WriteLineRed(" ");
            

            


        }

        public override async Task ExitState()
        {
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });          
        }

        //public override Task SendLastMessage()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
