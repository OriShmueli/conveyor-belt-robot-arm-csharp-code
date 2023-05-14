using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using ConveyorBelt_RobotArm.State_Machine;
using System.IO;
using ConveyorBelt_RobotArm.SeralPortCommunication;
using System.Reflection;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.On_Off;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Connection;
using ConveyorBelt_RobotArm.State_Machine.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using System.Security.Cryptography;

namespace ConveyorBelt_RobotArm
{
    public partial class Form1 : Form
    {
        SerialPort MainSerialPort = new SerialPort();
        SerialPort DistanceSensoresSerialPort = new SerialPort();
        
        //StateMachine ProgramContext;
        
        //IEnumerable<Type> ProtocolTypes;

        //List<char> ProtocolList;

        public Form1()
        {
            InitializeComponent();
           
            MainSerialPort.Parity = Parity.None;
            MainSerialPort.StopBits = StopBits.One;
            MainSerialPort.DataBits = 8;

            DistanceSensoresSerialPort.Parity = Parity.None;
            DistanceSensoresSerialPort.StopBits = StopBits.One;
            DistanceSensoresSerialPort.DataBits = 8;
            //SerialPort.BaudRate = int.Parse(AvailablePortsComboBox.Text);
            
            SetBaundRate();

            ElementsUtil.CreateElements(
                RobotArmPanel,
                ArmSettingsPanel,
                DistanceSensoresPanel,
                ProgramStatesPanel,
                ApplicationConsolePanel,
                PackagesAmountsPanel
                );

            ElementsUtil.SetGlobalSerialPortsClasses(MainSerialPort, DistanceSensoresSerialPort);

           

            AddDistanceSensoresToDictionary();

            //Type protocolParent = typeof(ReceiveBaseDataProtocol);
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //Type[] types = assembly.GetTypes();

            //ProtocolTypes = types.Where(t => t.BaseType == protocolParent);

            ////CreateControls();
            //ProtocolList = new List<char>();
            ////TODO ADD MORE
            //ProtocolList.Add(OnOffBaseData.StartingByte);
            //ProtocolList.Add(SensorBaseData.StartingByte);

            ElementsUtil.GenerateBaseDataProtocol();
            ElementsUtil.GenerateProtocolData();

            ElementsUtil.ActivateStateMachine();
            ElementsUtil.StartRequestData();



        }

        /*private void CreateControls()
        {
            Control RobotArm = new RobotArm();
            RobotArm.Location = new Point(5, 5);
            RobotArmPanel.Controls.Add(RobotArm);

            Control ArmSettings = new ArmSettings();
            ArmSettings.Location = new Point(0, 1);
            ArmSettingsPanel.Controls.Add(ArmSettings);

            Control DistanceSensore1 = new DistanceSensore();
            DistanceSensore1.Location = new Point(10, 10);
            DistanceSensoresPanel.Controls.Add(DistanceSensore1);

            Control DistanceSensore2 = new DistanceSensore();
            DistanceSensore2.Location = new Point(DistanceSensore1.Size.Width + 20, 10);
            DistanceSensoresPanel.Controls.Add(DistanceSensore2);

            ProgramStatesPanel.Controls.Add(new ProgramStates());
            ApplicationConsolePanel.Controls.Add(new ApplicationConsole());
            PackagesAmountsPanel.Controls.Add(new PackagesAmount());
        }*/

        //TODO: make the program context async
        #region Serial Port connection logic

        private void SetBaundRate()
        {
            MainSerialPort.BaudRate = int.Parse(RobotBaudRateComboBox.Text);
            DistanceSensoresSerialPort.BaudRate = int.Parse(DistanceSensoresBaudRateComboBox.Text);
        }

        private void BaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainSerialPort.BaudRate = int.Parse(RobotBaudRateComboBox.Text);
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void DistanceSensoresBaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistanceSensoresSerialPort.BaudRate = int.Parse(DistanceSensoresBaudRateComboBox.Text);
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void AvailablePortsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainSerialPort.PortName = RobotAvailablePortsComboBox.Text;
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void DistanceSensoresAvailablePortsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistanceSensoresSerialPort.PortName = DistanceSensoresAvailablePortsComboBox.Text;
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void AvailablePortsComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            RobotAvailablePortsComboBox.Items.Clear();
            RobotAvailablePortsComboBox.Items.AddRange(ports);
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void DistanceSensoresAvailablePortsComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            DistanceSensoresAvailablePortsComboBox.Items.Clear();
            DistanceSensoresAvailablePortsComboBox.Items.AddRange(ports);
            CheckForAvaibleDataForSerialPortToEnableConnection();
        }

