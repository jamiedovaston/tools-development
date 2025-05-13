using Unity.VisualScripting;
using UnityEngine;

namespace JD.LookOutside
{
    [System.Serializable]
    public class LocationEasy
    {
        public string m_Location;

        public LocationEasy() { }

        public LocationEasy(string location)
        {
            m_Location = location;
        }
    }
}