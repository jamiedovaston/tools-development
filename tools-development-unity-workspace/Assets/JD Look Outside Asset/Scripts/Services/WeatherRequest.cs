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
            public static string location { get; } = "London";

            public static float lat { get; }  = 51.5073219f;
            public static float lon { get; } = -0.1276474f;

            public async static Task<Models.Time> GetTime()
            {
                DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
                UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/unix-time?lat={0}&lon={1}", lat, lon), "GET");
                request.downloadHandler = downloadHandler;

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"{request.downloadHandler.text}");
                    Models.UnixTime unix = JsonUtility.FromJson<Models.UnixTime>(request.downloadHandler.text);
                    Models.Time time = new Models.Time(unix.unix_time);
                    return time;
                }               
                return null;
            }

            public async static Task<Models.Weather> GetWeather()
            {
                DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
                UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/weather?lat={0}&lon={1}", lat, lon), "GET");
                request.downloadHandler = downloadHandler;

                await request.SendWebRequest();

                if(request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log($"{request.downloadHandler.text}");
                    Models.Weather weather = JsonUtility.FromJson<Models.Weather>(request.downloadHandler.text);
                    return weather;
                }
                return null;
            }
        }
    }
}