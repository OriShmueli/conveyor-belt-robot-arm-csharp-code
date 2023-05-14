using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConveyorBelt_RobotArm.Components;
using ConveyorBelt_RobotArm.Components.Robot_Arm;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Connection;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Edit;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Error;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Distance_Sensor;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using ConveyorBelt_RobotArm.State_Machine;

namespace ConveyorBelt_RobotArm
{
    internal static class ElementsUtil
    {
        private static SerialPort _mainSerialPort;
        private static SerialPort _distanceSensorsSerialPort;

        public static SerialPort MainSerialPort { get { return _mainSerialPort; } }
        public static SerialPort DistanceSensorsSerialPort { get { return _distanceSensorsSerialPort; } }

        public static void SetGlobalSerialPortsClasses(SerialPort mainSerialPort, SerialPort distanceSensorsSerialPort)
        {
            _mainSerialPort = mainSerialPort;
            _distanceSensorsSerialPort = distanceSensorsSerialPort;
        }

        private static StateMachine _statemachine = null;
        public static StateMachine StateMachine { get { return _statemachine; } }
        
        public static void ActivateStateMachine()
        {
            _statemachine = new StateMachine();
        }

        private static RobotArmStateMachine _robotArmStateMachine = null;
        public static RobotArmStateMachine RobotArmStateMachine { get { return _robotArmStateMachine; } }

        public static void ActivateRobotArmStateMachine()
        {
            _robotArmStateMachine = new RobotArmStateMachine();
        }

        private static Panel _robotArmPanel;
        private static Panel _armSettingsPanel;
        private static Panel _distanceSensoresPanel;
        private static Panel _programStatesPanel;
        private static Panel _applicationConsolePanel;
        private static Panel _packagesAmountsPanel;

        public static Panel RobotArmPanel { get { return _robotArmPanel; } }
        public static Panel ArmSettingsPanel { get { return _armSettingsPanel; } }
        public static Panel DistanceSensoresPanel { get { return _distanceSensoresPanel; } }
        public static Panel ProgramStatesPanel { get { return _programStatesPanel; } }
        public static Panel ApplicationConsolePanel { get { return _applicationConsolePanel; } }
        public static Panel PackagesAmountsPanel { get { return _packagesAmountsPanel; } }

        private static RobotArm _robotArm;
        private static ArmSettings _armSettings;
        private static DistanceSensore _distanceSensore1;
        private static DistanceSensore _distanceSensore2;
        private static ProgramStates _programStates;
        private static ApplicationConsole _applicationConsole;
        private static PackagesAmount _packagesAmounts;

        public static RobotArm RobotArm { get { return _robotArm; } }
        public static ArmSettings ArmSettings { get { return _armSettings; } }
        public static DistanceSensore DistanceSensore1 { get { return _distanceSensore1; } }
        public static DistanceSensore DistanceSensore2 { get { return _distanceSensore2; } }
        public static ProgramStates ProgramStates { get { return _programStates; } }
        public static ApplicationConsole ApplicationConsole { get { return _applicationConsole; } }
        public static PackagesAmount PackagesAmounts { get { return _packagesAmounts; } }

        public static void CreateElements(Panel robotArmPanel, Panel armSettingsPanel, Panel distanceSensoresPanel, Panel programStatesPanel, Panel applicationConsolePanel, Panel packagesAmountsPanel)
        {
            _robotArm = new RobotArm();
            RobotArm.Location = new Point(5, 5);
            _robotArmPanel = robotArmPanel;
            RobotArmPanel.Controls.Add(_robotArm);

            _armSettings = new ArmSettings();
            ArmSettings.Location = new Point(0, 1);
            _armSettingsPanel = armSettingsPanel;
            ArmSettingsPanel.Controls.Add(_armSettings);

            _distanceSensore1 = new DistanceSensore();
            DistanceSensore1.Location = new Point(10, 10);
            _distanceSensoresPanel = distanceSensoresPanel;
            DistanceSensoresPanel.Controls.Add(_distanceSensore1);

            _distanceSensore2 = new DistanceSensore();
            DistanceSensore2.Location = new Point(DistanceSensore1.Size.Width + 20, 10);
            DistanceSensoresPanel.Controls.Add(_distanceSensore2);

            _programStates = new ProgramStates();
            _programStatesPanel = programStatesPanel;
            ProgramStatesPanel.Controls.Add(_programStates);
            
            _applicationConsole = new ApplicationConsole();
            _applicationConsolePanel = applicationConsolePanel;
            ApplicationConsolePanel.Controls.Add(_applicationConsole);

            _packagesAmounts = new PackagesAmount();
            _packagesAmountsPanel = packagesAmountsPanel;
            PackagesAmountsPanel.Controls.Add(_packagesAmounts);
        }

        private static FirstTimeOffData _firstTimeOffData;
        private static FirstTimeOnData _firstTimeOnData;
        private static FirstTimeOnOffBaseData _firstTimeOnOffBaseData;
        private static OnData _onData;
        private static OffData _offData;
        private static MagneticSensorData _magneticSensorData;
        private static TrackingSensorData _trackingSensorData;

