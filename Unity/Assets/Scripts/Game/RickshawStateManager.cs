using UnityEngine;
using TeleRickshaw.Util;

namespace TeleRickshaw.Game
{
    public class RickshawStateManager : Singleton<RickshawStateManager>
    {
        protected RickshawStateManager() { }

        public Vector3 VirtualRickshawSpeed { get; set; }
        public Vector3 VirtualRickshawSteer { get; set; }

        public bool Horn { get; set; }
        public bool Music { get; set; }

        public Vector3 RealRickshawSpeed { get; set; }
        public Vector3 RealRickshawSteer { get; set; }

        public float RealRickshawBatteryLevel { get; set; }
    }
}