using UnityEngine;
using RosSharp.RosBridgeClient;

public class RosSpeedSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Standard.String>
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

        hudDisplay.DisplaySpeed(messageData);
    }
}
