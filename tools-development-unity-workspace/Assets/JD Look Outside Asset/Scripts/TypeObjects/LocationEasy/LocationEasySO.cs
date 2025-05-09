using UnityEditor;
using UnityEngine;

namespace JD.LookOutside
{
    [CreateAssetMenu(menuName = "JD/JD Look Outside/Location", fileName = "LOCATION_NAME")]
    public class LocationEasySO : ScriptableObject
    {
        [SerializeField] protected LocationEasy m_Location;
        public LocationEasy Location => m_Location;
    }

    [CustomEditor(typeof(LocationEasySO))]
    public class LocationEasySOEditor : Editor
    {
        private Texture2D banner;

        private void OnEnable()
        {
            banner = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JD Look Outside Asset/Editor/Banner/easy-location-banner.png");
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
            property.NextVisible(true);

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