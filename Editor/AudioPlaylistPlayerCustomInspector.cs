using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound
{

    [CustomEditor(typeof(AudioPlaylistPlayer))]
    [CanEditMultipleObjects()]
    public class AudioPlaylistPlayerCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            AudioPlaylistPlayer c = (AudioPlaylistPlayer)target;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useDefaultHost"), true);
            EditorGUI.indentLevel++;
            if (!c.useDefaultHost)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("host"), true);
            EditorGUI.indentLevel--;

            GUILayout.Space(5);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playlist"), true);

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnAwake"), true);
            EditorGUI.indentLevel++;

            if(c.playOnAwake == AudioPlaylistPlayer.PlayOnAwkeMode.PlayDelayed)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"), true);

            if (c.playOnAwake == AudioPlaylistPlayer.PlayOnAwkeMode.PlayScheduled)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("time"), true);

            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();

        }
    }


}
