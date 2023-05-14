using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Magnetic_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Tracking_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors
{
    internal class SensorBaseData : ReceiveBaseDataProtocol
    {
        public static new char StartingByte { get; private set; } = 'n';
        public static new int BufferLength { get; private set; } = 20;
        public static char DetermineByte { get; private set; } = 'l';


        private char _sensorActivated = 'a'; 
        private char _sensorDeactivated = 'd'; 
        private char _trackingSensorActivated = 't'; 
        private char _trackingSensoreDeactivated = 't'; 
        private char _magneticSensoreActivated = 'm'; 
        private char _magneticSensoreDeactivated = 'm';

        public bool MagneticActivatedThisRound = false;
        public SensorBaseData _currentMagneticState = null;
        public SensorBaseData _currentTrackingState = null;

        bool magneticSensoreFirstTime = true;
        bool trackingSensoreFirstTime = true;
        
        public override int GetBufferLength()
        {
            return BufferLength;
        }

        public override char GetStatingByte()
        {
            return StartingByte;
        }

        public async Task ProcessMagneticData(int data)
        {
            if (data == 1)
            {
                if (magneticSensoreFirstTime)
                {
                    magneticSensoreFirstTime=false;
                }
                else
                {
                    MagneticActivatedThisRound = false;
                    if (_currentMagneticState is MagneticSensorDeactivated)
                    {
                        return;
                    }
                    else
                    {
                        _currentMagneticState = new MagneticSensorDeactivated();
                        await ElementsUtil.InformOnNewData(_currentMagneticState);
                        return;
                    }
                }
               
                
            }

            if (data == 0)
            {
                if (magneticSensoreFirstTime)
                {
                    magneticSensoreFirstTime = false;
                }
                else
                {
                    MagneticActivatedThisRound = true;
                    if (_currentMagneticState is MagneticSensorActivated)
                    {
                        return;
                    }
                    else
                    {
                        _currentMagneticState = new MagneticSensorActivated();
                        await ElementsUtil.InformOnNewData(_currentMagneticState);
                        return;
                    }
                }

            }
        }

        public async Task ProcessTrackingData(int data)
        {
            if (data == 1)
            {
                if (trackingSensoreFirstTime)
                {
                    _currentTrackingState = new TrackingSensorDeactivated();
                    await ElementsUtil.InformOnNewData(_currentTrackingState);
                    trackingSensoreFirstTime = false;
                }
                else
                {
                    if (_currentTrackingState is TrackingSensorDeactivated)
                    {
                        return;
                    }
                    else
                    {
                        _currentTrackingState = new TrackingSensorDeactivated();
                        await ElementsUtil.InformOnNewData(_currentTrackingState);
                        return;
                    }
                }
               
            }

            if (data == 0)
            {
                if (trackingSensoreFirstTime)
                {
                    _currentTrackingState = new TrackingSensorActivated();
                    await ElementsUtil.InformOnNewData(_currentTrackingState);
                    trackingSensoreFirstTime = false;
                }
                else
                {
                    if (_currentTrackingState is TrackingSensorActivated)
                    {
                        return;
                    }
                    else
                    {

                        _currentTrackingState = new TrackingSensorActivated();
                        await ElementsUtil.InformOnNewData(_currentTrackingState);
                        return;
                    }
                }
                
            }
        }

        public override async Task ProcessData(byte[] dataArray, int length)
        {
            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Sensors [class]. Requesting data again...");
            byte[] errData = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.Error);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, errData);
            /*if (length == 0)
            {
                return;
            }

            for (int i = 0; i < length; i++) //
            {
                if (i == 0 && dataArray[i] != partition)
                {
                    return;
                }

                if(dataArray[i] == partition)
                {
                    if(dataArray[i+1] == _sensorActivated)
                    {
                        if (dataArray[i+3] == _trackingSensorActivated)
                        {
                            await ElementsUtil.InformOnNewData(new TrackingSensorActivated());
                            return;
                        }

                        if (dataArray[i+3] == _magneticSensoreActivated)
                        {
                            await ElementsUtil.InformOnNewData(new MagneticSensorActivated());
                            return;
                        }
                    }

                    if (dataArray[i + 1] == _sensorDeactivated)
                    {
                        if (dataArray[i + 3] == _trackingSensoreDeactivated)
                        {
                            await ElementsUtil.InformOnNewData(new TrackingSensorDeactivated());
                            return;
                        }

                        if (dataArray[i + 3] == _magneticSensoreDeactivated)
                        {
                            await ElementsUtil.InformOnNewData(new MagneticSensorDeactivated());
                            return;
                        }
                    }
                }
            }*/
        }
    }
}
