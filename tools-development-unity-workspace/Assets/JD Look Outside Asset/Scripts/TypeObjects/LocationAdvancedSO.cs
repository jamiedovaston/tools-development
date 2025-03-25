using UnityEngine;

namespace JD.LookOutside.Location
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Location ADVANCED", fileName = "ADVANCED_LOCATION_NAME")]
    public class LocationAdvancedSO : LocationFrameworkSO
    {
        [Space]
        [SerializeField] protected float m_Latitude;
        [SerializeField] protected float m_Longitude;

        public float latitude { get => m_Latitude; }
        public float longitude { get => m_Longitude; }

        public LocationAdvancedSO(Models.Location data) {
            m_Location = data.name;
            m_Latitude = data.lat;
            m_Longitude = data.lon;
        }
    }
}