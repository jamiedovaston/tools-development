using System.IO;
using UnityEditor;
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
    [CustomEditor(typeof(JDLOConfig))]
    public class JDLOConfigEditor : Editor
    {
        private Texture2D banner;

        private void OnEnable()
        {
            banner = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JD Look Outside Asset/Editor/Banner/jdlo-config-banner.png");
        }

        public override void OnInspectorGUI()
        {
            if (banner != null)
            {
                float inspectorWidth = EditorGUIUtility.currentViewWidth;
                float bannerHeight = (float)banner.height / banner.width * inspectorWidth;

                Rect fullWidthRect = new Rect(0, 0, inspectorWidth, bannerHeight);
                GUI.DrawTexture(fullWidthRect, banner, ScaleMode.ScaleAndCrop);
                GUILayout.Space(bannerHeight);
            }

            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true); // Skip 'm_Script'

            EditorGUI.BeginChangeCheck();
            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

        }
    }
}