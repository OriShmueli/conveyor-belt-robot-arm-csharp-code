using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.State_Machine.States
{
    internal class FirstOffState : BaseState
    {
        public FirstOffState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            //MessageBox.Show("resonse from event");
            //if (args is FirstTimeOnData)
            //{
            //    await SwitchState(_factory.FirstTimeOn());
            //}
            //MessageBox.Show("First time off");

            //await Task.Run(() =>
            //{

            //    //Invoke((MethodInvoker)delegate { });

            //});

            if(args is ActivatingStateData)
            {

                await ElementsUtil.ProgramStates.ActivatingChangeStateName();
                await SwitchState(await _factory.Activating());
            }

            if(args is OnData)
            {
                
                //await SwitchState(await _factory.On());
            }

            if(args is FirstTimeOffData)
            {
                
                firstTimeOff = true;
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed First Off State");
                await ElementsUtil.ApplicationConsole.WriteLine("");
                await ElementsUtil.ApplicationConsole.WriteLineGreen("     Switch on to activate the robot");
                await ElementsUtil.ApplicationConsole.WriteLine("");
                //await SwitchState(await _factory.Off());
            }

            if(args is OffData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: First Off State. Requesting data again...");
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            }
        }

        public override async Task ApplyingData()
        {
        }

        public override async Task EnterState()
        {
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedFirstTimeOffData);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        public override async Task ExitState()
        {
            await ElementsUtil.ProgramStates.ActivateStatePath(GraphicStateUtil.FirstTimeOnOff);
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ActivatingDeactivating);
            
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }

    }
}
