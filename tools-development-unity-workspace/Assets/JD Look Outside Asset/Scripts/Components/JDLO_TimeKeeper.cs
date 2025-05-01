using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JD.LookOutside
{
    public class JDLO_TimeKeeper : JDLO_Component, ITimeKeeperable
    {
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
