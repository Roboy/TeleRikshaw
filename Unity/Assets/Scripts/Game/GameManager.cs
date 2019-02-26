using UnityEngine;
using Valve.VR;

namespace TeleRickshaw.Game
{
    public class GameManager : MonoBehaviour
    {
        public GameObject EnvironmentObjects;
        public GameObject CameraObject;
        public SteamVR_Action_Boolean resetScenePositionAction;
        public Vector3 CameraOffset;

        private void Start()
        {
            if(resetScenePositionAction == null)
            {
                return;
            }

            resetScenePositionAction.AddOnChangeListener(resetScenePosition, SteamVR_Input_Sources.Any);
        }

        private void resetScenePosition(SteamVR_Action_In action_In)
        {
            Debug.Log("position reset!");
            if(EnvironmentObjects != null && CameraObject != null)
            {
                EnvironmentObjects.transform.position = CameraObject.transform.position - CameraOffset;

                EnvironmentObjects.transform.localRotation = CameraObject.transform.rotation;

            }
        }
    }
}