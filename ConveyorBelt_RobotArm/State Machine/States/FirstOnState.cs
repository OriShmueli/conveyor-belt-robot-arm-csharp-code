using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.State_Machine.States
{
    internal class FirstOnState : BaseState
    {
        public FirstOnState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            //MessageBox.Show("Pleas turn off");
            
            //if(args is OffData)
            //{
            //    await SwitchState(await _factory.Off());
            //}

            if (args is FirstTimeOffData)
            {
                await ElementsUtil.ProgramStates.FirstTimeOffChangeStateName();
                await SwitchState(await _factory.FirstTimeOff());
                //await SwitchState(await _factory.Off());
            }

            if (args is FirstTimeOnData) //confirmed first time on
            {
                
                await ElementsUtil.ApplicationConsole.WriteLineRed("Please Switch Off.");
            }

            if(args is OffData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: First Off State, Get[OffData]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
            }

            if(args is OnData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: First Off State, Get[OnData]. Requesting data again...");
                byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
            }
            //await Task.Run(() => {
            //    if (args is FirstTimeOnData)
            //    {
            //        //await SwitchState(_factory.FirstTimeOn());
            //        MessageBox.Show("Pleas turn off");
            //    }

            //});
            
            
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

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedFirstTimeOnData);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            //await ElementsUtil.ApplicationConsole.WriteLineRed("Please Switch Off.");
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
