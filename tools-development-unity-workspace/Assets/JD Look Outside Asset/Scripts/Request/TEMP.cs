using JD.Weather;
using UnityEngine;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        async void Start()
        {
            Models.Weather weather = await WeatherRequest.London.GetWeather();
            Models.Time time = await WeatherRequest.London.GetTime();

            Debug.Log(weather.description);
            Debug.Log(time.time);
        }
    }
}
