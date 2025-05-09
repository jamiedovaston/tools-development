using UnityEngine;

namespace JD.LookOutside
{
    [System.Serializable]
    public class LocationAdvanced : LocationEasy
    {
        [Space]
        public float m_Latitude = float.NaN;
        public float m_Longitude = float.NaN;

        public void Initialise(Models.Location data) {
            m_Location = data.name;
            m_Latitude = data.lat;
            m_Longitude = data.lon;
        }

        public void Initialise(LocationEasy data)
        {
            m_Location = data.m_Location;
        }
    }
}