        private void CheckForAvaibleDataForSerialPortToEnableConnection()
        {
            if (RobotAvailablePortsComboBox.SelectedIndex > -1 && RobotAvailablePortsComboBox.SelectedIndex > -1 && 
                DistanceSensoresAvailablePortsComboBox.SelectedIndex > -1 && DistanceSensoresAvailablePortsComboBox.SelectedIndex > -1)
            {
                ConnectButton.Enabled = true; 
            }
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainSerialPort.Open();
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                return;
            }

            try
            {
                DistanceSensoresSerialPort.Open();
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                MessageBox.Show(ex.Message + "port: " + DistanceSensoresSerialPort.PortName);
                return;
            }

            await DataProtocol.ClearBuffer(ElementsUtil.MainSerialPort);
            //BaseState c = await ProgramContext.StateFactory.Connected();
            await ElementsUtil.StateMachine.SwitchState(await ElementsUtil.StateMachine.StateFactory.Connected()); //make this async
            await ElementsUtil.ApplicationConsole.WriteLineGreen("Connected to port: " + ElementsUtil.MainSerialPort.PortName);
            SerialPortStatusLabelText.Text = "Connected";
            SerialPortStatusLabelText.ForeColor = Color.Green;
            
            RobotAvailablePortsComboBox.Enabled = false;
            RobotBaudRateComboBox.Enabled = false;
            DistanceSensoresAvailablePortsComboBox.Enabled = false;
            DistanceSensoresBaudRateComboBox.Enabled = false;
            ConnectButton.Enabled = false;
            DisconnectButton.Enabled = true;

