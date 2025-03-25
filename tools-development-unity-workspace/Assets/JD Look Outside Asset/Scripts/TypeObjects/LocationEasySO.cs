using UnityEngine;

namespace JD.LookOutside.Location
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Location", fileName = "LOCATION_NAME")]
    public class LocationEasySO : ScriptableObject
    {
        [SerializeField] protected LocationEasy m_Location;
        public LocationEasy Location => m_Location;
    }
}