using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using JD.LookOutside.Utilities;

namespace JD.LookOutside
{
    public static class WeatherServices
    {
        public static bool IsNull(this LocationEasy frmwrkData)
        {
            LocationAdvanced data = frmwrkData as LocationAdvanced;

            if (data == null) return true;

            if (data.m_Longitude == float.NaN || data.m_Latitude == float.NaN) return true;
            
            return false;
        }

        public async static Task<Models.Weather> GetWeather()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/weather?lat={0}&lon={1}", LocationServices.CurrentLocation.m_Latitude, LocationServices.CurrentLocation.m_Longitude), "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Models.Weather weather = JsonUtility.FromJson<Models.Weather>(request.downloadHandler.text);
                return weather;
            }
            return null;
        }
    }
}

namespace JD.LookOutside
{
    public static class LocationServices
    {
        private static LocationAdvanced m_Location = null;
        public static LocationAdvanced CurrentLocation
        {
            get
            {
                if(m_Location == null)
                {
                    Debug.LogError(DebugHandling.FormatError($"Trying to access location before it is set! Use 'LocationRequest.SetLocation(LocationFrameworkSO, OnComplete(optional))'"));
                    return null;
                }
                return m_Location;
            }
        }

        public async static void SetLocation(LocationEasy m_Data, Action onComplete = null)
        {
            if (!m_Data.IsNull())
            {
                SetLocation(m_Data as LocationAdvanced, onComplete);
                Debug.Log("Location is advanced!");
            }

            Models.Location location = await GetLocation(m_Data.m_Location);

            LocationAdvanced advcedData = new LocationAdvanced();
            advcedData.Initialise(location);

            SetLocation(advcedData, onComplete);
        }

        public static void SetLocation(LocationAdvanced m_Data, Action onComplete = null)
        {
            m_Location = m_Data;

            Debug.Log(DebugHandling.Format($"Set location to {m_Data.m_Location}. Co-ordinates: lat: {m_Location.m_Latitude} lon: {m_Location.m_Longitude}"));

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
                Models.Location location_data = JsonUtility.FromJson<Models.Location>(request.downloadHandler.text);
                return location_data;
            }

            return null;
        }
    }
}