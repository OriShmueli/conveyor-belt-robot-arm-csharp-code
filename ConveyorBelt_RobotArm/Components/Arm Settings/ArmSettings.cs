using ConveyorBelt_RobotArm.Components.Robot_Arm.States;
using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.RobotArm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine;
using ConveyorBelt_RobotArm.State_Machine.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ConveyorBelt_RobotArm
{
    public partial class ArmSettings : UserControl
    {
        public ArmSettings()
        {
            InitializeComponent();
            
            _firstPositions.Add(0);
            _firstPositions.Add(0);
            _firstPositions.Add(0);
            _firstPositions.Add(0);

            _lastPositions.Add(0);
            _lastPositions.Add(0);
            _lastPositions.Add(0);
            _lastPositions.Add(0);

        }

        private GetArmPositionsData _armPositionData;
        public GetArmPositionsData GetStartArmPositionsData { get { return _armPositionData; } set { _armPositionData = value; } }

        private List<int> _firstPositions = new List<int>();
        private List<int> _lastPositions = new List<int>();

        private int _stageId = 0;
        public int StageID { get { return _stageId; } set { _stageId = value; } }

        private char _pathId = 'u'; //u - pick up
        public char PathId { get { return _pathId; } set { _pathId = value; } }


        public async Task StarteditingRobotButton(bool enable)
        {
            
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartRecordingButton.Enabled = enable;
                });
            });
            
        }

        public async Task EnterRecordingState(bool enbale)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    EnterRecordingStateButton.Enabled = enbale;
                });
                return;
            });
        }

        public async Task EnableEditingBar()
        {
            
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ServoEditingPanel.Enabled = true;
                    StartRecordingButton.Enabled = true;
                    EnterRecordingStateButton.Enabled = true;
                    TestButton.Enabled = true;
                });
                //return;
            });
            await DisableOrEnableServoPanels(false);
            await SetArmStartingPositions(GetStartArmPositionsData.ArmIdAndPositions);
            //await ElementsUtil.ArmSettings.SetArmStartingPositions(GetStartArmPositionsData.ArmIdAndPositions);
        }


        public async Task DisableOrEnableServoPanels(bool enable)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Servo1Panel.Enabled = enable;
                    Servo2Panel.Enabled = enable;
                    Servo3Panel.Enabled = enable;
                    Servo4Panel.Enabled = enable;

                });
                //return;
            });
            
        }


        public async Task DisableOrEnableEditingPanel(bool enable)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ServoEditingPanel.Enabled = enable;
                    
                });
                //return;
            });
            
        }

        public async Task WritePositionsToBars()
        {

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Servo1TrackBar.Value = _armPositionData.ArmIdAndPositions[1];
                    Servo2TrackBar.Value = _armPositionData.ArmIdAndPositions[2];
                    Servo3TrackBar.Value = _armPositionData.ArmIdAndPositions[3];
                    Servo4TrackBar.Value = _armPositionData.ArmIdAndPositions[4];
                });
             });
        }

        public async Task SetArmStartingPositions(Dictionary<int, int> armPositions)
        {

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    _firstPositions.Clear();
                    _lastPositions.Clear();
                    _firstPositions.Add(Servo1TrackBar.Value);
                    _firstPositions.Add(Servo2TrackBar.Value);
                    _firstPositions.Add(Servo3TrackBar.Value);
                    _firstPositions.Add(Servo4TrackBar.Value);

                    _lastPositions.Add(Servo1TrackBar.Value);
                    _lastPositions.Add(Servo2TrackBar.Value);
                    _lastPositions.Add(Servo3TrackBar.Value);
                    _lastPositions.Add(Servo4TrackBar.Value);
                    Servo1TrackBar.Value = armPositions[1];
                    Servo2TrackBar.Value = armPositions[2];
                    Servo3TrackBar.Value = armPositions[3];
                    Servo4TrackBar.Value = armPositions[4];

                    
                });
                //return;
            });
        }

        private async void EnterRecordingStateButton_Click(object sender, EventArgs e)
        {
            if (ElementsUtil.Request.GetRequest() is RequestEditing)
            {
                if (!checkIfSaved())
                {
                    MessageBox.Show("Positions has not been saved");
                }
                TestButton.Enabled = false;
                EnterRecordingStateButton.Text = "Enter Recording State";
                ElementsUtil.Request.SetRequest(new RequestOn());
                byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(SendBaseDataProtocol.SendEditingExitState);
                await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
                ServoEditingPanel.Enabled = false;
                //await ElementsUtil.RobotArm.EndEditingToStartOn();
                //ServoEditingPanel.Enabled = false;
                //await ElementsUtil.StateMachine.SwitchState(await ElementsUtil.StateMachine.StateFactory.Robot());
            }
            else
            {
                if (ElementsUtil.RobotArmStateMachine != null)
                {
                    if (!(ElementsUtil.RobotArmStateMachine.CurrentState is RobotEditingState)) //TODO: && !(ElementsUtil.RobotArmStateMachine.CurrentState is RobotOffState)
                    {
                        await ElementsUtil.RobotArm.EndEditingColorOn();
                    }
                }

                if (ElementsUtil.StateMachine != null)
                {
                    if (ElementsUtil.StateMachine.CurrentState is FirstOffState || ElementsUtil.StateMachine.CurrentState is FirstOnState || ElementsUtil.StateMachine.CurrentState is EntryState || ElementsUtil.StateMachine.CurrentState is OnState || ElementsUtil.StateMachine.CurrentState is OffState) //TODO: && !(ElementsUtil.RobotArmStateMachine.CurrentState is RobotOffState)
                    {
                        await ElementsUtil.RobotArm.StartPanelEditing();
                    }
                }

                
                EnterRecordingStateButton.Text = "Exit Recording State State";
                ElementsUtil.Request.SetRequest(new RequestEditing());
                EnterRecordingStateButton.Enabled = false;
                
            }
            //EnterRecordingStateButton.Enabled = false;
            //await ElementsUtil.StateMachine.SwitchStateLate(await ElementsUtil.StateMachine.StateFactory.EditingRobotArm());
        }

        private bool checkIfSaved()
        {
            for (int i = 0; i < _lastPositions.Count; i++)
            {
                if (_lastPositions[i] != _firstPositions[i])
                {
                    return false;
                }
            }
            return true;
        }

        private async Task SendDataToArduino(int id, int value)
        {
            if (ElementsUtil.StateMachine != null)
            {
                if (ElementsUtil.StateMachine.CurrentState is EditingArmSettingsState)
                {//e#w#(number)<---> //a#t#(number)<---> (new)

                    char[] message = new char[3];
                    for (int i = 2; i >= 0; i--)
                    {
                        if (value / 10 == 0 && value % 10 == 0)
                        {
                            message[i] = '-';
                        }
                        else
                        {
                            message[i] = (char)(value % 10 + '0');
                        }
                        value /= 10;
                    }

                    if (_testMode)
                    {
                        //e#t#(number)<---> 
                        await writeTestingPositionsToArduino(id, new String(message));
                    }
                    else
                    {
                        //e#w#p#(path id)#s#(state number 1-2)#i#(number)<--->
                        await writePositionsToArduino(id, new String(message));
                    }
                }
            }
        }

        private async Task writeTestingPositionsToArduino(int id, string message) {
            //if (ElementsUtil.StateMachine != null)
            //{
            //    if (ElementsUtil.StateMachine.CurrentState is EditingArmSettingsState)
            //    {//e#w#(number)<---> //e#t#(number)<---> (new)

            //        char[] message = new char[3];
            //        for (int i = 2; i >= 0; i--)
            //        {
            //            if (value / 10 == 0 && value % 10 == 0)
            //            {
            //                message[i] = '-';
            //            }
            //            else
            //            {
            //                message[i] = (char)(value % 10 + '0');
            //            }
            //            value /= 10; 
            //        }
            //        string strMessage = "e#t#" + id.ToString() + "<" + new String(message) + ">";
            //        byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(strMessage);
            //        await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);

            //    }
            //}
            
            string strMessage = "e#t#" + id.ToString() + "<" + message + ">";
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(strMessage);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
        }

        private async Task writePositionsToArduino(int id, string message)
        {
            string strMessage = "e#w#p#" + _pathId.ToString() + "#s#" + _stageId.ToString() + "#i#" + id.ToString() + "<" + message + ">";
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(strMessage);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            //if (ElementsUtil.StateMachine != null)
            //{
            //    if (ElementsUtil.StateMachine.CurrentState is EditingArmSettingsState)
            //    {//e#w#p#(path id)#s#(state number 1-2)#i#(number)<--->

            //        char[] message = new char[3];
            //        for (int i = 2; i >= 0; i--)
            //        {
            //            if (value / 10 == 0 && value % 10 == 0)
            //            {
            //                message[i] = '-';
            //            }
            //            else
            //            {
            //                message[i] = (char)(value % 10 + '0');
            //            }
            //            value /= 10;
            //        }
            //        //change
            //        string strMessage = "e#w#" + id.ToString() + "<" + new String(message) + ">";
            //        byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(strMessage);
            //        await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);

            //    }
            //}


        }

        bool start = false;
        public bool _continueSending = true;
        public bool ContinueSending { get { return _continueSending; } set { _continueSending = value; } }

        private async void Servo1TrackBar_ValueChanged(object sender, EventArgs e)
        {            
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Servo1CurrentDegreeLabel.Text = Servo1TrackBar.Value.ToString();
                    Servo1NumericUpDown.Value = Servo1TrackBar.Value;
                    _lastPositions[0] = Servo1TrackBar.Value;

                });
                //return;
            });
            if (start || _testMode)
            {
                if (_continueSending)
                {
                    await SendDataToArduino(1, Servo1TrackBar.Value);
                    ContinueSending = false;
                }                
            }            
        }

        private async void Servo2TrackBar_ValueChanged(object sender, EventArgs e)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Servo2CurrentDegreeLabel.Text = Servo2TrackBar.Value.ToString();
                    Servo2NumericUpDown.Value = Servo2TrackBar.Value;
                    _lastPositions[1] = Servo2TrackBar.Value;

                });
            });
            
            if (start || _testMode)
            {
                if (_continueSending)
                {
                    await SendDataToArduino(2, Servo2TrackBar.Value);
                    ContinueSending = false;
                }
            }
        }

        private async void Servo3TrackBar_ValueChanged(object sender, EventArgs e)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Servo3CurrentDegreeLabel.Text = Servo3TrackBar.Value.ToString();
                    Servo3NumericUpDown.Value = Servo3TrackBar.Value;
                    _lastPositions[2] = Servo3TrackBar.Value;

                });
            });
            

            if (start || _testMode)
            {
                if (_continueSending)
                {
                    await SendDataToArduino(3, Servo3TrackBar.Value);
                    ContinueSending = false;
                }
            }
        }

        private async void Servo4TrackBar_ValueChanged(object sender, EventArgs e)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Servo4CurrentDegreeLabel.Text = Servo4TrackBar.Value.ToString();
                    Servo4NumericUpDown.Value = Servo4TrackBar.Value;
                    _lastPositions[3] = Servo4TrackBar.Value;
                });
            });
           
            if (start || _testMode)
            {
                if (_continueSending)
                {
                    await SendDataToArduino(4, Servo4TrackBar.Value);
                    ContinueSending = false;
                }
            }
        }

        private void ResetLocationsButton_Click(object sender, EventArgs e)
        {

            string message = "Do you want to reset postions?";
            string title = "Reset Postions";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < _firstPositions.Count; i++)
                {
                    _lastPositions[i] = _firstPositions[i];
                }
                Servo1TrackBar.Value = _firstPositions[0];
                Servo2TrackBar.Value = _firstPositions[1];
                Servo3TrackBar.Value = _firstPositions[2];
                Servo4TrackBar.Value = _firstPositions[3];
            }
            else
            {
                // Do something  
            }
           
        }

        private void UpdateLocationsButton_Click(object sender, EventArgs e)
        {
            //ServoEditingPanel
        }

        private void StartRecordingButton_Click(object sender, EventArgs e)
        {
            if (start)
            {
                TestButton.Enabled = true;
                start = false;
                StartRecordingButton.BackColor = Color.White;
                EnterRecordingStateButton.Enabled = true;
                StartRecordingButton.Enabled = true;
                StartRecordingButton.Text = "Start";
                ElementsUtil.RobotArm.IsEdtingStateEnabled = false;
            }
            else
            {
                TestButton.Enabled = false;
                start = true;
                StartRecordingButton.BackColor = Color.LightBlue;
                EnterRecordingStateButton.Enabled = false;
                //StartRecordingButton.Enabled = false;
                StartRecordingButton.Text = "Stop";
                ElementsUtil.RobotArm.IsEdtingStateEnabled = true;
            }
        }

        private bool _testMode = false;
        public bool TestMode { get { return _testMode; } set { _testMode = value; } }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            if (!_testMode)
            {//enter test mode 
                _testMode = true;
                await StarteditingRobotButton(false);
                await EnterRecordingState(false);
                await Task.Run(() => {
                    this.Invoke((MethodInvoker)delegate {
                        TestButton.Text = "Exit Test Mode";
                        TestButton.BackColor = Color.Blue;
                    });
                });
                await SetArmStartingPositions(GetStartArmPositionsData.ArmIdAndPositions);
                await DisableOrEnableServoPanels(true);
            }
            else
            {//exit test mode
                _testMode = false;
                await StarteditingRobotButton(true);
                await EnterRecordingState(true);
                await DisableOrEnableServoPanels(false);
                await Task.Run(() => {
                    this.Invoke((MethodInvoker)delegate {
                        TestButton.Text = "Enter Test Mode";
                        TestButton.BackColor = Color.White;

                    });
                });
            }
        }
    }
}
