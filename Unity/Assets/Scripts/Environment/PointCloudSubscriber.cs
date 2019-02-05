using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Sensor;

public class PointCloudSubscriber : MonoBehaviour
{
    private RosSocket RosSocket;

    public int RgbMessageNumber = 0;
    public int depthMessageNumber = 0;

    private const string DEPTH_TOPIC = "/camera/color/image_raw/compressedDepth";
    private const string COLOR_TOPIC = "/camera/color/image_raw/compressed";

    private void Start()
    {
        if (RosSocket != null)
        {
            // subscribe depth topic
            RosSocket.Subscribe<CompressedImage>(DEPTH_TOPIC, receiveDepthImage);
            // subscribe color topic
            RosSocket.Subscribe<CompressedImage>(COLOR_TOPIC, receiveRgbImage);
        }
    }

    private void receiveRgbImage(Message message)
    {
        RgbMessageNumber++;
        CompressedImage RgbImage = (CompressedImage)message;
    }

    private void receiveDepthImage(Message message)
    {
        depthMessageNumber++;
        CompressedImage depthImage = (CompressedImage)message;

    }
}
