using ConveyorBelt_RobotArm.State_Machine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.State_Machine
{
    internal class StateFactory
    {
        private StateMachine _context;

        public StateFactory(StateMachine currentContext)
        {
            _context = currentContext;
        }

        public async Task<BaseState> Activating()
        {
            return await Task.Run(() =>
            {
                return new ActivatingState(_context, this);
            });
        }

        public async Task<BaseState> Deactivating()
        {
            return await Task.Run(() =>
            {
                return new DeactivatingState(_context, this);
            });
            
        }

        public async Task<BaseState> EditingRobotArm() 
        {
            return await Task.Run(() =>
            {
                return new EditingArmSettingsState(_context, this);
            });
            
        }

        public async Task<BaseState> Off()
        {
            return await Task.Run(() =>
            {
                return new OffState(_context, this);
            });
            
        }

        public async Task<BaseState> On()
        {
            return await Task.Run(() =>
            {
                return new OnState(_context, this);
            });
            
        }

        public async Task<BaseState> Entry()
        {
            return await Task.Run(() =>
            {
                return new EntryState(_context, this);
            });
            
        }

        public async Task<BaseState> Connected()
        {
            return await Task.Run(() =>
            {
                return new ConnectedState(_context, this);
            });
            
        }

        public async Task<BaseState> Disconnected()
        {
            return await Task.Run(() =>
            {
                return new DisconnectedState(_context, this);
            });
           
        }

        public async Task<BaseState> FirstTimeOn()
        {
            return await Task.Run(() =>
            {
                return new FirstOnState(_context, this);
            });
            
        }

        public async Task<BaseState> FirstTimeOff()
        {
            return await Task.Run(() =>
            {
                return new FirstOffState(_context, this);
            });
            
        }


        public async Task<BaseState> Robot()
        {
            return await Task.Run(() =>
            {
                return new RobotState(_context, this);
            });

        }
    }
}
