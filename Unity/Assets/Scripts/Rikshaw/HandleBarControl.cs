using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace TeleRikshaw.Rikshaw
{
    public class HandleBarControl : MonoBehaviour
    {

        #region PUBLIC_VARIABLES
        public GameObject LeftController;
        public GameObject RightController;
        public SteamVR_Action_Boolean ResetHandleBarAction;
        #endregion // PUBLIC_VARIABLES

        #region PRIVATE_VARIABLES
        private bool m_ControllerInitialized = false;

        private Vector3 m_ForwardVector;
        private Vector3 m_ControllerForwardVector;

        private float m_LastSteerAngle;
        private float m_SteerAngle;

        private Vector3 m_Up = new Vector3(0, 1, 0);
        #endregion PRIVATE_VARIABLES

        #region MONOBEHAVIOUR_FUNCTIONS
        private void OnEnable()
        {
            if (ResetHandleBarAction == null)
            {
                Debug.LogError("No plant action assigned");
                return;
            }

            ResetHandleBarAction.AddOnChangeListener(resetHandleBar, SteamVR_Input_Sources.Any);
        }

        private void Start()
        {
            InitializeHandleBar();
        }

        void Update()
        {
            if (m_ControllerInitialized)
            {
                getOrientationOfVRController();
                steerHandleBar();
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
        #endregion PRIVATE_MEMBER_FUNCTIONS
    }

}