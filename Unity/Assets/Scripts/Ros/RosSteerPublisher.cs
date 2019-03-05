﻿using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Standard;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class RosSteerPublisher : Publisher<Int16>
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
                int angle = (int)Mathf.Clamp(FORWARD_ANGLE + (RickshawStateManager.Instance.VirtualRickshawSteer.z * 50), MIN_ANGLE, MAX_ANGLE);
                Int16 msg = new Int16(angle);
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
