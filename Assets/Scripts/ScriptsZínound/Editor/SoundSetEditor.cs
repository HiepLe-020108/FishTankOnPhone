using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zindeaxx.SoundSystem
{
    /// <summary>
    /// Draws a custom Inspector for InventoryItem assets
    /// Actually, we will draw the default inspector but use the ability to draw a custom preview and render a custom asset's thumbnail
    /// </summary>
    [CustomEditor(typeof(SoundSet), true, isFallback = false)]
    public class SoundSetEditor : Editor
    {

        /// <summary>
        /// Tells if the Object has a custom preview
        /// </summary>
        public override bool HasPreviewGUI()
        {
            return false;
        }

        public override VisualElement CreateInspectorGUI()
        {
            return base.CreateInspectorGUI();
        }

        public override void OnInspectorGUI()
        {
            SerializedObject soundsetObject = new SerializedObject(target);
            soundsetObject.Update();

            SoundSet sounds = (SoundSet)target;

            EditorGUILayout.LabelField("Sound Settings", EditorStyles.boldLabel);
            var spatialBlendProperty = soundsetObject.FindProperty("SpatialBlend");
            EditorGUILayout.PropertyField(soundsetObject.FindProperty("VolumeAmount"));
            EditorGUILayout.PropertyField(soundsetObject.FindProperty("Pitch"));
            EditorGUILayout.PropertyField(spatialBlendProperty);
            EditorGUILayout.PropertyField(soundsetObject.FindProperty("Priority"));
            EditorGUILayout.PropertyField(soundsetObject.FindProperty("VolumeID"), new GUIContent("Volume Identifier"));
           
            if (spatialBlendProperty.floatValue > 0)
            {
                EditorGUILayout.PropertyField(soundsetObject.FindProperty("MinDistance"), new GUIContent("Minimum Distance: "));
                EditorGUILayout.PropertyField(soundsetObject.FindProperty("MaxDistance"), new GUIContent("Maximum Distance: "));
            }

            EditorGUILayout.PropertyField(soundsetObject.FindProperty("LoopedSounds"));
            var spatializeProperty = soundsetObject.FindProperty("Spatialize");
            EditorGUILayout.PropertyField(spatializeProperty);

            if (spatializeProperty.boolValue)
            {
                EditorGUILayout.PropertyField(soundsetObject.FindProperty("SpatializePostEffects"));
            }
            EditorGUILayout.PropertyField(soundsetObject.FindProperty("m_Clips"));


            soundsetObject.ApplyModifiedProperties();
        }


        public float GetFloatValueFromString(string s)
        {
            s = s.Replace(',', '.');
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out float f))
            {
                return f;
            }
            else
                return 0;
        }

        public int GetIntValueFromString(string s)
        {
            s = s.Replace(',', '.');
            if (int.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out int f))
            {
                return f;
            }
            else
                return 0;
        }
    }
}