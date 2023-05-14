using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.Data_Base.Requests
{
    internal class Request
    {
        private Request CurrentRequest = null;
        public void SetRequest(Request request)
        {
            CurrentRequest = request;
        }
        public Request GetRequest()
        {
            return CurrentRequest;
        }
    }
}
