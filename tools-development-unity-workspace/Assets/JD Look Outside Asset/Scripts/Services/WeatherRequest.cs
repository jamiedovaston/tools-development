using UnityEngine;
using UnityEngine.Networking;
using JD.Models;
using System.Threading.Tasks;
using JD.LookOutside.Location;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System;
using JetBrains.Annotations;
using JD.LookOutside.Weather;

namespace JD.LookOutside.Weather
{
    public static class WeatherRequest
    {
        public static bool IsNull(this LocationFrameworkSO frmwrkData)
        {
            LocationAdvancedSO data = frmwrkData as LocationAdvancedSO;

            if (string.IsNullOrEmpty(data.longitude) || string.IsNullOrEmpty(data.latitude))
                return true;
            else
                return false;
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

namespace JD.LookOutside.Time
{ 
    public static class TimeRequest
    {
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
    }
}

namespace JD.LookOutside.Location
{
    public static class LocationRequest
    {
        private static LocationAdvancedSO m_Location = null;
        public static LocationFrameworkSO Location
        {
            get
            {
                if(m_Location == null)
                {
                    Debug.LogError($"<color=#FF0000>JD Look Outside | Trying to access location before it is set! Use 'LocationRequest.SetLocation(LocationFrameworkSO)'</color>");
                    return null;
                }
                return m_Location;
            }
        }

        public async static void SetLocation(LocationFrameworkSO frmwrkData, Action onComplete = null)
        {
            if (!frmwrkData.IsNull()) SetLocation(frmwrkData as LocationAdvancedSO, onComplete);

            Models.Location data = await GetLocation(frmwrkData.Location);

            LocationAdvancedSO advcedData = new LocationAdvancedSO(data);
            SetLocation(advcedData, onComplete);
        }

        public static void SetLocation(LocationAdvancedSO advcedData, Action onComplete = null)
        {
            m_Location = advcedData;
            onComplete?.Invoke();
        }

        private async static Task<Models.Location> GetLocation(string location)
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/geo-location?location={0}", location), "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"{request.downloadHandler.text}");
                Models.Location location_data = JsonUtility.FromJson<Models.Location>(request.downloadHandler.text);
                return location_data;
            }
            return null;
        }
    }
}