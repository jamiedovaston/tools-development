using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JD.LookOutside.Samples
{
    public class JDLO_Weather_Sample_Controller : MonoBehaviour
    {
        public struct Example_CityExtended
        {
            public LocationAdvanced m_Location;
            public Sprite m_Image;
        }

        private int m_LocationIndex = 0;

        [SerializeField] private Button m_NextButton, m_PreviousButton;

        [field: SerializeField] public List<Example_CityExtended> m_Cities { get; private set; }

        private void Awake()
        {
            m_LocationIndex = 0;
            UpdateWeatherUI();
        }

        private void OnEnable()
        {
            m_NextButton.onClick.AddListener(Next);
            m_PreviousButton.onClick.AddListener(Previous);
        }

        private void OnDisable()
        {
            m_NextButton.onClick.RemoveAllListeners();
            m_PreviousButton.onClick.RemoveAllListeners();
        }

        private void Next()
        {
            if (m_LocationIndex + 1 > m_Cities.Count)
                m_LocationIndex = 0;
            else m_LocationIndex++;

            UpdateWeatherUI();
        }

        private void Previous()
        {
            if (m_LocationIndex + 1 < 0)
                m_LocationIndex = m_Cities.Count;
            else m_LocationIndex--;

            UpdateWeatherUI();
        }


        private async void UpdateWeatherUI()
        {
            if (!JDLOServices.Initialised) await JDLOServices.Init();

            LocationServices.SetLocation(m_Cities[m_LocationIndex].m_Location, async () =>
            {
                Models.Weather m_Weather = await WeatherServices.GetWeather();
            });
        }
    }
}

