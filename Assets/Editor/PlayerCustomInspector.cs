using Charlie.Scripts.Player;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerCustomInspector : UnityEditor.Editor
    {
        private SerializedProperty _enableTestMapInputs;
        private SerializedProperty _maxHp;
        private SerializedProperty _startingDashType;
        private SerializedProperty _startingProjectileType;
        private SerializedProperty _aimingDashIndicator;
        private SerializedProperty _aimingShootIndicator;

        private void OnEnable()
        {
            _enableTestMapInputs = serializedObject.FindProperty("_enableTestMapInputs");
            _maxHp = serializedObject.FindProperty("_maxHp");
            _startingDashType = serializedObject.FindProperty("_startingDashType");
            _startingProjectileType = serializedObject.FindProperty("_startingProjectileType");
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
            
            EditorGUILayout.PropertyField(_maxHp);
            EditorGUILayout.PropertyField(_startingDashType);
            EditorGUILayout.PropertyField(_startingProjectileType);

            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_aimingDashIndicator, new GUIContent("Aiming Dash Indicator"));
            EditorGUILayout.PropertyField(_aimingShootIndicator, new GUIContent("Aiming Shoot Indicator"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
