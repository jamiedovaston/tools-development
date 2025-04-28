using UnityEngine;
using JD.LookOutside;
using System;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        private async void Start()
        {
            if(await Services.Get())
            {
                LocationServices.SetLocation(m_FrmwrkLocation.Location, async ()=>
                {
                    LookOutside.Models.Weather weather = await WeatherServices.GetWeather();
                    DateTime time = TimeServices.GetTime();

                    Debug.Log(weather.description);
                    Debug.Log(time);
                });
            }
        }
    }
}