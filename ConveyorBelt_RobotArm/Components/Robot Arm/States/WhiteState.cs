using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Magnetic_And_Blue;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.White;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class WhiteState : RobotArmBaseState
    {
        public WhiteState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }

        CancellationTokenSource StageblinkCancellationToken1 = new CancellationTokenSource();
        CancellationTokenSource StageblinkCancellationToken2 = new CancellationTokenSource();
        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if (args is WhitePathData)
            { //start stage1
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed White State");
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.PathWhiteStage1Start);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.WhiteStateStage1Blink(StageblinkCancellationToken1.Token));
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }
            }

            if (args is WhiteStage1StartData)
            {
                await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.UpdatePackagesAmounts);
                StageblinkCancellationToken1.Cancel();
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.PathWhiteStage1End);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
            }

            if (args is WhiteStage1EndData)
            {
                await ElementsUtil.RobotArm.WhiteStateArrowOn();
                try
                {
                    await Task.Factory.StartNew(async () => await ElementsUtil.RobotArm.WhiteStateStage2Blink(StageblinkCancellationToken2.Token));
                }
                catch (AggregateException ae)
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed(ae.Message);
                }
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.PathWhiteStage2Start);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
            }

            if (args is WhiteStage2StartData)
            {
                await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ArmMovement);
                StageblinkCancellationToken2.Cancel();
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.PathWhiteStage2End);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
            }

            if (args is WhiteStage2EndData)
            {
                await ElementsUtil.RobotArm.WhiteToEndArrowOn();
                await SwitchState(await _factory.RotateFrom());
            }
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            //await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.ArmMovement);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved;
            });
            //draw line to stages box
            await ElementsUtil.RobotArm.WhiteToStagesArrowOn();
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.PathWhite);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }


        public override async Task ExitState()
        {
            await ElementsUtil.ProgramStates.ExitState(GraphicStateUtil.UpdatePackagesAmounts);
            await Task.Run(() => {
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
