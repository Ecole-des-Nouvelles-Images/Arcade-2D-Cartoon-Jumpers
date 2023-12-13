using UnityEditor;
using UnityEngine;

using Master.Scripts.Player;

namespace Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerCustomInspector : UnityEditor.Editor
    {
        private SerializedProperty _enableTestMapInputs;
        private SerializedProperty _initialMaxHealth;
        private SerializedProperty _startingDashType;
        private SerializedProperty _startingWeaponType;
        private SerializedProperty _aimingDashIndicator;
        private SerializedProperty _aimingShootIndicator;

        private void OnEnable()
        {
            _enableTestMapInputs = serializedObject.FindProperty("_enableTestMapInputs");
            _initialMaxHealth = serializedObject.FindProperty("_initialMaxHealth");
            _startingDashType = serializedObject.FindProperty("_startingDashType");
            _startingWeaponType = serializedObject.FindProperty("_startingWeaponType");
            _aimingDashIndicator = serializedObject.FindProperty("AimingDashIndicator");
            _aimingShootIndicator = serializedObject.FindProperty("AimingShootIndicator");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Enable Test Map Inputs");
                _enableTestMapInputs.boolValue = EditorGUILayout.Toggle(GUIContent.none, _enableTestMapInputs.boolValue, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            if (_enableTestMapInputs.boolValue) {
                EditorGUILayout.HelpBox("Warning: Using test/debug inputs", MessageType.Warning);
            }
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_initialMaxHealth);
            EditorGUILayout.PropertyField(_startingDashType);
            EditorGUILayout.PropertyField(_startingWeaponType);

            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_aimingDashIndicator, new GUIContent("Aiming Dash Indicator"));
            EditorGUILayout.PropertyField(_aimingShootIndicator, new GUIContent("Aiming Shoot Indicator"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
