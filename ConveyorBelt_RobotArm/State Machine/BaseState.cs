using ConveyorBelt_RobotArm.Components.Robot_Arm;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;
using ConveyorBelt_RobotArm.State_Machine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal abstract class BaseState
    {
        protected StateFactory _factory; 
        protected StateMachine _ctx; //current context

        //public static event Action onEnterState;
        //public static event Action onExitState;

        public string LastMessage = "";

        public BaseState(StateMachine context, StateFactory stateFactory)
        {
            _factory = stateFactory;
            _ctx = context;
        }

        public abstract Task EnterState();
        public abstract Task ExitState();
        public abstract Task ApplyingData();
        
        public virtual async Task SendLastMessage()
        {
            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray(LastMessage);
            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);
            await ElementsUtil.ApplicationConsole.WriteLineRed("ERROR: Sending message again: " + LastMessage);
        }

        public bool firstTimeOff = false;
        private bool _programActivatedFirstTime = true;

        public async Task SwitchState(BaseState newState)
        {
            await ExitState();
            if (_programActivatedFirstTime)
            {
                if(newState is ConnectedState)
                {

                }
                else
                {
                    await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: " + newState.GetType().Name);
                }
                _programActivatedFirstTime = false;
            }
            else
            {
                await ElementsUtil.ApplicationConsole.WriteLineGreen("Switch state to: " + newState.GetType().Name);
            }
            
            await newState.EnterState();

            _ctx.CurrentState = newState;
            //await Task.Yield();
        }
    }
}
