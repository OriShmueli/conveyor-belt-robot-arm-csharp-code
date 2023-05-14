using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class OffState : BaseState 
    {
        public OffState(StateMachine context, StateFactory stateFactory) :base(context, stateFactory)
        {

        }

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if(args is OnData)
            {
                await SwitchState(await _factory.On());
            }

            if(args is OffData)
            {
                if (!firstTimeOff)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Off State. Requesting data again...");
                    byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
                    await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
                    return;
                }
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Arduino Regular off");
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

            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.ReceivedOffData);
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
