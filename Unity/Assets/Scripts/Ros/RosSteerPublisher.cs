using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Standard;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class RosSteerPublisher : Publisher<String>
    {
        private const string STEER_CHAR = "D";
        private const float MIN_ANGLE = 40;
        private const float MAX_ANGLE = 140;
        private const float FORWARD_ANGLE = 90;

        private Coroutine publishCoroutine = null;

        public float PublishPeriod = 0.3f;

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
                int spd = (int)Mathf.Clamp(FORWARD_ANGLE + (RickshawStateManager.Instance.VirtualRickshawSpeed.x * 50), MIN_ANGLE, MAX_ANGLE);
                string msgString = STEER_CHAR + spd;
                String msg = new String(msgString);
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
