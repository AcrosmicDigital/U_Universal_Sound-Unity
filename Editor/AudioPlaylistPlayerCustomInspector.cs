using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound.Editor
{
    [CustomEditor(typeof(AudioPlaylistPlayer))]
    [CanEditMultipleObjects()]
    public class AudioPlaylistPlayerCustomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

            AudioPlaylistPlayer c = (AudioPlaylistPlayer)target;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useDefaultHost"), true);
            EditorGUI.indentLevel++;
            if (!c.UseDefaultHost)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("host"), true);
            EditorGUI.indentLevel--;

            GUILayout.Space(5);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playlist"), true);

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnAwakeMode"), true);
            EditorGUI.indentLevel++;

            if(c.PlayOnAwakeMode == AudioPlaylistPlayer.PlayOnAwkeModeOptions.PlayDelayed)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"), true);

            if (c.PlayOnAwakeMode == AudioPlaylistPlayer.PlayOnAwkeModeOptions.PlayScheduled)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("time"), true);

            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();

        }
    }
}
