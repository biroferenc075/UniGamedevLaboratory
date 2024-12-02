using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyWaveList))]
public class EnemyWaveListEditor : Editor
{
    SerializedProperty waves;

    private void OnEnable() { waves = serializedObject.FindProperty("waves"); }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Add New Wave"))
        {
            waves.arraySize++;
            var newWave = waves.GetArrayElementAtIndex(waves.arraySize - 1);
            newWave.FindPropertyRelative("startTime").floatValue = 0;
            newWave.FindPropertyRelative("repeat").intValue = 0;
            newWave.FindPropertyRelative("repeatDelay").floatValue = 0;
            newWave.FindPropertyRelative("wavePrefabIdx").intValue = 0;
        }

        for (int i = 0; i < waves.arraySize; i++)
        {
            SerializedProperty waveProperty = waves.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(waveProperty.FindPropertyRelative("startTime"));
            EditorGUILayout.PropertyField(waveProperty.FindPropertyRelative("repeat"));
            EditorGUILayout.PropertyField(waveProperty.FindPropertyRelative("repeatDelay"));
            EditorGUILayout.PropertyField(waveProperty.FindPropertyRelative("wavePrefabIdx"));

            if (GUILayout.Button("Remove Wave"))
            {
                waves.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
