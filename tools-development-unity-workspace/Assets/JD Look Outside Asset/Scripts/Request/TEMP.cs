using UnityEngine;
using JD.LookOutside;
using System;
using TMPro;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        [field: SerializeField] public TMP_Text m_TimeText { get; private set; }
        [field: SerializeField] public TMP_Text m_RequestedTimeText { get; private set; }
        [field: SerializeField] public TMP_Text m_LocationName { get; private set; }

        private async void Start()
        {
            if (await Services.Init())
            {
                LocationServices.SetLocation(m_FrmwrkLocation.Location, async () =>
                {
                    LookOutside.Models.Weather weather = await WeatherServices.GetWeather();
                    DateTime time = TimeServices.GetTime();

                    Debug.Log(weather.description);
                    Debug.Log(time);
                    m_LocationName.text = m_FrmwrkLocation.Location.m_Location;
                });
            }
        }

        public void FixedUpdate()
        {
            if(Services.Initialised)
            {
                DateTime time = TimeServices.GetTime();
                m_TimeText.text = time.ToString();
            }
        }

        public void ShowCurrentDateTime()
        {
            DateTime time = TimeServices.GetTime();

            m_RequestedTimeText.text = time.ToString();
        }
    }
}