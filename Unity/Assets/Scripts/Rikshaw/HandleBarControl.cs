using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace TeleRikshaw.Rikshaw
{
    [RequireComponent(typeof(RosSpeedPublisher))]
    [RequireComponent(typeof(RosSteerPublisher))]
    [RequireComponent(typeof(RosHornPublisher))]
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

        private float m_LastSteerAngle = 0;
        private float m_SteerAngle = 0;

        private Vector3 m_Up = new Vector3(0, 1, 0);

        private RosSteerPublisher m_SteerPublisher;
        private RosSpeedPublisher m_SpeedPublisher;
        private RosHornPublisher m_HornPublisher;

        private int updateCounter = 0;

        private const float MAX_SPEED_DISPLAY = 8;
        private const float MIN_SPEED_DISPLAY = -8;
        #endregion PRIVATE_VARIABLES

        #region MONOBEHAVIOUR_FUNCTIONS
        private void OnEnable()
        {
            if (ResetHandleBarAction == null || AccelerateAction == null || BrakeAction == null)
            {
                Debug.LogError("actions not assigned");
                return;
            }

            ResetHandleBarAction.AddOnChangeListener(resetHandleBar, SteamVR_Input_Sources.Any);
            AccelerateAction.AddOnChangeListener(accelerate, SteamVR_Input_Sources.Any);
            BrakeAction.AddOnChangeListener(brake, SteamVR_Input_Sources.Any);
            MusicAction.AddOnChangeListener(playMusic, SteamVR_Input_Sources.Any);
        }

        private void Start()
        {
            m_SteerPublisher = GetComponent<RosSteerPublisher>();
            m_SpeedPublisher = GetComponent<RosSpeedPublisher>();
            m_HornPublisher = GetComponent<RosHornPublisher>();
            InitializeHandleBar();
        }

        void Update()
        {
            if (m_ControllerInitialized)
            {
                getOrientationOfVRController();
                steerHandleBar();
                //Debug.Log(m_SteerAngle);
                int lsa = (int)m_LastSteerAngle;
                int sa = (int)m_SteerAngle;
                updateCounter++;
                if (sa != lsa && updateCounter > 10)
                {
                    updateCounter = 0;
                    m_SteerPublisher.PublishSteeringMessage(m_SteerAngle);
                }
                //System.Threading.Thread.Sleep(500);
            }
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
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        #endregion PUBLIC_MEMBER_FUNCTIONS

        #region PRIVATE_MEMBER_FUNCTIONS
        private void getOrientationOfVRController()
        {
            Vector3 ctline = LeftController.transform.position - RightController.transform.position;
            ctline.y = 0;
            Vector3.OrthoNormalize(ref ctline, ref m_Up, ref m_ControllerForwardVector);
            
            m_LastSteerAngle = m_SteerAngle;
            m_SteerAngle = Vector3.SignedAngle(m_ForwardVector, m_ControllerForwardVector, Vector3.up);
        }

        private void steerHandleBar()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, m_SteerAngle, 0));
        }
        
        private void resetHandleBar(SteamVR_Action_In actionIn)
        {
            InitializeHandleBar();
        }

        private void accelerate(SteamVR_Action_In action_In)
        {
            SteamVR_Action_Single act = (SteamVR_Action_Single)action_In;
            float speed = act.GetAxis(SteamVR_Input_Sources.RightHand);
            m_SpeedPublisher.PublishSpeedMessage(speed);
            Hud.DisplaySpeed((int)(speed*MAX_SPEED_DISPLAY));
        }

        private void brake(SteamVR_Action_In action_In)
        {
            SteamVR_Action_Single act = (SteamVR_Action_Single)action_In;
            float speed = act.GetAxis(SteamVR_Input_Sources.LeftHand);
            m_SpeedPublisher.PublishSpeedMessage(-speed);
            Debug.Log(-speed);
            Hud.DisplaySpeed((int)(-speed*MIN_SPEED_DISPLAY));
        }

        private void playMusic(SteamVR_Action_In action_In)
        {
            m_HornPublisher.PlayMusic();
        }

        #endregion PRIVATE_MEMBER_FUNCTIONS
    }

}