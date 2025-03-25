using JD.LookOutside.Weather;
using JD.LookOutside.Time;
using UnityEngine;
using UnityEditor;
using JD.LookOutside.Location;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        private void Start()
        {
            LocationServices.SetLocation(m_FrmwrkLocation.Location, async ()=>
            {
                LookOutside.Models.Weather weather = await JD.LookOutside.Weather.WeatherServices.GetWeather();
                LookOutside.Models.Time time = await TimeServices.GetTime();

                Debug.Log(weather.description);
                Debug.Log(time.time);
            });
        }
    }
}
