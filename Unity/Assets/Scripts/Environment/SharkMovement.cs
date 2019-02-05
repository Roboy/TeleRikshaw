using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public Vector3 SteerAngle;
    public float SwimSpeed = 1;

    private void Update()
    {
        transform.position = transform.position + transform.right * SwimSpeed * Time.deltaTime;
        if (SteerAngle != null)
        {
            transform.Rotate(SteerAngle*Time.deltaTime);
        }
    }
}
