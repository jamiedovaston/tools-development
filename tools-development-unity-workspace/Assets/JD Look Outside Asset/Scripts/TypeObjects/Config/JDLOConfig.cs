using System.IO;
using UnityEngine;

namespace JD.LookOutside.Utilities
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Config")]
    public class JDLOConfig : ScriptableObject
    {
        [field: SerializeField] public string hostDomain { get; private set; } = "https://localhost:3000/";
        public static JDLOConfig instance;
        public static string Domain
        {
            get
            {
                if (instance == null) instance = Resources.Load<JDLOConfig>("JD/Data/Config/JDLO Config");

                Debug.Assert(instance != null, DebugFormatting.FormatError("No 'JDLO Config' found in path 'JD/Data/Config/JDLO Config'. (is the data object missing?)"));

                return instance.hostDomain;
            }
        }
    }
}