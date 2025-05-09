using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using JD.LookOutside.Utilities;
using JD.LookOutside.Models;
using System;

namespace JD.LookOutside
{
    public static class WeatherServices
    {
        public static Action<Weather> OnUpdatedWeather;

        public static IWeatherKeeperable WeatherKeeperable;

        public static bool IsNull(this LocationEasy frmwrkData)
        {
            LocationAdvanced data = frmwrkData as LocationAdvanced;

            if (data == null) return true;

            if (data.m_Longitude == float.NaN || data.m_Latitude == float.NaN) return true;
            
            return false;
        }

        public static void Initialise()
        {
            GameObject weatherKeeperComponent = Resources.Load<GameObject>("JDLO/Prefabs/Components/JDLOWeatherKeeper");
            weatherKeeperComponent = UnityEngine.Object.Instantiate(weatherKeeperComponent);
            WeatherKeeperable = weatherKeeperComponent.GetComponent<IWeatherKeeperable>();
        }

        public async static Task SetCurrentWeather()
        {
            Models.Weather weather = await GetWeatherFromServices();

            Debug.Log(DebugFormatting.Format("Updated weather!"));

            OnUpdatedWeather?.Invoke(weather);

            WeatherKeeperable.SetCurrentWeather(weather, SetCurrentWeather);
        }

        public static Models.Weather GetWeather()
        {
            return WeatherKeeperable.CurrentWeather;
        }

        private async static Task<Models.Weather> GetWeatherFromServices()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest($"{ JDLOConfig.Domain }weather?lat={ LocationServices.CurrentLocation.m_Latitude }&lon={ LocationServices.CurrentLocation.m_Longitude }", "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Models.Weather weather = new Models.Weather(JsonUtility.FromJson<Models.Weather.Weather_JSON>(request.downloadHandler.text));
                return weather;
            }
            return null;
        }
    }
}