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

namespace ConveyorBelt_RobotArm
{
    public partial class PackagesAmount : UserControl
    {
        

        public PackagesAmount()
        {
            InitializeComponent();
            UpdatePackagesAmounts(NumberOfBlackAndYellowBoxLabelText, 0);
            UpdatePackagesAmounts(NumberOfMagneticAndBlueBoxLabelText, 0);
            UpdatePackagesAmounts(NumberOfWhiteBoxLabelText, 0);
        }

        public void UpdatePackagesAmounts(Label label, int number)
        {
            if (number > 999)
            {
                number = 999;
            }
            label.Text = number.ToString();
        }

        //Properties.Resources.image...
        private void ChangeIcon(Bitmap icon)
        {
            BoxesAmountStatusPictureBox.Image = icon;
        }

        public async Task CheckForValidPackagesAmounts()
        {
            
        } 
    }
}
