using UnityEngine;
using RosSharp.RosBridgeClient;

public class RosBatterySubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Standard.String>
{
    public string messageData;
    public HudDisplay hudDisplay;
   

    protected override void Start()
    {
        base.Start();
    }

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Standard.String message)
    {
        messageData = message.data;
    }
}
