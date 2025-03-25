using JD.LookOutside.Weather;
using JD.LookOutside.Time;
using UnityEngine;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        async void Start()
        {
            Models.Weather weather = await JD.LookOutside.Weather.WeatherRequest.GetWeather();
            Models.Time time = await TimeRequest.GetTime();

            Debug.Log(weather.description);
            Debug.Log(time.time);
        }
    }
}
