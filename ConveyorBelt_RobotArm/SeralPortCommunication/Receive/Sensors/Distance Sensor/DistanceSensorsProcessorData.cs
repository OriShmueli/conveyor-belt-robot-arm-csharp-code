using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Receive.Sensors.Distance_Sensor
{
    internal class DistanceSensorsProcessorData 
    {
        private int _distanceSensorStart; //2
        public int DistanceSensorStart { get { return _distanceSensorStart; } set { _distanceSensorStart = value; } }

        private int _distanceSensorEnd; //1    
        public int DistnaceSensorEnd { get { return _distanceSensorEnd; } set { _distanceSensorEnd = value; } }

        //private List<int> distanceSensorStartLocations = new List<int>(10); 
        //private List<int> distanceSensorEndLocations = new List<int>(10);

        //private bool _addLocations = true;
        //public bool AddLocations { get { return _addLocations; } }

        //public async Task InvokeDistanceSensorData(int startDistance, int endDistance)
        //{
            

        //    //if(distanceSensorStartLocations.Count == 10 || distanceSensorEndLocations.Count == 10)
        //    //{
        //    //    _addLocations = false;
        //    //    List<Task> calculateDisstances = new List<Task>();
        //    //    calculateDisstances.Add(calculateDistance(distanceSensorStartLocations, 11));
        //    //    calculateDisstances.Add(calculateDistance(distanceSensorEndLocations, 34));

        //    //    await Task.WhenAll(calculateDisstances);
        //    //}
        //    //else
        //    //{
        //    //    distanceSensorStartLocations.Add(startDistance);
        //    //    distanceSensorEndLocations.Add(endDistance);
        //    //}
        //}

        //private async Task calculateDistance(List<int> distanceLocations, int maxRange)
        //{
        //    if(distanceLocations.Any(x => x > maxRange))
        //    {
                
        //    }
        //}
    }
}
