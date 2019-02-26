using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Geometry;
using TeleRickshaw.Game;
using Vector3 = RosSharp.RosBridgeClient.Messages.Geometry.Vector3;

namespace TeleRickshaw.Rickshaw
{
    public class RosRickshawMovementPublisher : Publisher<Twist>
    {
        private const float MIN_SPEED = 0;
        private const float MAX_SPEED = 1.66F;
        private const float ZERO_SPEED = 0;

        private const float MIN_ANGLE = -0.52f;
        private const float MAX_ANGLE = 0.52f;
        private const float FORWARD_ANGLE = 0f;

        private Coroutine publishCoroutine = null;

        public float PublishPeriod = 0.3f;

        protected override void Start()
        {
            base.Start();
            if (publishCoroutine == null)
            {
                publishCoroutine = StartCoroutine(PublishSpeedMessage());
            }
        }

        private IEnumerator PublishSpeedMessage()
        {
            while (true)
            {
                Twist msg = new Twist();
                Vector3 speed = new Vector3
                {
                    x = Mathf.Clamp(ZERO_SPEED + (RickshawStateManager.Instance.VirtualRickshawSpeed.x * MAX_SPEED),
                        MIN_SPEED, MAX_SPEED),
                    y = 0,
                    z = 0
                };
                Vector3 steer = new Vector3
                {
                    x = 0,
                    y = 0,
                    z = Mathf.Clamp(RickshawStateManager.Instance.VirtualRickshawSteer.z, MIN_ANGLE, MAX_ANGLE)
                };
                msg.linear = speed;
                msg.angular = steer;
                Publish(msg);

                yield return new WaitForSeconds(PublishPeriod);
            }
        }

        public void StopPublish()
        {
            StopCoroutine(publishCoroutine);
        }
    }
}