using UnityEngine;
using TeleRickshaw.Util;

namespace TeleRickshaw.Game
{
    public class RickshawStateManager : Singleton<RickshawStateManager>
    {
        protected RickshawStateManager() { }

        public Vector3 VirtualRickshawSpeed;
        public Vector3 VirtualRickshawSteer;

        public bool horn;
        public bool music;



        public Vector3 RealRickshawSpeed;
        public Vector3 RealRickshawSteer;
    }

}