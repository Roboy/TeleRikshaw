using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotate : MonoBehaviour
{
    public float RotateSpeed = 360;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, RotateSpeed*Time.deltaTime, 0));
    }
}
