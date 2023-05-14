using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using ConveyorBelt_RobotArm.Data_Base.Sensors;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm.States.Sensor2;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Magnetic_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm.States
{
    internal class RobotSensor2State : RobotArmBaseState
    {
        public RobotSensor2State(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory) : base(stateMachine, robotArmFactory)
        {
        }

        //private async Task ElementsUtil_onNewDistanceData(SeralPortCommunication.Receive.Sensors.Distance_Sensor.DistanceSensorsProcessorData args)
        //{
            
        //}

        private async Task ElementsUtil_onNewDataRecieved(object sender, SeralPortCommunication.ReceiveBaseDataProtocol args)
        {
            if (args is Sensor2EnterData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Confirmed Sensor 2 State");
               
                byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Sensor2Leave);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
            }

            if(args is Sensor2LeaveData)
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("agneticActivatedThisRound: " + ElementsUtil.SensorBaseData.MagneticActivatedThisRound.ToString());
                if (ElementsUtil.SensorBaseData.MagneticActivatedThisRound)
                {
                    await ElementsUtil.RobotArm.Sensor2MagneticAndBlue();
                    ElementsUtil.RobotArm.Sensor2.SetPackage(new BlueAndMagneticPackage());
                }
                else
                {
                    await ElementsUtil.RobotArm.Sensor2white();
                    ElementsUtil.RobotArm.Sensor2.SetPackage(new WhitePackage());
                }
                
                await SwitchState(await _factory.RotateTo());
            }
        }

        public override Task ApplyingData()
        {
            throw new NotImplementedException();
        }

        public override async Task EnterState()
        {
            await ElementsUtil.ProgramStates.EnterState(GraphicStateUtil.Sensores);
            await Task.Run(() => {
                //ElementsUtil.onNewDistanceData += ElementsUtil_onNewDistanceData; 
                ElementsUtil.onNewDataRecieved += ElementsUtil_onNewDataRecieved; 
            });

            byte[] pdata = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Sensor2Enter);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, pdata);
        }

    
        public override async Task ExitState()
        {
            await Task.Run(() => {
                ElementsUtil.SensorBaseData.MagneticActivatedThisRound = false;
                //ElementsUtil.onNewDistanceData -= ElementsUtil_onNewDistanceData;
                ElementsUtil.onNewDataRecieved -= ElementsUtil_onNewDataRecieved;
            });
        }
    }
}
