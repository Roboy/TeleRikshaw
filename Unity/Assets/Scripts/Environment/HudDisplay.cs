using System;
using System.Collections;
using UnityEngine;
using TMPro;
using TeleRickshaw.Game;

namespace TeleRickshaw.Rickshaw
{
    public class HudDisplay : MonoBehaviour
    {
        public float UpdateInterval = 0.1f;

        public TextMeshPro SpeedDisplay;
        public TextMeshPro BatteryDisplay;

        private Coroutine UpdateHUDCoroutine = null;

        private float BatteryLevel;
        private Vector3 RickshawSpeed;

        private void Start()
        {
            if (SpeedDisplay == null || BatteryDisplay == null)
            {
                return;
            }
            if(UpdateHUDCoroutine == null)
            {
                UpdateHUDCoroutine = StartCoroutine(UpdateHUD());
            }
        }

        public void DisplaySpeed(Vector3 speed)
        {
            if (SpeedDisplay == null)
            {
                return;
            }
            Debug.Log(speed);
            if (speed == null)
            {
                SpeedDisplay.text = String.Format("Speed: {0:F1} km/h", 2.6);
                return;
            }
            
            SpeedDisplay.text = String.Format("Speed: {0:F1} km/h", speed.x);
        }

        public void DisplayBattery(float batteryLevel)
        {
            if(BatteryDisplay == null)
            {
                return;
            }

            try
            {
                if (batteryLevel > 0.75)
                {
                    BatteryDisplay.text = "<sprite=\"BatterySprite\" index=0>";
                }
                else if (batteryLevel > 0.50)
                {
                    BatteryDisplay.text = "<sprite=\"BatterySprite\" index=1>";
                }
                else if (batteryLevel > 0.25)
                {
                    BatteryDisplay.text = "<sprite=\"BatterySprite\" index=2>";
                }
                else if (batteryLevel > 0.05)
                {
                    BatteryDisplay.text = "<sprite=\"BatterySprite\" index=3>";
                }
                else
                {
                    BatteryDisplay.text = "<sprite=\"BatterySprite\" index=4>";
                }
            }
            catch (Exception)
            {
                BatteryDisplay.text = "<sprite=\"BatterySprite\" index=0>";
                Debug.Log("Battery Exception");
            }
        }

        private IEnumerator UpdateHUD()
        {
            while (true)
            {
                RickshawSpeed = RickshawStateManager.Instance.RealRickshawSpeed;
                BatteryLevel = RickshawStateManager.Instance.RealRickshawBatteryLevel;
                DisplaySpeed(RickshawSpeed);
                DisplayBattery(BatteryLevel);
                yield return new WaitForSeconds(UpdateInterval);
            }
        }
    }
}