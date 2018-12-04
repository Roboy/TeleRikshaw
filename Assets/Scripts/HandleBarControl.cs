using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBarControl : MonoBehaviour {

    public GameObject LeftController;
    public GameObject RightController;

    private bool m_ControllerInitialized = false;

    private Vector3 m_ForwardVector;
    private Vector3 m_ControllerForwardVector;
    private float m_SteerAnglel;

    private Vector3 m_Up = new Vector3(0, 1, 0);

    private void Start()
    {
        InitializeHandleBar();
    }

    void Update () {
        if (m_ControllerInitialized)
        {
            getOrientationOfVRController();
            steerHandleBar();
        }
	}

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

    private void getOrientationOfVRController()
    {
        Vector3 ctline = LeftController.transform.position - RightController.transform.position;
        ctline.y = 0;
        Vector3.OrthoNormalize(ref ctline, ref m_Up, ref m_ControllerForwardVector);
        m_SteerAnglel = Vector3.SignedAngle(m_ForwardVector, m_ControllerForwardVector, Vector3.up);
    }

    private void steerHandleBar()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, m_SteerAnglel, 0));
    }
}
