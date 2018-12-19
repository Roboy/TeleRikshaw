using UnityEngine;
using RosSharp.RosBridgeClient;

namespace TeleRikshaw.Rikshaw
{
    public class RosHornPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
    {
        private const string MUSIC_CHAR = "J";

        protected void Start()
        {
            base.Start();
        }

        public void PlayMusic()
        {
            string msgString = MUSIC_CHAR + 1.ToString();
            RosSharp.RosBridgeClient.Messages.Standard.String msg = new RosSharp.RosBridgeClient.Messages.Standard.String(msgString);
            Publish(msg);
        }
    }
}
