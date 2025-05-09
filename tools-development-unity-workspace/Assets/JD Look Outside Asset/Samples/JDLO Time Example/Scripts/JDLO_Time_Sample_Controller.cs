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

        public void SetLocation(LocationEasySO m_Location) => SetLocation(m_Location.Location);
        public void SetLocation(LocationAdvancedSO m_Location) => SetLocation(m_Location.Location);
        private void SetLocation(LocationEasy m_Location)
        {
            LocationServices.SetLocation(m_Location, () =>
            {
                LookOutside.Models.Weather weather = WeatherServices.GetWeather();
                DateTime time = TimeServices.GetTime();

                Debug.Log(weather.description);
                Debug.Log(weather.icon);
                Debug.Log(time);
                m_LocationName.text = m_Location.m_Location;
            });
        }
    }
}