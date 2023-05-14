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
            this.ApplicationConsoleVScrollBar = new System.Windows.Forms.VScrollBar();
            this.ApplicationConsoleTitlePanel = new System.Windows.Forms.Panel();
            this.ApplicationConosleTextBox = new System.Windows.Forms.RichTextBox();
            this.ApplicationConsoleTitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ApplicationConsoleTitleLabel
            // 
            this.ApplicationConsoleTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ApplicationConsoleTitleLabel.Location = new System.Drawing.Point(0, 0);
            this.ApplicationConsoleTitleLabel.Name = "ApplicationConsoleTitleLabel";
            this.ApplicationConsoleTitleLabel.Size = new System.Drawing.Size(369, 31);
            this.ApplicationConsoleTitleLabel.TabIndex = 0;
            this.ApplicationConsoleTitleLabel.Text = "Console";
            this.ApplicationConsoleTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ApplicationConsoleVScrollBar
            // 
            this.ApplicationConsoleVScrollBar.Location = new System.Drawing.Point(348, 31);
            this.ApplicationConsoleVScrollBar.Name = "ApplicationConsoleVScrollBar";
            this.ApplicationConsoleVScrollBar.Size = new System.Drawing.Size(21, 445);
            this.ApplicationConsoleVScrollBar.TabIndex = 2;
            // 
            // ApplicationConsoleTitlePanel
            // 
            this.ApplicationConsoleTitlePanel.Controls.Add(this.ApplicationConsoleTitleLabel);
            this.ApplicationConsoleTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.ApplicationConsoleTitlePanel.Name = "ApplicationConsoleTitlePanel";
            this.ApplicationConsoleTitlePanel.Size = new System.Drawing.Size(369, 31);
            this.ApplicationConsoleTitlePanel.TabIndex = 3;
            // 
            // ApplicationConosleTextBox
            // 
            this.ApplicationConosleTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ApplicationConosleTextBox.Location = new System.Drawing.Point(0, 31);
            this.ApplicationConosleTextBox.Name = "ApplicationConosleTextBox";
            this.ApplicationConosleTextBox.ReadOnly = true;
            this.ApplicationConosleTextBox.Size = new System.Drawing.Size(345, 447);
            this.ApplicationConosleTextBox.TabIndex = 4;
            this.ApplicationConosleTextBox.Text = "";
            // 
            // ApplicationConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ApplicationConosleTextBox);
            this.Controls.Add(this.ApplicationConsoleTitlePanel);
            this.Controls.Add(this.ApplicationConsoleVScrollBar);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ApplicationConsole";
            this.Size = new System.Drawing.Size(369, 478);
            this.ApplicationConsoleTitlePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ApplicationConsoleTitleLabel;
        private System.Windows.Forms.VScrollBar ApplicationConsoleVScrollBar;
        private System.Windows.Forms.Panel ApplicationConsoleTitlePanel;
        private System.Windows.Forms.RichTextBox ApplicationConosleTextBox;
    }
}
