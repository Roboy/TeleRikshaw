using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRikshaw.Rikshaw
{
    public class RosRickshawSpeedPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Geometry.Twist>
    {
        private const float MIN_SPEED = 0;
        private const float MAX_SPEED = 1.66F;
        private const float ZERO_SPEED = 0;

        protected override void Start()
        {
            base.Start();
        }

        public void PublishSpeedMessage(float speed)
        {
            float spd = Mathf.Clamp(ZERO_SPEED + (speed * MAX_SPEED), MIN_SPEED, MAX_SPEED);

            RosSharp.RosBridgeClient.Messages.Geometry.Twist msg = new RosSharp.RosBridgeClient.Messages.Geometry.Twist();
            msg.linear.x = spd;
            msg.linear.y = 0;
            msg.linear.z = 0;
            msg.angular.x = 0;
            msg.angular.y = 0;
            msg.angular.z = 0;

            Debug.Log(msg);

            Publish(msg);
            Publish(msg);
            Publish(msg);
        }
    }
}