using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JD.LookOutside
{
    public class TimeKeeperComponent : JDLO_Component, ITimeKeeperable
    {
        public static TimeKeeperComponent instance;
        public void Awake()
        {
            if (instance != null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else
                Destroy(gameObject);
        }

        public long CurrentDateTimeToTrack
        {
            get; private set;
        }

        private float localStartTime;

        void ITimeKeeperable.TimeToTrack(long _newUnixTimestamp)
        {
            CurrentDateTimeToTrack = _newUnixTimestamp;
            localStartTime = Time.realtimeSinceStartup;
        }

        public DateTime GetDateTime() 
        {
            float elapsedTime = Time.realtimeSinceStartup - localStartTime;
            long unixNow = CurrentDateTimeToTrack + (long)elapsedTime;

            return DateTimeOffset.FromUnixTimeSeconds(unixNow).UtcDateTime;
        }
    }

    public interface ITimeKeeperable
    {
        public DateTime GetDateTime();
        void TimeToTrack(long _newUnixTimestamp);
    }
}
