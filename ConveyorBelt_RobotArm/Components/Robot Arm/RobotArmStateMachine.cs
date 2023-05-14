using ConveyorBelt_RobotArm.Data_Base.Requests;
using ConveyorBelt_RobotArm.State_Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm
{
    internal class RobotArmStateMachine
    {
        private RobotArmBaseState _currentState = null;
        private RobotArmFactory _stateFactory = null;

        public RobotArmBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public RobotArmFactory StateFactory { get { return _stateFactory; } }

        public RobotArmStateMachine()
        {
            
            _stateFactory = new RobotArmFactory(this);
            Task.Run(async () =>
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Created Robot Arm state machine");
                if (ElementsUtil.Request.GetRequest() is RequestEditing)
                {
                    ////await ElementsUtil.RobotArm.EndEditingToStartOn();
                    ////await ElementsUtil.ArmSettings.EnterRecordingState(true);
                    await ElementsUtil.ApplicationConsole.WriteLineBlue("Switch state to: Editing State");
                    _currentState = await _stateFactory.Editing();

                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: Conveyor Belt State");
                    _currentState = await _stateFactory.ConveyorBelt();
                }
                

                await _currentState.EnterState();
            });

        }

        public async Task SwitchState(RobotArmBaseState newState)
        {
            await _currentState.SwitchState(newState);
            await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: " + newState.GetType().Name);
        }

        public async Task SwitchStateLate(RobotArmBaseState newState)
        {
            await _currentState.SwitchState(newState);
        }
        //public void TransitionTo(RobotArmBaseState newState)
        //{
        //    _currentState = newState;
        //    _currentState.SetStateMachine(this);
        //}
    }
}
