using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ConveyorBelt_RobotArm
{
    public partial class Form1 : Form
    {
        SerialPort SerialPort = new SerialPort();
        
        public Form1()
        {
            InitializeComponent();

            SerialPort.Parity = Parity.None;
            SerialPort.StopBits = StopBits.One;
            SerialPort.DataBits = 8;
            SerialPort.BaudRate = int.Parse(AvailablePortsComboBox.Text);

            string[] ports = SerialPort.GetPortNames();
            AvailablePortsComboBox.Items.Clear();
            AvailablePortsComboBox.Items.AddRange(ports);
            
            CreateControls();
        }

        private void CreateControls()
        {
            RobotArmPanel.Controls.Add(new RobotArm());
            DistanceSensoresPanel.Controls.Add(new DistanceSensore());
            ProgramStatesPanel.Controls.Add(new ProgramStates());
            SerialConsolePanel.Controls.Add(new SerialConsole());
            PackagesAmountsPanel.Controls.Add(new PackagesAmount());
        }

        private void BaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPort.BaudRate = int.Parse(BaudRateComboBox.Text);
        }

        private void AvailablePortsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPort.PortName = AvailablePortsComboBox.Text;
        }

        private void CheckForAvaibleDataForSerialPortToEnableConnection()
        {
            if (AvailablePortsComboBox.Text != null || AvailablePortsComboBox.Text != ""){
                ConnectButton.Enabled = true;
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort.Open();
                SerialPortStatusLabelText.Text = "Connected";
                SerialPortStatusLabelText.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                return;
            }

            ConnectButton.Enabled = false;
            DisconnectButton.Enabled = true;
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort.Close();
                SerialPortStatusLabelText.Text = "Disconnected";
                SerialPortStatusLabelText.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                SerialPortStatusLabelText.Text = ex.Message;
                SerialPortStatusLabelText.ForeColor = Color.Red;
                return;
            }

            CheckForAvaibleDataForSerialPortToEnableConnection();
            DisconnectButton.Enabled = false;
        }
    }

   
}
