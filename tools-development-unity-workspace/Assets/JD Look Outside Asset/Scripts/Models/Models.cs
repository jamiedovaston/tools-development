using System;
using UnityEngine;

namespace JD.LookOutside.Models
{
    public class Weather
    {
        public struct Weather_JSON
        {
            public int id;
            public string main;
            public string description;
            public string icon;
        }

        public int id;
        public string main;
        public string description;
        public Sprite icon;

        public Weather(Weather_JSON m_Weather)
        {
            this.id = m_Weather.id;
            this.main = m_Weather.main;
            this.description = m_Weather.description;
            this.icon = Resources.Load<Sprite>($"JDLO/Weather Icons/{ m_Weather.icon }");
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

