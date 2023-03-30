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
            this.AvailablePortsComboBox = new System.Windows.Forms.ComboBox();
            this.SerialPortSettingsPanel = new System.Windows.Forms.Panel();
            this.SettingsExplanationLabel = new System.Windows.Forms.Label();
            this.CurrentSettingLabel = new System.Windows.Forms.Label();
            this.StatusLabelText = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.BaudRateComboBox = new System.Windows.Forms.ComboBox();
            this.BaudRateLabel = new System.Windows.Forms.Label();
            this.AvailablePortsLabel = new System.Windows.Forms.Label();
            this.DistanceSensoresPanel = new System.Windows.Forms.Panel();
            this.ProgramStatesPanel = new System.Windows.Forms.Panel();
            this.SerialConsolePanel = new System.Windows.Forms.Panel();
            this.RobotArmPanel = new System.Windows.Forms.Panel();
            this.PackagesAmountsPanel = new System.Windows.Forms.Panel();
            this.SerialPortSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvailablePortsComboBox
            // 
            this.AvailablePortsComboBox.FormattingEnabled = true;
            this.AvailablePortsComboBox.Location = new System.Drawing.Point(83, 3);
            this.AvailablePortsComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AvailablePortsComboBox.Name = "AvailablePortsComboBox";
            this.AvailablePortsComboBox.Size = new System.Drawing.Size(92, 21);
            this.AvailablePortsComboBox.TabIndex = 0;
            // 
            // SerialPortSettingsPanel
            // 
            this.SerialPortSettingsPanel.Controls.Add(this.SettingsExplanationLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.CurrentSettingLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.StatusLabelText);
            this.SerialPortSettingsPanel.Controls.Add(this.StatusLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.DisconnectButton);
            this.SerialPortSettingsPanel.Controls.Add(this.ConnectButton);
            this.SerialPortSettingsPanel.Controls.Add(this.BaudRateComboBox);
            this.SerialPortSettingsPanel.Controls.Add(this.BaudRateLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.AvailablePortsLabel);
            this.SerialPortSettingsPanel.Controls.Add(this.AvailablePortsComboBox);
            this.SerialPortSettingsPanel.Location = new System.Drawing.Point(9, 10);
            this.SerialPortSettingsPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SerialPortSettingsPanel.Name = "SerialPortSettingsPanel";
            this.SerialPortSettingsPanel.Size = new System.Drawing.Size(645, 46);
            this.SerialPortSettingsPanel.TabIndex = 1;
            // 
            // SettingsExplanationLabel
            // 
            this.SettingsExplanationLabel.AutoSize = true;
            this.SettingsExplanationLabel.Location = new System.Drawing.Point(3, 25);
            this.SettingsExplanationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SettingsExplanationLabel.Name = "SettingsExplanationLabel";
            this.SettingsExplanationLabel.Size = new System.Drawing.Size(106, 13);
            this.SettingsExplanationLabel.TabIndex = 9;
            this.SettingsExplanationLabel.Text = "Settings Explanation:";
            // 
            // CurrentSettingLabel
            // 
            this.CurrentSettingLabel.AutoSize = true;
            this.CurrentSettingLabel.Location = new System.Drawing.Point(99, 25);
            this.CurrentSettingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentSettingLabel.Name = "CurrentSettingLabel";
            this.CurrentSettingLabel.Size = new System.Drawing.Size(198, 13);
            this.CurrentSettingLabel.TabIndex = 8;
            this.CurrentSettingLabel.Text = "Using Arduino default serial port settings.";
            // 
            // StatusLabelText
            // 
            this.StatusLabelText.AutoSize = true;
            this.StatusLabelText.Location = new System.Drawing.Point(567, 6);
            this.StatusLabelText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StatusLabelText.Name = "StatusLabelText";
            this.StatusLabelText.Size = new System.Drawing.Size(16, 13);
            this.StatusLabelText.TabIndex = 7;
            this.StatusLabelText.Text = "...";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(527, 6);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(40, 13);
            this.StatusLabel.TabIndex = 6;
            this.StatusLabel.Text = "Status:";
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(436, 3);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 19);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(357, 3);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 19);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            // 
            // BaudRateComboBox
            // 
            this.BaudRateComboBox.FormattingEnabled = true;
            this.BaudRateComboBox.Items.AddRange(new object[] {
            "115200",
            "9600 "});
            this.BaudRateComboBox.Location = new System.Drawing.Point(250, 3);
            this.BaudRateComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BaudRateComboBox.Name = "BaudRateComboBox";
            this.BaudRateComboBox.Size = new System.Drawing.Size(92, 21);
            this.BaudRateComboBox.TabIndex = 3;
            // 
            // BaudRateLabel
            // 
            this.BaudRateLabel.AutoSize = true;
            this.BaudRateLabel.Location = new System.Drawing.Point(190, 6);
            this.BaudRateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BaudRateLabel.Name = "BaudRateLabel";
            this.BaudRateLabel.Size = new System.Drawing.Size(61, 13);
            this.BaudRateLabel.TabIndex = 2;
            this.BaudRateLabel.Text = "Baud Rate:";
            // 
            // AvailablePortsLabel
            // 
            this.AvailablePortsLabel.AutoSize = true;
            this.AvailablePortsLabel.Location = new System.Drawing.Point(3, 6);
            this.AvailablePortsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AvailablePortsLabel.Name = "AvailablePortsLabel";
            this.AvailablePortsLabel.Size = new System.Drawing.Size(80, 13);
            this.AvailablePortsLabel.TabIndex = 1;
            this.AvailablePortsLabel.Text = "Available Ports:";
            // 
            // DistanceSensoresPanel
            // 
            this.DistanceSensoresPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DistanceSensoresPanel.Location = new System.Drawing.Point(9, 60);
            this.DistanceSensoresPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DistanceSensoresPanel.Name = "DistanceSensoresPanel";
            this.DistanceSensoresPanel.Size = new System.Drawing.Size(645, 366);
            this.DistanceSensoresPanel.TabIndex = 2;
            // 
            // ProgramStatesPanel
            // 
            this.ProgramStatesPanel.Location = new System.Drawing.Point(658, 60);
            this.ProgramStatesPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgramStatesPanel.Name = "ProgramStatesPanel";
            this.ProgramStatesPanel.Size = new System.Drawing.Size(356, 366);
            this.ProgramStatesPanel.TabIndex = 3;
            // 
            // SerialConsolePanel
            // 
            this.SerialConsolePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialConsolePanel.Location = new System.Drawing.Point(1018, 10);
            this.SerialConsolePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SerialConsolePanel.Name = "SerialConsolePanel";
            this.SerialConsolePanel.Size = new System.Drawing.Size(399, 416);
            this.SerialConsolePanel.TabIndex = 4;
            // 
            // RobotArmPanel
            // 
            this.RobotArmPanel.Location = new System.Drawing.Point(10, 431);
            this.RobotArmPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RobotArmPanel.Name = "RobotArmPanel";
            this.RobotArmPanel.Size = new System.Drawing.Size(1408, 398);
            this.RobotArmPanel.TabIndex = 5;
            // 
            // PackagesAmountsPanel
            // 
            this.PackagesAmountsPanel.Location = new System.Drawing.Point(658, 10);
            this.PackagesAmountsPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PackagesAmountsPanel.Name = "PackagesAmountsPanel";
            this.PackagesAmountsPanel.Size = new System.Drawing.Size(356, 46);
            this.PackagesAmountsPanel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 839);
            this.Controls.Add(this.PackagesAmountsPanel);
            this.Controls.Add(this.RobotArmPanel);
            this.Controls.Add(this.SerialConsolePanel);
            this.Controls.Add(this.ProgramStatesPanel);
            this.Controls.Add(this.DistanceSensoresPanel);
            this.Controls.Add(this.SerialPortSettingsPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SerialPortSettingsPanel.ResumeLayout(false);
            this.SerialPortSettingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox AvailablePortsComboBox;
        private System.Windows.Forms.Panel SerialPortSettingsPanel;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ComboBox BaudRateComboBox;
        private System.Windows.Forms.Label BaudRateLabel;
        private System.Windows.Forms.Label AvailablePortsLabel;
        private System.Windows.Forms.Label StatusLabelText;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Panel DistanceSensoresPanel;
        private System.Windows.Forms.Panel ProgramStatesPanel;
        private System.Windows.Forms.Panel SerialConsolePanel;
        private System.Windows.Forms.Panel RobotArmPanel;
        private System.Windows.Forms.Label SettingsExplanationLabel;
        private System.Windows.Forms.Label CurrentSettingLabel;
        private System.Windows.Forms.Panel PackagesAmountsPanel;
    }
}

