namespace ConveyorBelt_RobotArm
{
    partial class ApplicationConsole
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
            this.ApplicationConsoleTitleLabel = new System.Windows.Forms.Label();
            this.ApplicationConosleTextBox = new System.Windows.Forms.TextBox();
            this.ApplicationConsoleVScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // ApplicationConsoleTitleLabel
            // 
            this.ApplicationConsoleTitleLabel.AutoSize = true;
            this.ApplicationConsoleTitleLabel.Location = new System.Drawing.Point(176, 9);
            this.ApplicationConsoleTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ApplicationConsoleTitleLabel.Name = "ApplicationConsoleTitleLabel";
            this.ApplicationConsoleTitleLabel.Size = new System.Drawing.Size(45, 13);
            this.ApplicationConsoleTitleLabel.TabIndex = 0;
            this.ApplicationConsoleTitleLabel.Text = "Console";
            // 
            // ApplicationConosleTextBox
            // 
            this.ApplicationConosleTextBox.Location = new System.Drawing.Point(2, 24);
            this.ApplicationConosleTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ApplicationConosleTextBox.Multiline = true;
            this.ApplicationConosleTextBox.Name = "ApplicationConosleTextBox";
            this.ApplicationConosleTextBox.Size = new System.Drawing.Size(392, 366);
            this.ApplicationConosleTextBox.TabIndex = 1;
            // 
            // ApplicationConsoleVScrollBar
            // 
            this.ApplicationConsoleVScrollBar.Location = new System.Drawing.Point(395, 24);
            this.ApplicationConsoleVScrollBar.Name = "ApplicationConsoleVScrollBar";
            this.ApplicationConsoleVScrollBar.Size = new System.Drawing.Size(21, 365);
            this.ApplicationConsoleVScrollBar.TabIndex = 2;
            // 
            // SerialConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ApplicationConsoleVScrollBar);
            this.Controls.Add(this.ApplicationConosleTextBox);
            this.Controls.Add(this.ApplicationConsoleTitleLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SerialConsole";
            this.Size = new System.Drawing.Size(411, 392);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ApplicationConsoleTitleLabel;
        private System.Windows.Forms.TextBox ApplicationConosleTextBox;
        private System.Windows.Forms.VScrollBar ApplicationConsoleVScrollBar;
    }
}
