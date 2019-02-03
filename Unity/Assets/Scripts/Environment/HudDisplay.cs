using System;
using UnityEngine;
using TMPro;

public class HudDisplay : MonoBehaviour
{
    public TextMeshPro SpeedDisplay;
    public TextMeshPro BatteryDisplay;

    public void DisplaySpeed(string speed)
    {
        if(SpeedDisplay == null)
        {
            return;
        }


        SpeedDisplay.text = "Speed " + speed + " km/h";
    }

    public void DisplayBattery(string battery)
    {
        try
        {
            int batteryLevel = int.Parse(battery);
            if (batteryLevel > 75)
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=0>";
            }
            else if (batteryLevel > 50)
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=1>";
            }
            else if (batteryLevel > 25)
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=2>";
            }
            else if (batteryLevel > 5)
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=3>";
            }
            else
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=4>";
            }
        } catch (FormatException e)
        {
            
        }
    }
}
