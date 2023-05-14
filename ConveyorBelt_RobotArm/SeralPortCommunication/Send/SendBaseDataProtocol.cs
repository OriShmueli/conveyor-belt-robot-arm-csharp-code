using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorBelt_RobotArm.SeralPortCommunication.Send
{
    internal abstract class SendBaseDataProtocol
    {
        public static char data = '0';
        public abstract Task SendData();

        public static string ReceivedFirstTimeOnData = "o#n#f#r";
        public static string ReceivedFirstTimeOffData = "o#f#f#r";
        public static string ReceivedOnData = "o#n#r";
        public static string ReceivedOffData = "o#f#r";

        public static string ReceivedTrackSensorData = "n#t#r";
        public static string ReceivedMagneticSensorData = "n#m#r";

        public static string SendConnectedState = "s#c";
        public static string SendDisconnectedState = "s#n";

        public static string SendActivatingState = "s#a";
        public static string SendDeactivatingState = "s#d";

        public static string SendEditingStateRequest = "e#e";
        public static string SendEditingEnterState = "e#e#n";
        public static string SendEditingExitState = "e#e#t";

        public static string RequestEnterRobotState = "a#a";
        //public static string RequestEnterRobotState = "s#a";
        //public static string RequestEnterRobotState = "s#a";

        public static string RequestArmStartPositions = "a#r#p";
        public static string RequestArmStartPositionsError = "a#r#p#e";
        public static string Error = "v";

        public static string RequestingToEnterConveyorBeltState = "a#c#c";
        public static string ConveyorBeltStart = "a#c#t";
        public static string ConveyorBeltStop = "a#c#s";
        public static string EnterRobotArmPickupState = "a#s#u";
        public static string RobotArmPickupStartStage1 = "a#s#u#t#a";
        public static string RobotArmPickupEndStage1 = "a#s#u#n#a";
        public static string RobotArmPickupStartStage2 = "a#s#u#t#b";
        public static string RobotArmPickupEndStage2 = "a#s#u#n#b";
        public static string Sensor2 = "a#s#s";
        public static string Sensor2Enter = "a#s#e";
        public static string Sensor2Leave = "a#s#l";

        public static string RotateTo = "a#r#t";
        public static string RotateToWhite = "a#r#t#w";
        public static string RotateToblackAndYellow = "a#r#t#y";
        public static string RotateToBlueAndMagnetic = "a#r#t#m";
        public static string RotateToActivateLed = "a#r#t#o"; //no use
        public static string RotateToDeactivateLed = "a#r#t#f"; //no use

        public static string RotateFrom = "a#r#f";
        public static string RotateFromEnd = "a#r#f#n";
        public static string RotateFromWhite = "a#r#f#w";
        public static string RotateFromMagneticAndBlue = "a#r#f#m";
        public static string RotateFromBlackAndYellow = "a#r#f#y";

        public static string PathWhite = "a#s#u#p#w";
        public static string PathWhiteStage1 = "a#s#u#a#p#w"; //no use
        public static string PathWhiteStage1Start = "a#s#u#t#a#p#w";
        public static string PathWhiteStage1End = "a#s#u#n#a#p#w";
        public static string PathWhiteStage2 = "a#s#u#b#p#w"; //no use
        public static string PathWhiteStage2Start = "a#s#u#t#b#p#w";
        public static string PathWhiteStage2End = "a#s#u#n#b#p#w";

        public static string PathMagneticAndBlue = "a#s#u#p#m";
        public static string PathMagneticAndBlueStage1 = "a#s#u#a#p#m"; //no use
        public static string PathMagneticAndBlueStage1Start = "a#s#u#t#a#p#m";
        public static string PathMagneticAndBlueStage1End = "a#s#u#n#a#p#m";
        public static string PathMagneticAndBlueStage2 = "a#s#u#b#p#m"; //no use
        public static string PathMagneticAndBlueStage2Start = "a#s#u#t#b#p#m";
        public static string PathMagneticAndBlueStage2End = "a#s#u#n#b#p#m";

        public static string PathBlackAndYellow = "a#s#u#p#y";
        public static string PathBlackAndYellowStage1 = "a#s#u#a#p#y"; //no use
        public static string PathBlackAndYellowStage1Start = "a#s#u#t#a#p#y";
        public static string PathBlackAndYellowStage1End = "a#s#u#n#a#p#y";
        public static string PathBlackAndYellowStage2 = "a#s#u#b#p#y"; //no use
        public static string PathBlackAndYellowStage2Start = "a#s#u#t#b#p#y";
        public static string PathBlackAndYellowStage2End = "a#s#u#n#b#p#y";

        public static string EndState = "a#s#e#n";

        /*
        black and yellow package state: a#s#u#p#y || p - package || y - black and yellow
			Stage1: a#s#u#a#p#y
				Start: a#s#u#t#a#p#y
				End: a#s#u#n#a#p#y
			Stage2:	a#s#u#b#p#y
				Start: a#s#u#t#b#p#y
				End: a#s#u#n#b#p#y
         */
        public static async Task<byte[]> ConvertStringToByteArray(string message) {
            
            return await Task.Run(() => {
                byte[] bytes = Encoding.ASCII.GetBytes(message + "\n");
                return bytes;
            });
        }
    }
}
