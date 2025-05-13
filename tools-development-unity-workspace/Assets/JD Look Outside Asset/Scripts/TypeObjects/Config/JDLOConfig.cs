using System.IO;
using UnityEngine;

namespace JD.LookOutside.Utilities
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Config")]
    public class JDLOConfig : ScriptableObject
    {
        public const string JDLO_MAIN_DOMAIN = "https://jportfolio-tool-development.ulquuu.easypanel.host/";
        [field: SerializeField, Tooltip("Put domain here to override primary domain")] public string OverrideDefaultDomain { get; private set; }
        [field: SerializeField, Range(10.0f, 120.0f)] public float UpdateWeatherRequestDelay { get; private set; } = 60.0f;

        private static JDLOConfig instance;
        public static string Domain
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<JDLOConfig>("JDLO/Data/Config/JDLO Config");
                }

                Debug.Assert(instance != null, DebugFormatting.FormatError("No 'JDLO Config' found in path 'JDLO/Data/Config/JDLO Config'. (is the data object missing?)"));

                if (string.IsNullOrEmpty(instance.OverrideDefaultDomain))
                    return JDLO_MAIN_DOMAIN;

                return instance.OverrideDefaultDomain;
            }
        }

        public static float m_UpdatedWeatherRequestDelay 
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<JDLOConfig>("JDLO/Data/Config/JDLO Config");
                }

                Debug.Assert(instance != null, DebugFormatting.FormatError("No 'JDLO Config' found in path 'JDLO/Data/Config/JDLO Config'. (is the data object missing?)"));

                return instance.UpdateWeatherRequestDelay;
            }
        }
    }
}