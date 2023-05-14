using ConveyorBelt_RobotArm.State_Machine;
using ConveyorBelt_RobotArm.State_Machine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm
{
    internal abstract class RobotArmBaseState
    {
        protected RobotArmFactory _factory;
        protected RobotArmStateMachine _ctx; //current context

        public RobotArmBaseState(RobotArmStateMachine stateMachine, RobotArmFactory robotArmFactory)
        {
            _factory = robotArmFactory;
            _ctx = stateMachine;
        }

        public abstract Task EnterState();
        public abstract Task ExitState();
        public abstract Task ApplyingData();

        public async Task SwitchState(RobotArmBaseState newState)
        {
            await ExitState();

            await ElementsUtil.ApplicationConsole.WriteLineGreen("[Robot Arm] Switch state to: " + newState.GetType().Name);
            
            await newState.EnterState();

            _ctx.CurrentState = newState;
            //await Task.Yield();
        }
    }
}
