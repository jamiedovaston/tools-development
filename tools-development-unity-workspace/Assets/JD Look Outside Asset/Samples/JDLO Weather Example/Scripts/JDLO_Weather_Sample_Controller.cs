using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JD.LookOutside;
using TMPro;

namespace JD.LookOutside.Samples
{
    public class JDLO_Weather_Sample_Controller : MonoBehaviour
    {
        [Serializable]
        public struct Example_LocationExtended
        {
            public LocationEasy m_Location;
            public Sprite m_Image;
        }

        private int m_LocationIndex = 0;
        [SerializeField, Header("Cities")] private List<Example_LocationExtended> m_Cities = new List<Example_LocationExtended>();

        [Header("UI")]
        [SerializeField] private Button m_NextButton;
        [SerializeField] private Button m_PreviousButton;
        [Space(15)]
        [SerializeField] private TMP_Text m_CityText;
        [Space(15)]
        [SerializeField] private Image m_WeatherIcon;
        [SerializeField] private TMP_Text m_WeatherText;
        [Space(15)]
        [SerializeField] private TMP_Text m_TimeText;
        [SerializeField] private TMP_Text m_SunsetText;
        [SerializeField] private TMP_Text m_SunriseText;
        [Space(15)]
        [SerializeField] private Image m_CityImage;

        private async void Awake()
        {
            m_LocationIndex = 0;
            if (await JDLOServices.Init())
            {
                UpdateWeatherUI();
            }
        }

        private void FixedUpdate()
        {
            if (JDLOServices.Initialised)
            {
                DateTime time = TimeServices.GetTime();
                m_TimeText.text = time.ToString();
            }
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
            if (m_LocationIndex + 1 > m_Cities.Count - 1)
                m_LocationIndex = 0;
            else m_LocationIndex++;

            UpdateWeatherUI();
        }

        private void Previous()
        {
            if (m_LocationIndex - 1 < 0)
                m_LocationIndex = m_Cities.Count - 1;
            else m_LocationIndex--;

            UpdateWeatherUI();
        }


        private async void UpdateWeatherUI()
        {
            if (!JDLOServices.Initialised) await JDLOServices.Init();

            Example_LocationExtended m_City = m_Cities[m_LocationIndex];

            LocationServices.SetLocation(m_City.m_Location, async () =>
            {
                Models.Weather m_Weather = await WeatherServices.GetWeather();

                m_CityText.text = m_City.m_Location.m_Location;

                m_WeatherText.text = m_Weather.description;
                m_WeatherIcon.sprite = m_Weather.icon;

                m_SunriseText.text = $"SR: { TimeServices.GetSunriseTime().ToString("HH:mm:ss")}";
                m_SunsetText.text = $"SS: { TimeServices.GetSunsetTime().ToString("HH:mm:ss")}";

                m_CityImage.sprite = m_City.m_Image;
            });
        }
    }
}

