namespace ConveyorBelt_RobotArm
{
    partial class DistanceSensore
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DistanceSensore));
            this.SensorePanel = new System.Windows.Forms.Panel();
            this.StartRedSabel = new System.Windows.Forms.Label();
            this.StartYellowLabel = new System.Windows.Forms.Label();
            this.StartGreenLabel = new System.Windows.Forms.Label();
            this.CurrentMinValueInfoLabel = new System.Windows.Forms.Label();
            this.CurrentMaxValueInfoLabel = new System.Windows.Forms.Label();
            this.CurrentMinValueLable = new System.Windows.Forms.Label();
            this.CurrentMaxValueLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.ErrorTitleLabel = new System.Windows.Forms.Label();
            this.MinSensitivityPanel = new System.Windows.Forms.Panel();
            this.MinDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.MinOffsetTitleLabel = new System.Windows.Forms.Label();
            this.MinOffsetTrackBar = new System.Windows.Forms.TrackBar();
            this.MinSensitivityLabel2 = new System.Windows.Forms.Label();
            this.MaxSensitivityLabel2 = new System.Windows.Forms.Label();
            this.MinRangeTextBoxLabel = new System.Windows.Forms.Label();
            this.MaxSensitivityPanel = new System.Windows.Forms.Panel();
            this.MaxDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.MaxOffsetTitleLabel = new System.Windows.Forms.Label();
            this.MaxOffsetTrackBar = new System.Windows.Forms.TrackBar();
            this.MaxSensitivityLabel1 = new System.Windows.Forms.Label();
            this.MinSensitivityLabel1 = new System.Windows.Forms.Label();
            this.MaxRangeTextBoxLabel = new System.Windows.Forms.Label();
            this.DistanceInfoLabel = new System.Windows.Forms.Label();
            this.DistanceLabel = new System.Windows.Forms.Label();
            this.SensoreTitleLabel = new System.Windows.Forms.Label();
            this.SensoreSliderPanel = new System.Windows.Forms.Panel();
            this.DistanceIndecatorPictureBox = new System.Windows.Forms.PictureBox();
            this.YellowPictureBox = new System.Windows.Forms.PictureBox();
            this.GreenPictureBox = new System.Windows.Forms.PictureBox();
            this.RedPictureBox = new System.Windows.Forms.PictureBox();
            this.SensorePanel.SuspendLayout();
            this.MinSensitivityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinOffsetTrackBar)).BeginInit();
            this.MaxSensitivityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxOffsetTrackBar)).BeginInit();
            this.SensoreSliderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceIndecatorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YellowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SensorePanel
            // 
            this.SensorePanel.Controls.Add(this.StartRedSabel);
            this.SensorePanel.Controls.Add(this.StartYellowLabel);
            this.SensorePanel.Controls.Add(this.StartGreenLabel);
            this.SensorePanel.Controls.Add(this.CurrentMinValueInfoLabel);
            this.SensorePanel.Controls.Add(this.CurrentMaxValueInfoLabel);
            this.SensorePanel.Controls.Add(this.CurrentMinValueLable);
            this.SensorePanel.Controls.Add(this.CurrentMaxValueLabel);
            this.SensorePanel.Controls.Add(this.ErrorLabel);
            this.SensorePanel.Controls.Add(this.ErrorTitleLabel);
            this.SensorePanel.Controls.Add(this.MinSensitivityPanel);
            this.SensorePanel.Controls.Add(this.MaxSensitivityPanel);
            this.SensorePanel.Controls.Add(this.DistanceInfoLabel);
            this.SensorePanel.Controls.Add(this.DistanceLabel);
            this.SensorePanel.Controls.Add(this.SensoreTitleLabel);
            this.SensorePanel.Controls.Add(this.SensoreSliderPanel);
            this.SensorePanel.Location = new System.Drawing.Point(3, 3);
            this.SensorePanel.Name = "SensorePanel";
            this.SensorePanel.Size = new System.Drawing.Size(394, 395);
            this.SensorePanel.TabIndex = 10;
            // 
            // StartRedSabel
            // 
            this.StartRedSabel.AutoSize = true;
            this.StartRedSabel.Location = new System.Drawing.Point(361, 300);
            this.StartRedSabel.Name = "StartRedSabel";
            this.StartRedSabel.Size = new System.Drawing.Size(25, 16);
            this.StartRedSabel.TabIndex = 32;
            this.StartRedSabel.Text = "1/6";
            // 
            // StartYellowLabel
            // 
            this.StartYellowLabel.AutoSize = true;
            this.StartYellowLabel.Location = new System.Drawing.Point(361, 220);
            this.StartYellowLabel.Name = "StartYellowLabel";
            this.StartYellowLabel.Size = new System.Drawing.Size(25, 16);
            this.StartYellowLabel.TabIndex = 31;
            this.StartYellowLabel.Text = "3/6";
            // 
            // StartGreenLabel
            // 
            this.StartGreenLabel.AutoSize = true;
            this.StartGreenLabel.Location = new System.Drawing.Point(361, 116);
            this.StartGreenLabel.Name = "StartGreenLabel";
            this.StartGreenLabel.Size = new System.Drawing.Size(25, 16);
            this.StartGreenLabel.TabIndex = 30;
            this.StartGreenLabel.Text = "6/6";
            // 
            // CurrentMinValueInfoLabel
            // 
            this.CurrentMinValueInfoLabel.AutoSize = true;
            this.CurrentMinValueInfoLabel.Location = new System.Drawing.Point(188, 375);
            this.CurrentMinValueInfoLabel.Name = "CurrentMinValueInfoLabel";
            this.CurrentMinValueInfoLabel.Size = new System.Drawing.Size(16, 16);
            this.CurrentMinValueInfoLabel.TabIndex = 29;
            this.CurrentMinValueInfoLabel.Text = "...";
            // 
            // CurrentMaxValueInfoLabel
            // 
            this.CurrentMaxValueInfoLabel.AutoSize = true;
            this.CurrentMaxValueInfoLabel.Location = new System.Drawing.Point(62, 375);
            this.CurrentMaxValueInfoLabel.Name = "CurrentMaxValueInfoLabel";
            this.CurrentMaxValueInfoLabel.Size = new System.Drawing.Size(16, 16);
            this.CurrentMaxValueInfoLabel.TabIndex = 28;
            this.CurrentMaxValueInfoLabel.Text = "...";
            // 
            // CurrentMinValueLable
            // 
            this.CurrentMinValueLable.AutoSize = true;
            this.CurrentMinValueLable.Location = new System.Drawing.Point(140, 375);
            this.CurrentMinValueLable.Name = "CurrentMinValueLable";
            this.CurrentMinValueLable.Size = new System.Drawing.Size(45, 16);
            this.CurrentMinValueLable.TabIndex = 27;
            this.CurrentMinValueLable.Text = "Value:";
            // 
            // CurrentMaxValueLabel
            // 
            this.CurrentMaxValueLabel.AutoSize = true;
            this.CurrentMaxValueLabel.Location = new System.Drawing.Point(11, 375);
            this.CurrentMaxValueLabel.Name = "CurrentMaxValueLabel";
            this.CurrentMaxValueLabel.Size = new System.Drawing.Size(45, 16);
            this.CurrentMaxValueLabel.TabIndex = 26;
            this.CurrentMaxValueLabel.Text = "Value:";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(251, 76);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(16, 16);
            this.ErrorLabel.TabIndex = 25;
            this.ErrorLabel.Text = "...";
            this.ErrorLabel.Visible = false;
            // 
            // ErrorTitleLabel
            // 
            this.ErrorTitleLabel.AutoSize = true;
            this.ErrorTitleLabel.Location = new System.Drawing.Point(251, 60);
            this.ErrorTitleLabel.Name = "ErrorTitleLabel";
            this.ErrorTitleLabel.Size = new System.Drawing.Size(39, 16);
            this.ErrorTitleLabel.TabIndex = 24;
            this.ErrorTitleLabel.Text = "Error:";
            this.ErrorTitleLabel.Visible = false;
            // 
            // MinSensitivityPanel
            // 
            this.MinSensitivityPanel.Controls.Add(this.MinDomainUpDown);
            this.MinSensitivityPanel.Controls.Add(this.MinOffsetTitleLabel);
            this.MinSensitivityPanel.Controls.Add(this.MinOffsetTrackBar);
            this.MinSensitivityPanel.Controls.Add(this.MinSensitivityLabel2);
            this.MinSensitivityPanel.Controls.Add(this.MaxSensitivityLabel2);
            this.MinSensitivityPanel.Controls.Add(this.MinRangeTextBoxLabel);
            this.MinSensitivityPanel.Location = new System.Drawing.Point(138, 31);
            this.MinSensitivityPanel.Name = "MinSensitivityPanel";
            this.MinSensitivityPanel.Size = new System.Drawing.Size(107, 341);
            this.MinSensitivityPanel.TabIndex = 21;
            // 
            // MinDomainUpDown
            // 
            this.MinDomainUpDown.Location = new System.Drawing.Point(6, 37);
            this.MinDomainUpDown.Name = "MinDomainUpDown";
            this.MinDomainUpDown.Size = new System.Drawing.Size(98, 22);
            this.MinDomainUpDown.TabIndex = 24;
            this.MinDomainUpDown.Text = "2";
            this.MinDomainUpDown.SelectedItemChanged += new System.EventHandler(this.MinDomainUpDown_SelectedItemChanged);
            // 
            // MinOffsetTitleLabel
            // 
            this.MinOffsetTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinOffsetTitleLabel.AutoSize = true;
            this.MinOffsetTitleLabel.Location = new System.Drawing.Point(3, 69);
            this.MinOffsetTitleLabel.Name = "MinOffsetTitleLabel";
            this.MinOffsetTitleLabel.Size = new System.Drawing.Size(44, 16);
            this.MinOffsetTitleLabel.TabIndex = 12;
            this.MinOffsetTitleLabel.Text = "Offset:";
            // 
            // MinOffsetTrackBar
            // 
            this.MinOffsetTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinOffsetTrackBar.Location = new System.Drawing.Point(6, 85);
            this.MinOffsetTrackBar.Name = "MinOffsetTrackBar";
            this.MinOffsetTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.MinOffsetTrackBar.Size = new System.Drawing.Size(56, 253);
            this.MinOffsetTrackBar.TabIndex = 13;
            this.MinOffsetTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MinOffsetTrackBar_MouseUp);
            // 
            // MinSensitivityLabel2
            // 
            this.MinSensitivityLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinSensitivityLabel2.AutoSize = true;
            this.MinSensitivityLabel2.Location = new System.Drawing.Point(68, 311);
            this.MinSensitivityLabel2.Name = "MinSensitivityLabel2";
            this.MinSensitivityLabel2.Size = new System.Drawing.Size(28, 16);
            this.MinSensitivityLabel2.TabIndex = 23;
            this.MinSensitivityLabel2.Text = "- 2 -";
            this.MinSensitivityLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MaxSensitivityLabel2
            // 
            this.MaxSensitivityLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxSensitivityLabel2.AutoSize = true;
            this.MaxSensitivityLabel2.Location = new System.Drawing.Point(62, 95);
            this.MaxSensitivityLabel2.Name = "MaxSensitivityLabel2";
            this.MaxSensitivityLabel2.Size = new System.Drawing.Size(42, 16);
            this.MaxSensitivityLabel2.TabIndex = 22;
            this.MaxSensitivityLabel2.Text = "- 400 -";
            this.MaxSensitivityLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MinRangeTextBoxLabel
            // 
            this.MinRangeTextBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinRangeTextBoxLabel.AutoSize = true;
            this.MinRangeTextBoxLabel.Location = new System.Drawing.Point(3, 10);
            this.MinRangeTextBoxLabel.Name = "MinRangeTextBoxLabel";
            this.MinRangeTextBoxLabel.Size = new System.Drawing.Size(63, 16);
            this.MinRangeTextBoxLabel.TabIndex = 18;
            this.MinRangeTextBoxLabel.Text = "Minimum:";
            // 
            // MaxSensitivityPanel
            // 
            this.MaxSensitivityPanel.Controls.Add(this.MaxDomainUpDown);
            this.MaxSensitivityPanel.Controls.Add(this.MaxOffsetTitleLabel);
            this.MaxSensitivityPanel.Controls.Add(this.MaxOffsetTrackBar);
            this.MaxSensitivityPanel.Controls.Add(this.MaxSensitivityLabel1);
            this.MaxSensitivityPanel.Controls.Add(this.MinSensitivityLabel1);
            this.MaxSensitivityPanel.Controls.Add(this.MaxRangeTextBoxLabel);
            this.MaxSensitivityPanel.Location = new System.Drawing.Point(9, 31);
            this.MaxSensitivityPanel.Name = "MaxSensitivityPanel";
            this.MaxSensitivityPanel.Size = new System.Drawing.Size(126, 341);
            this.MaxSensitivityPanel.TabIndex = 20;
            // 
            // MaxDomainUpDown
            // 
            this.MaxDomainUpDown.Location = new System.Drawing.Point(6, 38);
            this.MaxDomainUpDown.Name = "MaxDomainUpDown";
            this.MaxDomainUpDown.Size = new System.Drawing.Size(98, 22);
            this.MaxDomainUpDown.TabIndex = 18;
            this.MaxDomainUpDown.Text = "400";
            this.MaxDomainUpDown.SelectedItemChanged += new System.EventHandler(this.MaxDomainUpDown_SelectedItemChanged);
            // 
            // MaxOffsetTitleLabel
            // 
            this.MaxOffsetTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxOffsetTitleLabel.AutoSize = true;
            this.MaxOffsetTitleLabel.Location = new System.Drawing.Point(3, 69);
            this.MaxOffsetTitleLabel.Name = "MaxOffsetTitleLabel";
            this.MaxOffsetTitleLabel.Size = new System.Drawing.Size(44, 16);
            this.MaxOffsetTitleLabel.TabIndex = 12;
            this.MaxOffsetTitleLabel.Text = "Offset:";
            // 
            // MaxOffsetTrackBar
            // 
            this.MaxOffsetTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxOffsetTrackBar.Location = new System.Drawing.Point(6, 85);
            this.MaxOffsetTrackBar.Name = "MaxOffsetTrackBar";
            this.MaxOffsetTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.MaxOffsetTrackBar.Size = new System.Drawing.Size(56, 253);
            this.MaxOffsetTrackBar.TabIndex = 13;
            this.MaxOffsetTrackBar.Value = 10;
            this.MaxOffsetTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MaxOffsetTrackBar_MouseUp);
            // 
            // MaxSensitivityLabel1
            // 
            this.MaxSensitivityLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxSensitivityLabel1.AutoSize = true;
            this.MaxSensitivityLabel1.Location = new System.Drawing.Point(81, 95);
            this.MaxSensitivityLabel1.Name = "MaxSensitivityLabel1";
            this.MaxSensitivityLabel1.Size = new System.Drawing.Size(42, 16);
            this.MaxSensitivityLabel1.TabIndex = 14;
            this.MaxSensitivityLabel1.Text = "- 400 -";
            this.MaxSensitivityLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MinSensitivityLabel1
            // 
            this.MinSensitivityLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MinSensitivityLabel1.AutoSize = true;
            this.MinSensitivityLabel1.Location = new System.Drawing.Point(81, 311);
            this.MinSensitivityLabel1.Name = "MinSensitivityLabel1";
            this.MinSensitivityLabel1.Size = new System.Drawing.Size(28, 16);
            this.MinSensitivityLabel1.TabIndex = 15;
            this.MinSensitivityLabel1.Text = "- 2 -";
            this.MinSensitivityLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MaxRangeTextBoxLabel
            // 
            this.MaxRangeTextBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MaxRangeTextBoxLabel.AutoSize = true;
            this.MaxRangeTextBoxLabel.Location = new System.Drawing.Point(3, 10);
            this.MaxRangeTextBoxLabel.Name = "MaxRangeTextBoxLabel";
            this.MaxRangeTextBoxLabel.Size = new System.Drawing.Size(67, 16);
            this.MaxRangeTextBoxLabel.TabIndex = 17;
            this.MaxRangeTextBoxLabel.Text = "Maximum:";
            // 
            // DistanceInfoLabel
            // 
            this.DistanceInfoLabel.AutoSize = true;
            this.DistanceInfoLabel.Location = new System.Drawing.Point(313, 38);
            this.DistanceInfoLabel.Name = "DistanceInfoLabel";
            this.DistanceInfoLabel.Size = new System.Drawing.Size(16, 16);
            this.DistanceInfoLabel.TabIndex = 11;
            this.DistanceInfoLabel.Text = "...";
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Location = new System.Drawing.Point(251, 38);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(66, 16);
            this.DistanceLabel.TabIndex = 10;
            this.DistanceLabel.Text = "Distance: ";
            // 
            // SensoreTitleLabel
            // 
            this.SensoreTitleLabel.AutoSize = true;
            this.SensoreTitleLabel.Location = new System.Drawing.Point(178, 9);
            this.SensoreTitleLabel.Name = "SensoreTitleLabel";
            this.SensoreTitleLabel.Size = new System.Drawing.Size(67, 16);
            this.SensoreTitleLabel.TabIndex = 9;
            this.SensoreTitleLabel.Text = "Sensore...";
            this.SensoreTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SensoreSliderPanel
            // 
            this.SensoreSliderPanel.Controls.Add(this.DistanceIndecatorPictureBox);
            this.SensoreSliderPanel.Controls.Add(this.YellowPictureBox);
            this.SensoreSliderPanel.Controls.Add(this.GreenPictureBox);
            this.SensoreSliderPanel.Controls.Add(this.RedPictureBox);
            this.SensoreSliderPanel.Location = new System.Drawing.Point(251, 116);
            this.SensoreSliderPanel.Name = "SensoreSliderPanel";
            this.SensoreSliderPanel.Size = new System.Drawing.Size(104, 260);
            this.SensoreSliderPanel.TabIndex = 8;
            // 
            // DistanceIndecatorPictureBox
            // 
            this.DistanceIndecatorPictureBox.Image = global::ConveyorBelt_RobotArm.Properties.Resources.slider;
            this.DistanceIndecatorPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DistanceIndecatorPictureBox.Name = "DistanceIndecatorPictureBox";
            this.DistanceIndecatorPictureBox.Size = new System.Drawing.Size(104, 20);
            this.DistanceIndecatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistanceIndecatorPictureBox.TabIndex = 3;
            this.DistanceIndecatorPictureBox.TabStop = false;
            // 
            // YellowPictureBox
            // 
            this.YellowPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YellowPictureBox.Image = global::ConveyorBelt_RobotArm.Properties.Resources.yellow;
            this.YellowPictureBox.Location = new System.Drawing.Point(26, 120);
            this.YellowPictureBox.Name = "YellowPictureBox";
            this.YellowPictureBox.Size = new System.Drawing.Size(52, 80);
            this.YellowPictureBox.TabIndex = 2;
            this.YellowPictureBox.TabStop = false;
            // 
            // GreenPictureBox
            // 
            this.GreenPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GreenPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GreenPictureBox.BackgroundImage")));
            this.GreenPictureBox.Image = global::ConveyorBelt_RobotArm.Properties.Resources.green;
            this.GreenPictureBox.Location = new System.Drawing.Point(26, 0);
            this.GreenPictureBox.Name = "GreenPictureBox";
            this.GreenPictureBox.Size = new System.Drawing.Size(52, 120);
            this.GreenPictureBox.TabIndex = 1;
            this.GreenPictureBox.TabStop = false;
            // 
            // RedPictureBox
            // 
            this.RedPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RedPictureBox.Image = global::ConveyorBelt_RobotArm.Properties.Resources.red;
            this.RedPictureBox.Location = new System.Drawing.Point(26, 200);
            this.RedPictureBox.Name = "RedPictureBox";
            this.RedPictureBox.Size = new System.Drawing.Size(52, 40);
            this.RedPictureBox.TabIndex = 0;
            this.RedPictureBox.TabStop = false;
            // 
            // DistanceSensore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SensorePanel);
            this.Name = "DistanceSensore";
            this.Size = new System.Drawing.Size(400, 410);
            this.SensorePanel.ResumeLayout(false);
            this.SensorePanel.PerformLayout();
            this.MinSensitivityPanel.ResumeLayout(false);
            this.MinSensitivityPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinOffsetTrackBar)).EndInit();
            this.MaxSensitivityPanel.ResumeLayout(false);
            this.MaxSensitivityPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxOffsetTrackBar)).EndInit();
            this.SensoreSliderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DistanceIndecatorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YellowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SensorePanel;
        private System.Windows.Forms.Label StartRedSabel;
        private System.Windows.Forms.Label StartYellowLabel;
        private System.Windows.Forms.Label StartGreenLabel;
        private System.Windows.Forms.Label CurrentMinValueInfoLabel;
        private System.Windows.Forms.Label CurrentMaxValueInfoLabel;
        private System.Windows.Forms.Label CurrentMinValueLable;
        private System.Windows.Forms.Label CurrentMaxValueLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label ErrorTitleLabel;
        private System.Windows.Forms.Panel MinSensitivityPanel;
        private System.Windows.Forms.DomainUpDown MinDomainUpDown;
        private System.Windows.Forms.Label MinOffsetTitleLabel;
        private System.Windows.Forms.TrackBar MinOffsetTrackBar;
        private System.Windows.Forms.Label MinSensitivityLabel2;
        private System.Windows.Forms.Label MaxSensitivityLabel2;
        private System.Windows.Forms.Label MinRangeTextBoxLabel;
        private System.Windows.Forms.Panel MaxSensitivityPanel;
        private System.Windows.Forms.DomainUpDown MaxDomainUpDown;
        private System.Windows.Forms.Label MaxOffsetTitleLabel;
        private System.Windows.Forms.TrackBar MaxOffsetTrackBar;
        private System.Windows.Forms.Label MaxSensitivityLabel1;
        private System.Windows.Forms.Label MinSensitivityLabel1;
        private System.Windows.Forms.Label MaxRangeTextBoxLabel;
        private System.Windows.Forms.Label DistanceInfoLabel;
        private System.Windows.Forms.Label DistanceLabel;
        private System.Windows.Forms.Label SensoreTitleLabel;
        private System.Windows.Forms.Panel SensoreSliderPanel;
        private System.Windows.Forms.PictureBox DistanceIndecatorPictureBox;
        private System.Windows.Forms.PictureBox YellowPictureBox;
        private System.Windows.Forms.PictureBox GreenPictureBox;
        private System.Windows.Forms.PictureBox RedPictureBox;
    }
}
