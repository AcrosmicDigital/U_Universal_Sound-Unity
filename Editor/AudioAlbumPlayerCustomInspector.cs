using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound
{


    [CustomEditor(typeof(AudioAlbumPlayer))]
    [CanEditMultipleObjects()]
    public class AudioAlbumPlayerCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            AudioAlbumPlayer c = (AudioAlbumPlayer)target;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useDefaultHost"), true);
            EditorGUI.indentLevel++;
            if (!c.useDefaultHost)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("host"), true);
            EditorGUI.indentLevel--;

            GUILayout.Space(5);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("album"), true);

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnAwake"), true);
            EditorGUI.indentLevel++;

            if(c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByIndex ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByIndexDelayed ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByIndexScheduled)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("playlistIndex"), true);

            if (c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByName ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByNameDelayed ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByNameScheduled)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("playlistName"), true);

            if(c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByIndexDelayed ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByNameDelayed ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayAllDelayed ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayRandomDelayed)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"), true);

            if (c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayAllScheduled ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayRandomScheduled ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByIndexScheduled ||
                c.playOnAwake == AudioAlbumPlayer.PlayOnAwkeMode.PlayByNameScheduled)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("time"), true);

            EditorGUI.indentLevel--;
            serializedObject.ApplyModifiedProperties();

        }
    }


}
