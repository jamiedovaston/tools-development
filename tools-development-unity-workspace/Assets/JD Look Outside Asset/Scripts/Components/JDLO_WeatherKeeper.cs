using JD.LookOutside.Utilities;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace JD.LookOutside
{
    public class JDLO_WeatherKeeper : JDLO_Component, IWeatherKeeperable
    {
        public event Func<Task> onRequest;

        public Models.Weather CurrentWeather
        {
            get; private set;
        }

        private float localLastWeatherTrack;

        [SerializeField] private float time;

        public void FixedUpdate()
        {
            time = Time.realtimeSinceStartup - localLastWeatherTrack;
            if (time >= JDLOConfig.m_UpdatedWeatherRequestDelay)
            {
                onRequest?.Invoke();
                onRequest = null;
            }
        }

        void IWeatherKeeperable.SetCurrentWeather(Models.Weather m_Weather, Func<Task> onRequest)
        {
            CurrentWeather = m_Weather;
            localLastWeatherTrack = Time.realtimeSinceStartup;
            this.onRequest = onRequest;
        }
    }
    public interface IWeatherKeeperable
    {
        public event Func<Task> onRequest;
        public Models.Weather CurrentWeather { get; }
        public void SetCurrentWeather(Models.Weather m_Weather, Func<Task> onRequest);
    }
}
