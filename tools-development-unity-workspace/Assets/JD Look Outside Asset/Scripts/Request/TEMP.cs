using UnityEngine;
using JD.LookOutside;
using System;
using TMPro;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        [SerializeField] private TMP_Text m_TimeText;

        private async void Start()
        {
            if (await Services.Get())
            {
                LocationServices.SetLocation(m_FrmwrkLocation.Location, async () =>
                {
                    LookOutside.Models.Weather weather = await WeatherServices.GetWeather();
                    DateTime time = TimeServices.GetTime();

                    Debug.Log(weather.description);
                    Debug.Log(time);
                });
            }
        }

        public void FixedUpdate()
        {
            DateTime time = TimeServices.GetTime();

            m_TimeText.text = time.ToString();
        }

        public void ShowCurrentDateTime()
        {
            DateTime time = TimeServices.GetTime();

            m_TimeText.text = time.ToString();
        }
    }
}