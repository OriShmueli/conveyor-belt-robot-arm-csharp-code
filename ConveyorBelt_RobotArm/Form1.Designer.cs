namespace ConveyorBelt_RobotArm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RobotAvailablePortsComboBox = new System.Windows.Forms.ComboBox();
            this.SerialPortSettingsPanel = new System.Windows.Forms.Panel();
            this.SerialPortStatusLabelText = new System.Windows.Forms.Label();
            this.SerialPortStatusLabel = new System.Windows.Forms.Label();
            this.DistanceSensoresLabel = new System.Windows.Forms.Label();
            this.RobotLabel = new System.Windows.Forms.Label();
            this.DistanceSensoresBaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.DistanceSensoresBaudRateLabel = new System.Windows.Forms.Label();
            this.DistaceSensoresAvailablePortsLabel = new System.Windows.Forms.Label();
            this.DistanceSensoresAvailablePortsComboBox = new System.Windows.Forms.ComboBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.RobotBaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.RobotBaudRateLabel = new System.Windows.Forms.Label();
            this.RobotAvailablePortsLabel = new System.Windows.Forms.Label();
            this.ProgramStatesPanel = new System.Windows.Forms.Panel();
            this.RobotArmPanel = new System.Windows.Forms.Panel();
            this.ApplicationConsolePanel = new System.Windows.Forms.Panel();
            this.DistanceSensoresPanel = new System.Windows.Forms.Panel();
            this.ArmSettingsPanel = new System.Windows.Forms.Panel();
            this.PackagesAmountsPanel = new System.Windows.Forms.Panel();
            this.SerialPortSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RobotAvailablePortsComboBox
            // 
            this.RobotAvailablePortsComboBox.FormattingEnabled = true;
            this.RobotAvailablePortsComboBox.Location = new System.Drawing.Point(243, 2);
            this.RobotAvailablePortsComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RobotAvailablePortsComboBox.Name = "RobotAvailablePortsComboBox";
            this.RobotAvailablePortsComboBox.Size = new System.Drawing.Size(121, 24);
            this.RobotAvailablePortsComboBox.TabIndex = 0;
            this.RobotAvailablePortsComboBox.SelectedIndexChanged += new System.EventHandler(this.AvailablePortsComboBox_SelectedIndexChanged);
            this.RobotAvailablePortsComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AvailablePortsComboBox_MouseClick);
            // 
            // SerialPortSettingsPanel
            // 
            this.SerialPortSettingsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SerialPortSettingsPanel.Controls.Add(this.SerialPortStatusLabelText);
            this.SerialPortSettingsPanel.Controls.Add(this.SerialPortStatusLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.DistanceSensoresLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.RobotLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.DistanceSensoresBaudRateComboBox);
            this.SerialPortSettingsPanel.Controls.Add(this.DistanceSensoresBaudRateLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.DistaceSensoresAvailablePortsLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.DistanceSensoresAvailablePortsComboBox);
            this.SerialPortSettingsPanel.Controls.Add(this.DisconnectButton);
            this.SerialPortSettingsPanel.Controls.Add(this.ConnectButton);
            this.SerialPortSettingsPanel.Controls.Add(this.RobotBaudRateComboBox);
            this.SerialPortSettingsPanel.Controls.Add(this.RobotBaudRateLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.RobotAvailablePortsLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.RobotAvailablePortsComboBox);
            this.SerialPortSettingsPanel.Location = new System.Drawing.Point(12, 11);
            this.SerialPortSettingsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SerialPortSettingsPanel.Name = "SerialPortSettingsPanel";
            this.SerialPortSettingsPanel.Size = new System.Drawing.Size(830, 57);
            this.SerialPortSettingsPanel.TabIndex = 1;
            // 
            // SerialPortStatusLabelText
            // 
            this.SerialPortStatusLabelText.Location = new System.Drawing.Point(698, 22);
            this.SerialPortStatusLabelText.Name = "SerialPortStatusLabelText";
            this.SerialPortStatusLabelText.Size = new System.Drawing.Size(129, 31);
            this.SerialPortStatusLabelText.TabIndex = 15;
            this.SerialPortStatusLabelText.Text = "...";
            // 
            // SerialPortStatusLabel
            // 
            this.SerialPortStatusLabel.AutoSize = true;
            this.SerialPortStatusLabel.Location = new System.Drawing.Point(698, 6);
            this.SerialPortStatusLabel.Name = "SerialPortStatusLabel";
            this.SerialPortStatusLabel.Size = new System.Drawing.Size(47, 16);
            this.SerialPortStatusLabel.TabIndex = 14;
            this.SerialPortStatusLabel.Text = "Status:";
            // 
            // DistanceSensoresLabel
            // 
            this.DistanceSensoresLabel.AutoSize = true;
            this.DistanceSensoresLabel.Location = new System.Drawing.Point(3, 32);
            this.DistanceSensoresLabel.Name = "DistanceSensoresLabel";
            this.DistanceSensoresLabel.Size = new System.Drawing.Size(127, 16);
            this.DistanceSensoresLabel.TabIndex = 13;
            this.DistanceSensoresLabel.Text = "Distance Sensores :";
            // 
            // RobotLabel
            // 
            this.RobotLabel.AutoSize = true;
            this.RobotLabel.Location = new System.Drawing.Point(3, 8);
            this.RobotLabel.Name = "RobotLabel";
            this.RobotLabel.Size = new System.Drawing.Size(50, 16);
            this.RobotLabel.TabIndex = 12;
            this.RobotLabel.Text = "Robot :";
            // 
            // DistanceSensoresBaudRateComboBox
            // 
            this.DistanceSensoresBaudRateComboBox.FormattingEnabled = true;
            this.DistanceSensoresBaudRateComboBox.Items.AddRange(new object[] {
            "115200",
            "9600 ",
            "230400 ",
            "1000000",
            "2000000"});
            this.DistanceSensoresBaudRateComboBox.Location = new System.Drawing.Point(465, 29);
            this.DistanceSensoresBaudRateComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DistanceSensoresBaudRateComboBox.Name = "DistanceSensoresBaudRateComboBox";
            this.DistanceSensoresBaudRateComboBox.Size = new System.Drawing.Size(121, 24);
            this.DistanceSensoresBaudRateComboBox.TabIndex = 11;
            this.DistanceSensoresBaudRateComboBox.Text = "2000000";
            this.DistanceSensoresBaudRateComboBox.SelectedIndexChanged += new System.EventHandler(this.DistanceSensoresBaudRateComboBox_SelectedIndexChanged);
            // 
            // DistanceSensoresBaudRateLabel
            // 
            this.DistanceSensoresBaudRateLabel.AutoSize = true;
            this.DistanceSensoresBaudRateLabel.Location = new System.Drawing.Point(385, 32);
            this.DistanceSensoresBaudRateLabel.Name = "DistanceSensoresBaudRateLabel";
            this.DistanceSensoresBaudRateLabel.Size = new System.Drawing.Size(74, 16);
            this.DistanceSensoresBaudRateLabel.TabIndex = 10;
            this.DistanceSensoresBaudRateLabel.Text = "Baud Rate:";
            // 
            // DistaceSensoresAvailablePortsLabel
            // 
            this.DistaceSensoresAvailablePortsLabel.AutoSize = true;
            this.DistaceSensoresAvailablePortsLabel.Location = new System.Drawing.Point(136, 32);
            this.DistaceSensoresAvailablePortsLabel.Name = "DistaceSensoresAvailablePortsLabel";
            this.DistaceSensoresAvailablePortsLabel.Size = new System.Drawing.Size(101, 16);
            this.DistaceSensoresAvailablePortsLabel.TabIndex = 9;
            this.DistaceSensoresAvailablePortsLabel.Text = "Available Ports:";
            // 
            // DistanceSensoresAvailablePortsComboBox
            // 
            this.DistanceSensoresAvailablePortsComboBox.FormattingEnabled = true;
            this.DistanceSensoresAvailablePortsComboBox.Location = new System.Drawing.Point(243, 29);
            this.DistanceSensoresAvailablePortsComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DistanceSensoresAvailablePortsComboBox.Name = "DistanceSensoresAvailablePortsComboBox";
            this.DistanceSensoresAvailablePortsComboBox.Size = new System.Drawing.Size(121, 24);
            this.DistanceSensoresAvailablePortsComboBox.TabIndex = 8;
            this.DistanceSensoresAvailablePortsComboBox.SelectedIndexChanged += new System.EventHandler(this.DistanceSensoresAvailablePortsComboBox_SelectedIndexChanged);
            this.DistanceSensoresAvailablePortsComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DistanceSensoresAvailablePortsComboBox_MouseClick);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(592, 28);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(100, 24);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(592, 2);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 24);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // RobotBaudRateComboBox
            // 
            this.RobotBaudRateComboBox.FormattingEnabled = true;
            this.RobotBaudRateComboBox.Items.AddRange(new object[] {
            "2000000",
            "115200",
            "9600 ",
            "230400 ",
            "1000000"});
            this.RobotBaudRateComboBox.Location = new System.Drawing.Point(465, 2);
            this.RobotBaudRateComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RobotBaudRateComboBox.Name = "RobotBaudRateComboBox";
            this.RobotBaudRateComboBox.Size = new System.Drawing.Size(121, 24);
            this.RobotBaudRateComboBox.TabIndex = 3;
            this.RobotBaudRateComboBox.Text = "2000000";
            this.RobotBaudRateComboBox.SelectedIndexChanged += new System.EventHandler(this.BaudRateComboBox_SelectedIndexChanged);
            // 
            // RobotBaudRateLabel
            // 
            this.RobotBaudRateLabel.AutoSize = true;
            this.RobotBaudRateLabel.Location = new System.Drawing.Point(385, 5);
            this.RobotBaudRateLabel.Name = "RobotBaudRateLabel";
            this.RobotBaudRateLabel.Size = new System.Drawing.Size(74, 16);
            this.RobotBaudRateLabel.TabIndex = 2;
            this.RobotBaudRateLabel.Text = "Baud Rate:";
            // 
            // RobotAvailablePortsLabel
            // 
            this.RobotAvailablePortsLabel.AutoSize = true;
            this.RobotAvailablePortsLabel.Location = new System.Drawing.Point(136, 5);
            this.RobotAvailablePortsLabel.Name = "RobotAvailablePortsLabel";
            this.RobotAvailablePortsLabel.Size = new System.Drawing.Size(101, 16);
            this.RobotAvailablePortsLabel.TabIndex = 1;
            this.RobotAvailablePortsLabel.Text = "Available Ports:";
            // 
            // ProgramStatesPanel
            // 
            this.ProgramStatesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ProgramStatesPanel.Location = new System.Drawing.Point(848, 11);
            this.ProgramStatesPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgramStatesPanel.Name = "ProgramStatesPanel";
            this.ProgramStatesPanel.Size = new System.Drawing.Size(505, 430);
            this.ProgramStatesPanel.TabIndex = 3;
            // 
            // RobotArmPanel
            // 
            this.RobotArmPanel.BackColor = System.Drawing.SystemColors.Control;
            this.RobotArmPanel.Location = new System.Drawing.Point(12, 507);
            this.RobotArmPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RobotArmPanel.Name = "RobotArmPanel";
            this.RobotArmPanel.Size = new System.Drawing.Size(1508, 478);
            this.RobotArmPanel.TabIndex = 5;
            // 
            // ApplicationConsolePanel
            // 
            this.ApplicationConsolePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplicationConsolePanel.BackColor = System.Drawing.SystemColors.Control;
            this.ApplicationConsolePanel.Location = new System.Drawing.Point(1526, 508);
            this.ApplicationConsolePanel.Name = "ApplicationConsolePanel";
            this.ApplicationConsolePanel.Size = new System.Drawing.Size(369, 478);
            this.ApplicationConsolePanel.TabIndex = 12;
            // 
            // DistanceSensoresPanel
            // 
            this.DistanceSensoresPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DistanceSensoresPanel.Location = new System.Drawing.Point(12, 73);
            this.DistanceSensoresPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DistanceSensoresPanel.Name = "DistanceSensoresPanel";
            this.DistanceSensoresPanel.Size = new System.Drawing.Size(830, 430);
            this.DistanceSensoresPanel.TabIndex = 2;
            // 
            // ArmSettingsPanel
            // 
            this.ArmSettingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ArmSettingsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ArmSettingsPanel.Location = new System.Drawing.Point(1359, 10);
            this.ArmSettingsPanel.Name = "ArmSettingsPanel";
            this.ArmSettingsPanel.Size = new System.Drawing.Size(536, 492);
            this.ArmSettingsPanel.TabIndex = 15;
            // 
            // PackagesAmountsPanel
            // 
            this.PackagesAmountsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.PackagesAmountsPanel.Location = new System.Drawing.Point(848, 445);
            this.PackagesAmountsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PackagesAmountsPanel.Name = "PackagesAmountsPanel";
            this.PackagesAmountsPanel.Size = new System.Drawing.Size(505, 58);
            this.PackagesAmountsPanel.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1901, 993);
            this.Controls.Add(this.PackagesAmountsPanel);
            this.Controls.Add(this.ArmSettingsPanel);
            this.Controls.Add(this.ApplicationConsolePanel);
            this.Controls.Add(this.ProgramStatesPanel);
            this.Controls.Add(this.DistanceSensoresPanel);
            this.Controls.Add(this.SerialPortSettingsPanel);
            this.Controls.Add(this.RobotArmPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SerialPortSettingsPanel.ResumeLayout(false);
            this.SerialPortSettingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox RobotAvailablePortsComboBox;
        private System.Windows.Forms.Panel SerialPortSettingsPanel;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ComboBox RobotBaudRateComboBox;
        private System.Windows.Forms.Label RobotBaudRateLabel;
        private System.Windows.Forms.Label RobotAvailablePortsLabel;
        private System.Windows.Forms.Panel ProgramStatesPanel;
        private System.Windows.Forms.Panel RobotArmPanel;
        private System.Windows.Forms.Panel ApplicationConsolePanel;
        private System.Windows.Forms.Panel DistanceSensoresPanel;
        private System.Windows.Forms.Panel ArmSettingsPanel;
        private System.Windows.Forms.Panel PackagesAmountsPanel;
        private System.Windows.Forms.Label SerialPortStatusLabelText;
        private System.Windows.Forms.Label SerialPortStatusLabel;
        private System.Windows.Forms.Label DistanceSensoresLabel;
        private System.Windows.Forms.Label RobotLabel;
        private System.Windows.Forms.ComboBox DistanceSensoresBaudRateComboBox;
        private System.Windows.Forms.Label DistanceSensoresBaudRateLabel;
        private System.Windows.Forms.Label DistaceSensoresAvailablePortsLabel;
        private System.Windows.Forms.ComboBox DistanceSensoresAvailablePortsComboBox;
    }
}

