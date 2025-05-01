using UnityEngine;
using JD.LookOutside;
using System;
using TMPro;
using JD.LookOutside.Models;

namespace JD.LookOutside.Samples
{
    public class JDLO_Time_Sample_Controller : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        [field: SerializeField] public TMP_Text m_TimeText { get; private set; }
        [field: SerializeField] public TMP_Text m_RequestedTimeText { get; private set; }
        [field: SerializeField] public TMP_Text m_LocationName { get; private set; }

        private async void Start()
        {
            if (await JDLOServices.Init())
            {
                SetLocation(m_FrmwrkLocation);
            }
        }

        public void FixedUpdate()
        {
            if(JDLOServices.Initialised)
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

        public void SetLocation(LocationEasySO m_Location)
        {
            LocationServices.SetLocation(m_Location.Location, async () =>
            {
                LookOutside.Models.Weather weather = await WeatherServices.GetWeather();
                DateTime time = TimeServices.GetTime();

                Debug.Log(weather.description);
                Debug.Log(weather.id);
                Debug.Log(time);
                m_LocationName.text = m_Location.Location.m_Location;
            });
        }
    }
}