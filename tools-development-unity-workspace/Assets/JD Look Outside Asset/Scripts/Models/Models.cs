using System;

namespace JD.LookOutside.Models
{
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;

        public Weather(int id, string main, string description, string icon)
        {
            this.id = id;
            this.main = main;
            this.description = description;
            this.icon = icon;
        }
    }

    public class UnixTime
    {
        public int timezone_offset;
        public long unix_time;
        public long unix_sunrise;
        public long unix_sunset;
    }

    public class Time
    {
        public long unix_timestamp;
        public Time(long unix_timestamp)
        {
            this.unix_timestamp = unix_timestamp;
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

