using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Standard;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class RosSteerPublisher : Publisher<Int16>
    {
        private const string STEER_CHAR = "D";
        private const float MIN_ANGLE = 55;
        private const float MAX_ANGLE = 125;
        private const float FORWARD_ANGLE = 80;

        private Coroutine publishCoroutine = null;

        public float PublishInterval = 0.3f;

        protected override void Start()
        {
            base.Start();

            if (publishCoroutine == null)
            {
                publishCoroutine = StartCoroutine(PublishSteerMessage());
            }
        }

        private IEnumerator PublishSteerMessage()
        {
            while (true)
            {
                int angle = (int)Mathf.Clamp(FORWARD_ANGLE + (RickshawStateManager.Instance.VirtualRickshawSteer.z * 45), MIN_ANGLE, MAX_ANGLE);
                Int16 msg = new Int16(angle);
                Publish(msg);

                yield return new WaitForSeconds(PublishInterval);
            }

        }

        public void StopPublish()
        {
            StopCoroutine(publishCoroutine);
        }
    }
}
