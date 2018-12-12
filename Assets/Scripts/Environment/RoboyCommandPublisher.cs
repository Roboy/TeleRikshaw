using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class RoboyCommandPublisher : Publisher<Messages.Rikshaw.RoboyCommand>
    {

        private Messages.Rikshaw.RoboyCommand message;

        private Coroutine cou;

        protected override void Start()
        {
            base.Start();
            initializeMessage();
            cou = StartCoroutine(publishMes());
        }

        private void initializeMessage()
        {
            message = new Messages.Rikshaw.RoboyCommand
            {
                cmd = "test_cmd",
                content = "test_content"
            };
        }

        private IEnumerator publishMes()
        {
            while (true)
            {
                Publish(message);
                yield return new WaitForSeconds(1);
            }
        }
    }

}