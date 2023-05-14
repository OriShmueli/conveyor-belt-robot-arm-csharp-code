using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.Components.Program_States
{
    internal class GraphicStateComponentError : GraphicStateComponent
    {
        public GraphicStateComponentError(PictureBox arrowPictureBox, Panel statePanel, Panel statePath, int highestPosition, int lowestPosition, Label label) : base(arrowPictureBox, statePanel, statePath, highestPosition, lowestPosition, label)
        {
        }

        public override void AddedToActivatedList(bool path = false)
        {
            StatePanel.BackColor = Color.FromArgb(255, 0, 0);
            ArrowPictureBox.Visible = false;
        }
    }
}
