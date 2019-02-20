using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRickshaw.Rickshaw
{
    public class ControlRosPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
    {

        public enum ControlType
        {
            steer,
            horn,
            accelerate,
            brake
        }

        private Dictionary<ControlType, string> m_ControlDict;

        protected override void Start()
        {
            base.Start();

            m_ControlDict = new Dictionary<ControlType, string>();
            m_ControlDict.Add(ControlType.steer, "a");
            m_ControlDict.Add(ControlType.accelerate, "s");
            m_ControlDict.Add(ControlType.brake, "s");
            m_ControlDict.Add(ControlType.horn, "h");
        }

        public void PublishControlMessage(ControlType controlType, float controlValue)
        {
            publishControlMessageRC(controlType, controlValue);
        }

        private void publishControlMessageRC(ControlType controlType, float controlValue)
        {
            string messageString = m_ControlDict[controlType] + controlValue.ToString();
            RosSharp.RosBridgeClient.Messages.Standard.String rosString = new RosSharp.RosBridgeClient.Messages.Standard.String(messageString);
            Publish(rosString);
        }
    }
}

