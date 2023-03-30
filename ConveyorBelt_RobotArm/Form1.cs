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
    public partial class Form1 : Form
    {
        List<Panel> PanelsControls = new List<Panel>();
        
        public Form1()
        {
            InitializeComponent();

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
    }

   
}
