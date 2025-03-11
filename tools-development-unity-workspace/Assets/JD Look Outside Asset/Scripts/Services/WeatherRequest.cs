using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using JD.Models;
using System.Threading.Tasks;

namespace JD.Weather
{
    public static class WeatherRequest
    {
        public static class London 
        { 
            public static void GetTime()
            {

            }

            public async static Task<Models.Weather> GetWeather()
            {
                DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
                UnityWebRequest request = new UnityWebRequest("https://api.openweathermap.org/data/3.0/onecall?lat=51.5073219&lon=-0.1276474&appid=c74b686274bd6cfd89569e10c7849f95", "GET");
                request.downloadHandler = downloadHandler;

                await request.SendWebRequest();

                if(request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"{request.downloadHandler.text}");
                    Models.Weather weather = JsonUtility.FromJson<Models.Weather>(request.downloadHandler.text);
                    Debug.Log(weather.ToString());
                    return weather;
                }
                return new Models.Weather();
            }
        }
    }
}