namespace ConveyorBelt_RobotArm
{
    partial class SerialConsole
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
            this.SerialConsoleTitleLabel = new System.Windows.Forms.Label();
            this.SerialConosleTextBox = new System.Windows.Forms.TextBox();
            this.SerialConsoleVScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // SerialConsoleTitleLabel
            // 
            this.SerialConsoleTitleLabel.AutoSize = true;
            this.SerialConsoleTitleLabel.Location = new System.Drawing.Point(235, 11);
            this.SerialConsoleTitleLabel.Name = "SerialConsoleTitleLabel";
            this.SerialConsoleTitleLabel.Size = new System.Drawing.Size(57, 16);
            this.SerialConsoleTitleLabel.TabIndex = 0;
            this.SerialConsoleTitleLabel.Text = "Console";
            // 
            // SerialConosleTextBox
            // 
            this.SerialConosleTextBox.Location = new System.Drawing.Point(3, 30);
            this.SerialConosleTextBox.Multiline = true;
            this.SerialConosleTextBox.Name = "SerialConosleTextBox";
            this.SerialConosleTextBox.Size = new System.Drawing.Size(502, 479);
            this.SerialConosleTextBox.TabIndex = 1;
            // 
            // SerialConsoleVScrollBar
            // 
            this.SerialConsoleVScrollBar.Location = new System.Drawing.Point(508, 30);
            this.SerialConsoleVScrollBar.Name = "SerialConsoleVScrollBar";
            this.SerialConsoleVScrollBar.Size = new System.Drawing.Size(21, 479);
            this.SerialConsoleVScrollBar.TabIndex = 2;
            // 
            // SerialConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SerialConsoleVScrollBar);
            this.Controls.Add(this.SerialConosleTextBox);
            this.Controls.Add(this.SerialConsoleTitleLabel);
            this.Name = "SerialConsole";
            this.Size = new System.Drawing.Size(548, 512);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SerialConsoleTitleLabel;
        private System.Windows.Forms.TextBox SerialConosleTextBox;
        private System.Windows.Forms.VScrollBar SerialConsoleVScrollBar;
    }
}
