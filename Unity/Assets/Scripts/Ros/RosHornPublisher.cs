using UnityEngine;
using System.Collections;
using RosSharp.RosBridgeClient;

namespace TeleRickshaw.Rickshaw
{
    public class RosHornPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.Int16>
    {
        private bool isBuzzerPlaying = false;

        protected void Start()
        {
            base.Start();
        }

        public void PlayBuzzer(int playCount)
        {
            if (!isBuzzerPlaying)
            {
                isBuzzerPlaying = true;
                StartCoroutine(PublishBuzzerMsg(playCount));
            }
        }

        private IEnumerator PublishBuzzerMsg(int playCount)
        {
            Debug.Log("publish music");
            RosSharp.RosBridgeClient.Messages.Standard.Int16 msg = new RosSharp.RosBridgeClient.Messages.Standard.Int16(playCount);
            Publish(msg);
            yield return new WaitForSeconds(1);
            isBuzzerPlaying = false;
        }
    }
}
