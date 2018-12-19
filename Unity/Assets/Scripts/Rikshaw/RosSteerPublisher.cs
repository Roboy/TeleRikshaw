using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRikshaw.Rikshaw
{
    public class RosSteerPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
    {
        private const string STEER_CHAR = "D";
        private const float MIN_ANGLE = 40;
        private const float MAX_ANGLE = 140;
        private const float FORWARD_ANGLE = 90;

        protected override void Start()
        {
            base.Start();
        }

        public void PublishSteeringMessage(float angle)
        {
            int steerAngle = (int)Mathf.Clamp(FORWARD_ANGLE - angle, MIN_ANGLE, MAX_ANGLE);
            string msgString = STEER_CHAR + steerAngle.ToString();

            RosSharp.RosBridgeClient.Messages.Standard.String msg = new RosSharp.RosBridgeClient.Messages.Standard.String(msgString);
            Publish(msg);
        }
    }
}
