using UnityEngine;
using RosSharp.RosBridgeClient;
using TeleRickshaw.Game;

public class RosBatterySubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Standard.Float32>
{
    public float messageData;
   

    protected override void Start()
    {
        base.Start();
    }

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Standard.Float32 message)
    {
        messageData = message.data;
        RickshawStateManager.Instance.RealRickshawBatteryLevel = messageData;
    }
}
