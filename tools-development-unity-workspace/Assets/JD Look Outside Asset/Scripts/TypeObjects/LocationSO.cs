using Unity.VisualScripting;
using UnityEngine;

namespace JD.LookOutside.Location
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Location", fileName = "LOCATION_NAME")]
    public class LocationFrameworkSO : ScriptableObject
    {
        [SerializeField] protected string m_Location;
        public string Location { get => m_Location; }
    }
}