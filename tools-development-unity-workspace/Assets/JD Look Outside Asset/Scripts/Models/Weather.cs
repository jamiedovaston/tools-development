using System;

namespace JD.LookOutside.Models
{
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    public class UnixTime
    {
        public long unix_time;
        public long unix_sunrise;
        public long unix_sunset;
    }

    public class Time
    {
        public DateTime time;

        public Time(long unix_timestamp)
        {
            time = DateTimeOffset.FromUnixTimeSeconds(unix_timestamp).UtcDateTime;
        }
    }

    public class Location
    {
        public string name;
        public float lat;
        public float lon;
        public string country;
    }
}

