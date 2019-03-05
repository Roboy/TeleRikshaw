using UnityEngine;
using Valve.VR;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class HandleBarControl : MonoBehaviour
    {

        #region PUBLIC_VARIABLES
        public GameObject LeftController;
        public GameObject RightController;
        public SteamVR_Action_Boolean ResetHandleBarAction;
        public SteamVR_Action_Boolean MusicAction;

        public SteamVR_Action_Single AccelerateAction;
        public SteamVR_Action_Single BrakeAction;

        public HudDisplay Hud;

        #endregion // PUBLIC_VARIABLES

        #region PRIVATE_VARIABLES
        private bool m_ControllerInitialized = false;

        private Vector3 m_ForwardVector;
        private Vector3 m_ControllerForwardVector;

        private float m_SteerAngle = 0;

        private Vector3 m_Up = new Vector3(0, 1, 0);

        private int updateCounter = 0;

        private const float MAX_SPEED_DISPLAY = 8;
        private const float MIN_SPEED_DISPLAY = -8;
        #endregion PRIVATE_VARIABLES

        #region MONOBEHAVIOUR_FUNCTIONS
        private void OnEnable()
        {
            if (ResetHandleBarAction == null || AccelerateAction == null || BrakeAction == null || MusicAction == null)
            {
                Debug.LogError("actions not assigned");
                return;
            }

            ResetHandleBarAction.AddOnChangeListener(ResetHandleBar, SteamVR_Input_Sources.Any);
            AccelerateAction.AddOnChangeListener(Accelerate, SteamVR_Input_Sources.Any);
            BrakeAction.AddOnChangeListener(Brake, SteamVR_Input_Sources.Any);
            MusicAction.AddOnChangeListener(PlayMusic, SteamVR_Input_Sources.Any);
        }

        private void Start()
        {
            InitializeHandleBar();
        }

        private void Update()
        {
            if (!m_ControllerInitialized) return;

            //  calculate the orientation of the real handlebar
            GetOrientationOfVrController();

            //  steer the virtual handlebar
            SteerHandleBar();
            //Debug.Log(m_SteerAngle);

            //  update the state of steer in RickshawStateManager
            //  have to flip the sign of the steering angle due to different coordinate system of unity and gazebo
            RickshawStateManager.Instance.VirtualRickshawSteer = new Vector3(0, 0, -Mathf.Deg2Rad * m_SteerAngle);

        }
        #endregion MONOBEHAVIOUR_FUNCTIONS

        #region PUBLIC_MEMBER_FUNCTIONS
        public void InitializeHandleBar()
        {
            //  get the forward direction vector from the two controllers
            if (LeftController != null && RightController != null)
            {
                Vector3 ctline = LeftController.transform.position - RightController.transform.position;
                ctline.y = 0; // project to horizontal plane
                Vector3.OrthoNormalize(ref ctline, ref m_Up, ref m_ForwardVector);
                m_ControllerInitialized = true;
                // reset the rotation of the handlebar
                transform.localRotation = Quaternion.identity;
            }
        }
        #endregion PUBLIC_MEMBER_FUNCTIONS

        #region PRIVATE_MEMBER_FUNCTIONS
        private void GetOrientationOfVrController()
        {
            Vector3 ctline = LeftController.transform.position - RightController.transform.position;
            ctline.y = 0;
            Vector3.OrthoNormalize(ref ctline, ref m_Up, ref m_ControllerForwardVector);
            
            m_SteerAngle = Vector3.SignedAngle(m_ForwardVector, m_ControllerForwardVector, Vector3.up);
        }

        private void SteerHandleBar()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, m_SteerAngle, 0));
        }
        
        private void ResetHandleBar(SteamVR_Action_In actionIn)
        {
            InitializeHandleBar();
        }

        private void Accelerate(SteamVR_Action_In action_In)
        {
            SteamVR_Action_Single act = (SteamVR_Action_Single)action_In;
            float speed = act.GetAxis(SteamVR_Input_Sources.RightHand);

            //  update the state of speed in RickshawStateManager
            RickshawStateManager.Instance.VirtualRickshawSpeed = new Vector3(speed, 0, 0);
        }

        private void Brake(SteamVR_Action_In action_In)
        {
            SteamVR_Action_Single act = (SteamVR_Action_Single)action_In;
            float speed = act.GetAxis(SteamVR_Input_Sources.LeftHand);

            //  update the state of speed in RickshawStateManager
            //  !!brake means set the speed to 0!!
            RickshawStateManager.Instance.VirtualRickshawSpeed = new Vector3(0, 0, 0);
        }

        private void PlayMusic(SteamVR_Action_In action_In)
        {
            RickshawStateManager.Instance.Music = true;
        }

        #endregion PRIVATE_MEMBER_FUNCTIONS
    }

}