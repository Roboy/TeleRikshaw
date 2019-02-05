using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRikshaw.Rikshaw
{
    public class RosRickshawSteerPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Geometry.Twist>
    {
        private const float MIN_ANGLE = -0.52f;
        private const float MAX_ANGLE = 0.52f;
        private const float FORWARD_ANGLE = 0f;

        protected override void Start()
        {
            base.Start();
        }

        public void PublishSteeringMessage(float angle)
        {
            float rad = (Mathf.PI / 180) * angle;
            float steerAngle = Mathf.Clamp(FORWARD_ANGLE - rad, MIN_ANGLE, MAX_ANGLE);

            RosSharp.RosBridgeClient.Messages.Geometry.Twist msg = new RosSharp.RosBridgeClient.Messages.Geometry.Twist();
            msg.linear.x = 0;
            msg.linear.y = 0;
            msg.linear.z = 0;
            msg.angular.x = 0;
            msg.angular.y = 0;
            msg.angular.z = steerAngle;
            Debug.Log(msg);


            Publish(msg);
            Publish(msg);
            Publish(msg);
        }
    }
}
