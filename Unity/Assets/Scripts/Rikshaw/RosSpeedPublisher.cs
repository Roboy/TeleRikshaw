using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRikshaw.Rikshaw
{
    public class RosSpeedPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
    {
        private const string SPEED_CHAR = "S";
        private const float MIN_SPEED = 0;
        private const float MAX_SPEED = 510;
        private const float ZERO_SPEED = 255;

        protected override void Start()
        {
            base.Start();
        }

        public void PublishSpeedMessage(float speed)
        {
            int spd = (int)Mathf.Clamp(ZERO_SPEED + (speed * 254), 0, 510);

            string msgString = SPEED_CHAR + spd.ToString();

            RosSharp.RosBridgeClient.Messages.Standard.String msg = new RosSharp.RosBridgeClient.Messages.Standard.String(msgString);
            Publish(msg);
        }
    }
}