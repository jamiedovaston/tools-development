using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using JD.LookOutside.Utilities;

namespace JD.LookOutside
{
    public static class LocationServices
    {
        public static Action<LocationAdvanced> OnLocationSet;

        private static LocationAdvanced m_Loc;
        private static LocationAdvanced m_Location
        {
            get => m_Loc;
            set
            {
                m_Loc = value;
                OnLocationSet?.Invoke(m_Loc);
            }
        }

        public static LocationAdvanced CurrentLocation
        {
            get
            {
                if(m_Location == null)
                {
                    Debug.LogError(DebugFormatting.FormatError($"Trying to access location before it is set! Use 'LocationRequest.SetLocation(LocationFrameworkSO, OnComplete(optional))'"));
                    return null;
                }
                return m_Location;
            }
            private set
            {
                m_Location = value;
                OnLocationSet?.Invoke(m_Location);
            }
        }

        public static void SetLocation(string m_Location, Action onComplete = null) => SetLocation(new LocationEasy(m_Location), onComplete);

        public async static void SetLocation(LocationEasy m_Data, Action onComplete = null)
        {
            if (!m_Data.IsNull())
            {
                SetLocation(m_Data as LocationAdvanced, onComplete);
                Debug.Log("Location is advanced!");
            }

            Models.Location location = await GetLocation(m_Data.m_Location);

            if(location != null)
            {
                LocationAdvanced advcedData = new LocationAdvanced();
                advcedData.Initialise(location);
                SetLocation(advcedData, onComplete);
            }
            else Debug.LogError(DebugFormatting.FormatError($"Unable to set location. Location data came back null."));
        }

        private async static void SetLocation(LocationAdvanced m_Data, Action onComplete = null)
        {
            m_Location = m_Data;

            Debug.Log(DebugFormatting.Format($"Set location to {m_Data.m_Location}. Co-ordinates: lat: {m_Location.m_Latitude} lon: {m_Location.m_Longitude}"));

            await TimeServices.StartListening();
            await WeatherServices.SetCurrentWeather();

            onComplete?.Invoke();
        }

        private async static Task<Models.Location> GetLocation(string location)
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest($"{JDLOConfig.Domain}geo-location?location={location}", "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Models.Location location_data = JsonUtility.FromJson<Models.Location>(request.downloadHandler.text);
                return location_data;
            }
            else
                Debug.LogError(DebugFormatting.FormatError($"Couldn't find location from parameter: { location }. Error: { request.downloadHandler.error }"));

            return null;
        }
    }
}