using ConveyorBelt_RobotArm.Components.Robot_Arm.States;
using ConveyorBelt_RobotArm.State_Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Components.Robot_Arm
{
    internal class RobotArmFactory
    {
        private RobotArmStateMachine _context;

        public RobotArmFactory(RobotArmStateMachine currentContext)
        {
            _context = currentContext;
        }

        public async Task<RobotArmBaseState> ConveyorBelt()
        {
            return await Task.Run(() =>
            {
                return new ConveyorBeltState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> Pickup()
        {
            return await Task.Run(() =>
            {
                return new RobotPickupState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> BlackAndYellowState()
        {
            return await Task.Run(() =>
            {
                return new BlackAndYellowState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> Sensor2()
        {
            return await Task.Run(() =>
            {
                return new RobotSensor2State(_context, this);
            });
        }

        public async Task<RobotArmBaseState> MagneticAndBlue()
        {
            return await Task.Run(() =>
            {
                return new MagneticAndBlueState(_context, this);
            });
        }


        public async Task<RobotArmBaseState> White()
        {
            return await Task.Run(() =>
            {
                return new WhiteState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> End()
        {
            return await Task.Run(() =>
            {
                return new RobotEndState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> RotateFrom()
        {
            return await Task.Run(() =>
            {
                return new RobotRotateFromPathState(_context, this);
            });
        }

        public async Task<RobotArmBaseState> RotateTo()
        {
            return await Task.Run(() =>
            {
                return new RobotRotateToPathState(_context, this);
            });
        }
        //Add editing state
        public async Task<RobotArmBaseState> Editing()
        {
            return await Task.Run(() =>
            {
                return new RobotEditingState(_context, this);
            });
        }

        //public async Task<BaseState> Activating()
        //{
        //    return await Task.Run(() =>
        //    {
        //        return new ActivatingState(_context, this);
        //    });
        //}
    }
}
