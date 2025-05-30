﻿using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using JD.LookOutside.Utilities;
using UnityEditor;

namespace JD.LookOutside
{
    public static class TimeServices
    {
        public static ITimeKeeperable TimeKeeperable;

        public static void Initialise()
        {
            GameObject timeKeeperComponent = Resources.Load<GameObject>("JDLO/Prefabs/Components/JDLOTimeKeeper");
            timeKeeperComponent = UnityEngine.Object.Instantiate(timeKeeperComponent);
            TimeKeeperable = timeKeeperComponent.GetComponent<ITimeKeeperable>();
        }

        private static Models.Time m_Sunset, m_Sunrise;

        public async static Task StartListening()
        {
            Models.Time time = await GetTimeFromServices();

            TimeKeeperable.TimeToTrack(time.unix_timestamp);
        }

        private async static Task<Models.Time> GetTimeFromServices()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest($"{JDLOConfig.Domain}unix-time?lat={LocationServices.CurrentLocation.m_Latitude}&lon={LocationServices.CurrentLocation.m_Longitude}", "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Models.UnixTime unix = JsonUtility.FromJson<Models.UnixTime>(request.downloadHandler.text);
                Models.Time time = new Models.Time(unix.unix_time + unix.timezone_offset);
                m_Sunset = new Models.Time(unix.unix_sunset + unix.timezone_offset);
                m_Sunrise = new Models.Time(unix.unix_sunrise + unix.timezone_offset);
                return time;
            }
            return null;
        }

        public static DateTime GetTime() => TimeKeeperable.GetDateTime();
        public static DateTime GetSunsetTime() => DateTimeOffset.FromUnixTimeSeconds(m_Sunset.unix_timestamp).UtcDateTime;
        public static DateTime GetSunriseTime() => DateTimeOffset.FromUnixTimeSeconds(m_Sunrise.unix_timestamp).UtcDateTime;
    }
}