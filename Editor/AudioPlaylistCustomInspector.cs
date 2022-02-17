using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound.Editor
{
    [CustomEditor(typeof(AudioPlaylist))]
    [CanEditMultipleObjects()]
    public class AudioPlaylistCustomInspector : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {

            AudioPlaylist c = (AudioPlaylist)target;

            GUILayout.Space(8);
            GUILayout.Label("Name:   " + c.name);
            GUILayout.Space(8);

            #region AudioFiles

            GUILayout.Label("If random are false, max value will be used");
            GUILayout.Label("Deselect and select Playlist to apply changes");


            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("audioFiles"), true);


            #endregion

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeMode"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeBetweenPlays"), true);
            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultHostToDDOL"), new GUIContent("Default Host To DDOL"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mute"), true);
            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playMode"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("loopMode"), true);
            // If replicas or loop
            EditorGUI.indentLevel++;
            if (c.LoopMode == AudioPlaylist.LoopModeOptions.LoopCount)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iterations"), new GUIContent("Iterations"), true);
            else if (c.LoopMode == AudioPlaylist.LoopModeOptions.Clone)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("replicas"), new GUIContent("Replicas"), true);
            EditorGUI.indentLevel--;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("output"), true);


            serializedObject.ApplyModifiedProperties();

        }
    }
}