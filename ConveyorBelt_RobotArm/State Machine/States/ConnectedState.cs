using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Connection;
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
    internal class ConnectedState : BaseState
    {
        public ConnectedState(StateMachine context, StateFactory stateFactory) : base(context, stateFactory)
        {
        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {

            if (args is ConnectedData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Connected");
            }
            
            if (args is DisconnectedData)
            {
                
                await SwitchState(await _factory.Disconnected());
            }

            if (args is FirstTimeOffData)
            {
                //MessageBox.Show("switching state: off first Time");
                await ElementsUtil.ProgramStates.FirstTimeOffChangeStateName();
                await SwitchState(await _factory.FirstTimeOff());
            }

            if (args is FirstTimeOnData)
            {
                //MessageBox.Show("switching state: on first Time");
                await ElementsUtil.ProgramStates.FirstTimeOnChangeStateName();
                await SwitchState(await _factory.FirstTimeOn());
            }
        }

        private async Task ElementsUtil_onEnableWritingData()
        {
            //await Task.Delay(1000);
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendConnectedState);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            //await DataProtocol.WriteData(ElementsUtil.MainSerialPort, SendBaseDataProtocol.SendConnectedState);
            await ElementsUtil.ApplicationConsole.WriteLine("Waiting for arduino...");
            //await Task.Delay(10);
            //await ElementsUtil.ApplicationConsole.WriteLine("Sent: " + System.Text.Encoding.Default.GetString(data));
            //await ElementsUtil.ApplicationConsole.WriteLine("Sent: " + SendBaseDataProtocol.SendConnectedState);

        }

        public override async Task ApplyingData()
        {
        }

        public override async Task EnterState()
        {

            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
                ElementsUtil.onEnableWritingData += ElementsUtil_onEnableWritingData;

            });

           

        }

       

        public override async Task ExitState()
        {
            await Task.Run(() =>
            {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
                ElementsUtil.onEnableWritingData -= ElementsUtil_onEnableWritingData;
                //ElementsUtil.onEnableWritingData -= ElementsUtil_onEnableWritingData;
            });
        }
    }
}