            //await MainSerialPortDataProcessor();
            //MessageBox.Show("Distance sensores: " + DistanceSensoresSerialPort.PortName + ". " + DistanceSensoresSerialPort.BaudRate.ToString());
            //MessageBox.Show("serial port: " + SerialPort.PortName + ". " + SerialPort.BaudRate.ToString());
            //await ReadAndWriteDistanceSensoreData(DistanceSensoresSerialPort);
            await MainSerialPortDataProcessor();
        }

        private async void DisconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainSerialPort.Close(); 
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                return;
            }

            try
            {
                DistanceSensoresSerialPort.Close();
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                return;
            }

            await ElementsUtil.StateMachine.SwitchState(await ElementsUtil.StateMachine.StateFactory.Disconnected()); //make this async
            
            SerialPortStatusLabelText.Text = "Disconnected";
            SerialPortStatusLabelText.ForeColor = Color.Black;
            
            RobotAvailablePortsComboBox.Enabled = true;
            RobotBaudRateComboBox.Enabled = true;
            DistanceSensoresAvailablePortsComboBox.Enabled = true;
            DistanceSensoresBaudRateComboBox.Enabled = true;
            CheckForAvaibleDataForSerialPortToEnableConnection();
            DisconnectButton.Enabled = false;
        }

        private async Task MainSerialPortDataProcessor()
        {
            List<Task> ListenToData = new List<Task>();
            ListenToData.Add(ReadAndWriteDistanceSensoreData(DistanceSensoresSerialPort));
            ListenToData.Add(MainProgramProcessData());
            
            try
            {
                await Task.WhenAll(ListenToData);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region SerialPortIncomingData

        private bool _enableWriteConnectedData = true;
        
        private async Task MainProgramProcessData()
        {
           
            //MessageBox.Show("running while loop");
            //await ElementsUtil.ApplicationConsole.WriteLine("wating for more data... (read loop)");

            await Task.Run(async () => {

                //await Task.Delay(100);
                //await ElementsUtil.ApplicationConsole.WriteLine("wating for more data... (read loop)");
                //await DataProtocol.ClearBuffer(ElementsUtil.MainSerialPort);
                while (MainSerialPort.IsOpen)
                {
                    if (_enableWriteConnectedData)
                    {
                        _enableWriteConnectedData = false;
                        await ElementsUtil.EnableWritingData();
                    }
                    if (MainSerialPort.BytesToRead > 0)
                    {
                        //List<Task> readData = new List<Task>();
                        //MessageBox.Show("SerialPort.BytesToRead > 0");
                        byte byte_buffer = (byte)MainSerialPort.ReadByte();
                       
                        
                        //await ElementsUtil.ApplicationConsole.WriteLine("wating for more data... (read loop)");
                        while (true)
                        {
                            bool res = await CheckForTypes(byte_buffer);
                            if (res)
                            {
                                //MessageBox.Show("break first while loop");
                                //await ElementsUtil.ApplicationConsole.WriteLine("Braking loop");
                                break;
                            }
                            else
                            {
                                try
                                {
                                    byte_buffer = (byte)MainSerialPort.ReadByte();
                                }
                                catch(IOException io)
                                {
                                    await ElementsUtil.ApplicationConsole.WriteLineRed(io.Message);
                                }
                                
                            }
                        }  
                    }
                }
            });
        }

        private async Task<bool> CheckForTypes(byte byte_buffer)
        {
            foreach (ReceiveBaseDataProtocol item in ElementsUtil.BaseDataProtocols)
            {
                if(item.GetStatingByte() == byte_buffer)
                {
                    if(byte_buffer == ElementsUtil.ErrorData.GetStatingByte())
                    {
                        await ElementsUtil.ErrorData.ProcessData();
                        return true;
                    }
                    //MessageBox.Show("found: " + (char)byte_buffer);
                    await ElementsUtil.ApplicationConsole.WriteLine("find some data: " + item.GetStatingByte().ToString() + "   length: [" + item.GetBufferLength().ToString() + "]");
                    await DataProtocol.ReadAsync(MainSerialPort, item.GetBufferLength(), item);
                    //await ElementsUtil.ApplicationConsole.WriteLine("finish process data");
                    return true;
                }
            }
            return false;
        }

        #endregion

        //TODO: change the code to d
        #region DistaceSensoresLogic

        Dictionary<int, DistanceSensore> IdDistanceSensore = new Dictionary<int, DistanceSensore>();

        private void AddDistanceSensoresToDictionary()
        {
            IdDistanceSensore.Add(1, ElementsUtil.DistanceSensore1); ElementsUtil.DistanceSensore1.SensoreNumber = 1; ElementsUtil.DistanceSensore1.SensorName = "End"; ElementsUtil.DistanceSensore1.SetSensoreName(); //end
            IdDistanceSensore.Add(2, ElementsUtil.DistanceSensore2); ElementsUtil.DistanceSensore2.SensoreNumber = 2; ElementsUtil.DistanceSensore2.SensorName = "Start"; ElementsUtil.DistanceSensore2.SetSensoreName(); 
        }

        private async Task ReadAndWriteDistanceSensoreData(SerialPort serialPort)
        {
            while (serialPort.IsOpen)
            {
                await write(serialPort);
                /*byte byte_buffer = (byte)SerialPort.ReadByte();
                while (byte_buffer != 'd') //change in code to d
                {
                    byte_buffer = (byte)SerialPort.ReadByte();
                    //await Task.Delay(10);
                }*/
                await Task.Delay(10);
                await read(serialPort);
            }
        }

        private async Task write(SerialPort serialPort)
        {
            await Task.Run(() => {
                //use base stream baseStream.asyncWrite
                try
                {
                    serialPort.Write("c");
                }
                catch(Exception ex)
                {

                }
                
            });
        }

        private async Task read(SerialPort serialPort)
        {
            //MessageBox.Show("reading data. " + serialPort.PortName);
            byte[] buffer = new byte[27]; //17
            //int data;
            //try
            //{
            //    data = await serialPort.BaseStream?.ReadAsync(buffer, 0, buffer.Length);
            //}
            //catch (IOException ex)
            //{
            //    MessageBox.Show("retun null. ex: " + ex.Message);
            //    return;
            //}

            try
            {
                var data = await serialPort.BaseStream?.ReadAsync(buffer, 0, buffer.Length);
                var byteArray = new byte[data];
                Array.Copy(buffer, byteArray, data);
                string s = System.Text.Encoding.UTF8.GetString(byteArray, 0, data);
                //MessageBox.Show(s);
                if (byteArray.Length < 27) //17
                {
                    //MessageBox.Show("return");
                    return;
                }

                await WriteNewDataToGUI(byteArray);
            }
            catch (InvalidOperationException ex)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("No sensor data: " + ex.Message);
            }

            //MessageBox.Show("data: " + data.ToString());
           
        }

        private async Task WriteNewDataToGUI(byte[] received)
        {
            //MessageBox.Show("writing to gui");
            List<Task> updateDistanceSensore = new List<Task>();
            //int distance1 = 0;
            //int distance2 = 0;
            try
            {
                for (int i = 0; i < received.Length; i++)
                {
                    if (received[i] == '#') //i=1, i=9
                    {
                        if (IdDistanceSensore.ContainsKey((int)(char)received[i + 1] - '0'))
                        {
                            try
                            {
                                DistanceSensore distanceSensore = (DistanceSensore)DistanceSensoresPanel.Controls[DistanceSensoresPanel.Controls.IndexOf(IdDistanceSensore[(char)received[i + 1] - '0'])];
                                int newDistance = ConvertByteArrayToIntByOffset(received, i + 3);
                                if (distanceSensore.IsDistanceChanged(newDistance))
                                {
                                    distanceSensore.NewDistance = newDistance;
                                    updateDistanceSensore.Add(distanceSensore.ShowDistance());
                                }
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }
                        }
                        try
                        {
                            if (((char)received[i + 1] - '0') == 3)
                            {
                                updateDistanceSensore.Add(ElementsUtil.SensorBaseData.ProcessMagneticData(((char)received[i + 3] - '0')));
                            }

                            if (((char)received[i + 1] - '0') == 4)
                            {
                                //MessageBox.Show(((char)received[i + 3] - '0').ToString());
                                updateDistanceSensore.Add(ElementsUtil.SensorBaseData.ProcessTrackingData(((char)received[i + 3] - '0')));
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            //await ElementsUtil.ApplicationConsole.WriteLineRed("Sensors method:[WriteNewDataToGUI]: " + ex.Message);
                            return;
                        }

                        //if (((int)(char)received[i + 1] - '0') == 1)
                        //{
                        //    distance1 = ConvertByteArrayToInt(received, i + 3);
                        //}

                        //if (((char)received[i + 1] - '0') == 2)
                        //{
                        //    distance2 = ConvertByteArrayToInt(received, i + 3);
                        //}
                    }
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                //ElementsUtil.ApplicationConsole.WriteLineRed("Sensors main method:[WriteNewDataToGUI]: " + ex.Message);
                return;
            }
            

            updateDistanceSensore.Add(ElementsUtil.InvokeNewDistanceReceived(IdDistanceSensore[2].NewDistance, IdDistanceSensore[1].NewDistance));
            
            if (updateDistanceSensore.Count > 0)
            {
                try
                {
                    //MessageBox.Show("when all");
                    await Task.WhenAll(updateDistanceSensore);
                }
                catch (OperationCanceledException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private int ConvertByteArrayToIntByOffset(byte[] array, int offset)
        {
            try
            {
                int distance = 0;
                List<int> newListNumber = new List<int>();
                for (int i = offset; i < 4 + offset; i++)
                {
                    if (array[i] == '-')
                    {
                    }
                    else
                    {
                        newListNumber.Add((int)(char)array[i] - '0');
                    }
                }

                for (int i = 0; i < newListNumber.Count; i++)
                {
                    distance += newListNumber[i] * Convert.ToInt32(Math.Pow(10, newListNumber.Count - i - 1));
                }

                return distance;
            }
            catch(IndexOutOfRangeException iex)
            {
                /*Task.Run(async () =>
                {
                    await ElementsUtil.ApplicationConsole.WriteLineRed("Sensors method:[ConvertByteArrayToIntByOffset]: " + iex.Message);
                });*/
                
                return 0;
            }
            
        }

        #endregion

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, Encoding.ASCII.GetBytes("e" + "\n"));
            await DataProtocol.WriteDataAsync(ElementsUtil.DistanceSensorsSerialPort, Encoding.ASCII.GetBytes("e" + "\n"));

            try
            {
                ElementsUtil.DistanceSensorsSerialPort.Close();
                ElementsUtil.MainSerialPort.Close();
            }catch(IOException ex)
            {
                await ElementsUtil.ApplicationConsole.WriteLineRed("Application Closing event:[Form1_FormClosing]: " + ex.Message);
            }
        }
    }
}
