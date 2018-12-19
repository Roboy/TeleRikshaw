using System;
using UnityEngine;
using TMPro;

public class HudDisplay : MonoBehaviour
{
    public TextMeshPro SpeedDisplay;

    public void DisplaySpeed(float speed)
    {
        if(SpeedDisplay == null)
        {
            return;
        }

        SpeedDisplay.text = String.Format("Speed {0:f} km/h", speed);
    }
}