        public static FirstTimeOffData FirstTimeOffData { get { return _firstTimeOffData; } }
        public static FirstTimeOnData FirstTimeOnData { get { return _firstTimeOnData; } }
        public static OnData OnData { get { return _onData; } }
        public static OffData OffData { get { return _offData; } }
        public static MagneticSensorData MagneticSensorData { get { return _magneticSensorData; } }
        public static TrackingSensorData TrackingSensorData { get { return _trackingSensorData; } }
        public static FirstTimeOnOffBaseData FirstTimeOnOffBaseData { get { return _firstTimeOnOffBaseData; } }

        public static List<ReceiveBaseDataProtocol> DataProtocols = new List<ReceiveBaseDataProtocol>(); 

        public static void GenerateProtocolData()
        {
            _firstTimeOffData = new FirstTimeOffData();
            _firstTimeOnData = new FirstTimeOnData();
            _onData = new OnData();
            _offData = new OffData();
            _magneticSensorData = new MagneticSensorData(); 
            _trackingSensorData = new TrackingSensorData();
            _firstTimeOnOffBaseData = new FirstTimeOnOffBaseData();

            DataProtocols.Add(_firstTimeOffData);
            DataProtocols.Add(_firstTimeOnData);
            BaseDataProtocols.Add(_onData);
            BaseDataProtocols.Add(_offData);
            BaseDataProtocols.Add(_magneticSensorData);
            BaseDataProtocols.Add(_trackingSensorData);

        }

        private static SensorBaseData _sensorBaseData;
        private static OnOffBaseData _onOffBaseData;
        private static SensorActivatedData _sensorActivatedData;
        private static SensorDeactivatedData _sensorDeactivatedData;
        private static ConnectedData _connectedData;
        private static StateData _stateData;
        private static RobotArmData _robotArmData;
        private static ErrorData _errorData;
        private static EditData _editData;

        public static SensorBaseData SensorBaseData { get { return _sensorBaseData; } }
        public static OnOffBaseData OnOffBaseData { get { return _onOffBaseData; } }
        public static SensorActivatedData SensorActivatedData { get { return _sensorActivatedData; } }
        public static SensorDeactivatedData SensorDeactivatedData { get { return _sensorDeactivatedData; } }
        public static ConnectedData ConnectedData { get { return _connectedData; } }
        public static StateData StateData { get { return _stateData; } }
        public static RobotArmData RobotArmData { get { return _robotArmData; } }
        public static ErrorData ErrorData { get { return _errorData; } }
        public static EditData EditData { get { return _editData; } }

        public static List<ReceiveBaseDataProtocol> BaseDataProtocols = new List<ReceiveBaseDataProtocol>();

        public static void GenerateBaseDataProtocol()
        {
            _sensorBaseData = new SensorBaseData();
            _onOffBaseData = new OnOffBaseData();
            _sensorActivatedData = new SensorActivatedData();
            _sensorDeactivatedData = new SensorDeactivatedData();
            _connectedData = new ConnectedData();
            _stateData = new StateData();
            _robotArmData = new RobotArmData();
            _errorData = new ErrorData();
            _editData = new EditData();

            BaseDataProtocols.Add(_sensorBaseData);
            BaseDataProtocols.Add(_onOffBaseData);
            BaseDataProtocols.Add(_sensorActivatedData);
            BaseDataProtocols.Add(_sensorDeactivatedData);
            BaseDataProtocols.Add(_connectedData);
            BaseDataProtocols.Add(_stateData);
            BaseDataProtocols.Add(_robotArmData);
            BaseDataProtocols.Add(_errorData);
            BaseDataProtocols.Add(_editData);
        }

        public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs args);
        
        public static event AsyncEventHandler<ReceiveBaseDataProtocol> onNewDataRecieved;

        public static async Task InformOnNewData(ReceiveBaseDataProtocol baseDataProtocol)
        {
            if(onNewDataRecieved != null)
            {
                //MessageBox.Show("new event: " + baseDataProtocol.GetType().ToString());
                await onNewDataRecieved?.Invoke(_programStates, baseDataProtocol);
            }//err
            else
            {
                MessageBox.Show("New Information Event Is [null]: " + baseDataProtocol.GetType().ToString());
            }
        }

        public delegate Task AsyncEventHandler();
        public static event AsyncEventHandler onEnableWritingData;

        public static async Task EnableWritingData()
        {
            if (onEnableWritingData != null)
            {
                //MessageBox.Show("new event: " + baseDataProtocol.GetType().ToString());
                await onEnableWritingData?.Invoke();
            }//err
        }
        public delegate Task AsyncEventHandlerDistanceSensor<TEventArgs>(TEventArgs args);
        public static event AsyncEventHandlerDistanceSensor<DistanceSensorsProcessorData> onNewDistanceData;

        public static async Task InvokeNewDistanceReceived(int startDistance, int EndDistance)
        {
            if(onNewDistanceData != null)
            {
                await onNewDistanceData?.Invoke(new DistanceSensorsProcessorData() { DistanceSensorStart = startDistance, DistnaceSensorEnd = EndDistance});
            }
        }

        private static Request _request;
        public static Request Request { get { return _request; } }

        public static void StartRequestData()
        {
            _request = new Request();
            _request.SetRequest(new RequestOn());
        }

    }
}
