using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class IntSubscriber : Subscriber<Messages.Standard.Int16>
    {
        public int messageData;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Messages.Standard.Int16 message)
        {
            messageData = message.data;
        }
    }
}