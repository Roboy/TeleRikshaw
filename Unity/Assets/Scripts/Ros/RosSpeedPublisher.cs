using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Standard;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class RosSpeedPublisher : Publisher<String>
    {
        private const string SPEED_CHAR = "S";
        private const float MIN_SPEED = 0;
        private const float MAX_SPEED = 510;
        private const float ZERO_SPEED = 255;

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
                int spd = (int)Mathf.Clamp(ZERO_SPEED + (RickshawStateManager.Instance.VirtualRickshawSpeed.x * 254), MIN_SPEED, MAX_SPEED);
                string msgString = SPEED_CHAR + spd;
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