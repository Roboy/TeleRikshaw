using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraAligner : MonoBehaviour {

    private float headRotation = 0.0f;

	void Update () {
        headRotation = InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.y;
        Debug.Log(headRotation);

        if(headRotation >= 30.0f){
            // TODO: send command to Arduino
            // check back with timo, how camera platform is controlled: 
            // threshold, angle etc ...
        }
	}
}
