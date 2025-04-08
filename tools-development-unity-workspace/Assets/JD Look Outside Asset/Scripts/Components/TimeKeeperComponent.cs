using System;
using UnityEngine;

namespace JD.LookOutside
{
    public class TimeKeeperComponent : JDLO_Component, ITimeKeeperable
    {
        public DateTime CurrentDateTimeToTrack
        {
            get; private set;
        }

        void ITimeKeeperable.TimeToTrack(DateTime _newDateTime)
        {
            CurrentDateTimeToTrack = _newDateTime;
        }
    }

    public interface ITimeKeeperable
    {
        public void TimeToTrack(DateTime time);
    }
}
