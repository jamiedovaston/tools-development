using JD.Weather;
using UnityEngine;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        async void Start()
        {
            await WeatherRequest.London.GetWeather();
        }
    }
}
