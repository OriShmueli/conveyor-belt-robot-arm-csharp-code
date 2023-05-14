using ConveyorBelt_RobotArm.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.Components.Program_States
{
    public class GraphicStateComponent
    {
        private PictureBox _ArrowPictureBox;
        private Panel _statePanel;
        private Panel _statePath;
        
        public PictureBox ArrowPictureBox { get { return _ArrowPictureBox; } }
        public Panel StatePanel { get { return _statePanel; } }
        public Panel StatePath { get { return _statePath; } }

        private int _lowerPositionY = 0;
        private int _highestPositionY = 0;

        public int LowerPositionY { get { return _lowerPositionY; } }
        public int HighestPositionY { get { return _highestPositionY; } }

        private Label _stateLabel;
        public  Label StateLabel { get { return _stateLabel; } set { _stateLabel = value; } }

        public GraphicStateComponent(PictureBox arrowPictureBox, Panel statePanel, Panel statePath, int highestPosition, int lowestPosition, Label label)
        {
            _ArrowPictureBox = arrowPictureBox;
            _ArrowPictureBox.Visible = false;
            _statePanel = statePanel;
            _statePath = statePath;
            _highestPositionY = highestPosition;
            _lowerPositionY = lowestPosition;
            _stateLabel = label;
        }

        public virtual void DeactivateComponentPath()
        {
            _statePath.BackgroundImage = Resources.gray_transparent;
            _statePath.BackColor = Color.Transparent;
            _ArrowPictureBox.Visible = false;
        }

        public virtual void ExitState()
        {
            _ArrowPictureBox.Visible = false;
            _statePath.BackgroundImage = Resources.gray_transparent;
            _statePath.BackColor = Color.Transparent;
            _statePanel.BackgroundImage = Resources.gray_transparent;
            _statePanel.BackColor = Color.Transparent;
           
        }

        public virtual void EnterState()
        {
            _ArrowPictureBox.Visible = true;
            _statePath.BackColor = Color.FromArgb(0, 255, 0);            
            _statePanel.BackColor = Color.FromArgb(0, 255, 0);
            _statePath.BackgroundImage = null;
            _statePanel.BackgroundImage = null;
        }

        public virtual void ExitStateToNewState()
        { 
            _ArrowPictureBox.Visible = false;
            _statePath.BackColor = Color.FromArgb(0,255,0);
            _statePath.BackgroundImage = null;
            _statePanel.BackColor = Color.Transparent;
            _statePanel.BackgroundImage = Resources.gray_transparent;
        }

        public virtual void AddedToActivatedList(bool off = false)
        {
            if (off)
            {
                _statePanel.BackColor = Color.FromArgb(255, 0, 0);
                _statePanel.BackgroundImage = null;
                _ArrowPictureBox.Visible = false;
            }
            else
            {
                _statePanel.BackColor = Color.FromArgb(0, 255, 0);
                _statePanel.BackgroundImage = null;
                _ArrowPictureBox.Visible = false;
            }            
        }

        public virtual void ChangeName(string newName)
        {
            _stateLabel.Text = newName;
        }

        public void ActivateStatePath()
        {
            _statePath.BackColor = Color.FromArgb(0, 255, 0);
            _statePath.BackgroundImage = null;
        }

        public void DeavtivateStatePanel()
        {
            _statePanel.BackColor = Color.Transparent;
            _statePanel.BackgroundImage = Resources.gray_transparent;
            _ArrowPictureBox.Visible = false;
        }

        public void DeactivateStatePath()
        {
            _statePath.BackgroundImage = Resources.gray_transparent;
            _statePath.BackColor = Color.Transparent;
        }
    }
}
