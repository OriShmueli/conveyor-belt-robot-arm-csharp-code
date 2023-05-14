using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class StateMachine
    {
        private BaseState _currentState = null;
        private StateFactory _stateFactory = null;

        public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public StateFactory StateFactory { get { return _stateFactory; } }

        public StateMachine()
        {
            _stateFactory = new StateFactory(this);
            Task.Run(async () =>
            {
                _currentState = await _stateFactory.Entry();
                //await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: EntryState");
                await _currentState.EnterState();
            });
            
           
        }

        public async Task SwitchState(BaseState newState)
        {
            await _currentState.SwitchState(newState);
            await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: " + newState.GetType().Name);
        }

        public async Task SwitchStateLate(BaseState newState)
        {
            await _currentState.SwitchState(newState);
        }
    }
}
