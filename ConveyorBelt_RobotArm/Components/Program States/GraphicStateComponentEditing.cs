using ConveyorBelt_RobotArm.Properties;
using ConveyorBelt_RobotArm.SeralPortCommunication.Receive.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.Components.Program_States
{
    public class GraphicStateComponentEditing : GraphicStateComponent
    {
        public GraphicStateComponentEditing(PictureBox arrowPictureBox, Panel statePanel, Panel statePath, int highestPosition, int lowestPosition, System.Windows.Forms.Label label) : base(arrowPictureBox, statePanel, statePath, highestPosition, lowestPosition, label)
        {
        }

        public override void EnterState()
        {
            ArrowPictureBox.Visible = true;
            ArrowPictureBox.BackgroundImage = Resources.blue_arrow_down;
            StatePath.BackColor = Color.FromArgb(8, 52, 204);            
            StatePanel.BackColor = Color.FromArgb(8, 52, 204);
            StatePath.BackgroundImage = null;
            StatePanel.BackgroundImage = null;
        }

        //public override void ExitState()
        //{           
        //    base.ExitState();
        //}

        public override void AddedToActivatedList(bool off = false)
        {
            //base.AddedToActivatedList(off);
        }
    }
}
