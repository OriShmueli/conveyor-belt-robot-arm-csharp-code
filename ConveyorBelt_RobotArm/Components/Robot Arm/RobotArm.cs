using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm
{ 
    //TODO: Fix tick frequency default is 1. currently its on 30.
    public partial class RobotArm : UserControl
    {
        public RobotArm()
        {
            InitializeComponent();
        }

        private void ChangeOnOffIcon(Bitmap icon)
        {
            OnOffPictureBox.Image = icon;
        }

        private void ApplicationOn()
        {
            ChangeOnOffIcon(Properties.Resources.turn_on_icon);
            OnPiece1Panel.BackColor = Color.Green;
            OnPiece2PictureBox.BackColor = Color.Green;
            OffPiece1Panel.BackColor = Color.Black;
            OffPiece2Panel.BackColor = Color.Black;
            OffPiece3PictureBox.BackColor = Color.Black;
            StartPanel.BackColor = Color.Green;
        }

        private void ApplicationOff()
        {
            ChangeOnOffIcon(Properties.Resources.turn_off_icon);
            OnPiece1Panel.BackColor = Color.Black;
            OnPiece2PictureBox.BackColor = Color.Black;
            OffPiece1Panel.BackColor = Color.Red;
            OffPiece2Panel.BackColor = Color.Red;
            OffPiece3PictureBox.BackColor = Color.Red;
        }

        private void SensoreDetectedBlackOrMagnetic()
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    
}
