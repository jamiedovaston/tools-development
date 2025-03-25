using UnityEngine;

namespace JD.LookOutside.Location
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Location ADVANCED", fileName = "ADVANCED_LOCATION_NAME")]
    public class LocationAdvancedSO : ScriptableObject
    {
        [SerializeField] protected LocationAdvanced m_Location;
        public LocationAdvanced Location => m_Location;
    }
}