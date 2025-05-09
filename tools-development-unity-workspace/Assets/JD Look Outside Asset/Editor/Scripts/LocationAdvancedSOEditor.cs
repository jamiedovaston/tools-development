using UnityEditor;
using UnityEngine;

namespace JD.LookOutside
{
    [CustomEditor(typeof(LocationAdvancedSO))]
    public class LocationAdvancedSOEditor : Editor
    {
        private Texture2D banner;

        private void OnEnable()
        {
            banner = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/JD Look Outside Asset/Editor/Banner/advanced-location-banner.png");
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
