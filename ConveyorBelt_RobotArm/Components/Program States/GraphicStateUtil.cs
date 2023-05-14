using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm.Components.Program_States
{
    internal static class GraphicStateUtil
    {
        //computer states
        private static GraphicStateComponent _ArmUIWorkFlow;
        private static GraphicStateComponent _updatePackagesAmounts;
        private static GraphicStateComponent _activatingDeactivating;
        private static GraphicStateComponent _sendingErrorResponse;
        private static GraphicStateComponent _writingDataToArduino;

        public static GraphicStateComponent ArmUIWorkFlow { get { return _ArmUIWorkFlow; } set { _ArmUIWorkFlow = value; } }
        public static GraphicStateComponent UpdatePackagesAmounts { get { return _updatePackagesAmounts; } set { _updatePackagesAmounts = value; } }
        public static GraphicStateComponent ActivatingDeactivating { get { return _activatingDeactivating; } set { _activatingDeactivating = value; }}
        public static GraphicStateComponent SendingErrorResponse { get { return _sendingErrorResponse; } set { _sendingErrorResponse = value; } }
        public static GraphicStateComponent WritingDataToArduino { get { return _writingDataToArduino; } set { _writingDataToArduino = value; } }

        //arduino states
        private static GraphicStateComponent _onOff;
        private static GraphicStateComponent _firstTimeOnOff;
        private static GraphicStateComponent _armMovement;
        private static GraphicStateComponent _conveyerBelt;
        private static GraphicStateComponent _sensores;

        public static GraphicStateComponent OnOff { get { return _onOff; } set { _onOff = value; } }
        public static GraphicStateComponent FirstTimeOnOff { get { return _firstTimeOnOff; } set { _firstTimeOnOff = value; } }
        public static GraphicStateComponent ArmMovement { get { return _armMovement; } set { _armMovement = value; } }
        public static GraphicStateComponent ConveyerBelt { get { return _conveyerBelt; } set { _conveyerBelt = value; } }
        public static GraphicStateComponent Sensores { get { return _sensores; } set { _sensores = value; } }

        //editing state
        private static GraphicStateComponent _modifyingArmMovement;
        public static GraphicStateComponent ModifyingArmMovement { get { return _modifyingArmMovement; } set { _modifyingArmMovement = value; } }

        private static void AddState(GraphicStateComponent graphicStateComponent)
        {
            
        }
    }
}
