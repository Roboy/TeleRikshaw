using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class FloatSubscriber : Subscriber<Messages.Standard.Float32>
    {
        public float messageData;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Messages.Standard.Float32 message)
        {
            messageData = message.data;
        }
    }
}