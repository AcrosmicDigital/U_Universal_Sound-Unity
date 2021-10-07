using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound
{

    [CustomEditor(typeof(AudioPlaylist))]
    [CanEditMultipleObjects()]
    public class AudioPlaylistCustomInspector : Editor
    {
        
        public override void OnInspectorGUI()
        {

            AudioPlaylist c = (AudioPlaylist)target;

            GUILayout.Space(8);
            GUILayout.Label("Name:   " + c.name);
            GUILayout.Space(8);

            #region AudioFiles

            if (GUILayout.Button("Restore default"))
            {
                foreach (var file in c.audioFiles)
                {
                    file.volMin = 1;
                    file.volMax = 1;
                    file.pitchMin = 1;
                    file.pitchMax = 1;
                }
            }
            GUILayout.Label("If random are false, max value will be used");
            GUILayout.Label("Deselect and select Playlist to apply changes");


            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("audioFiles"), true);

            
            #endregion

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeMode"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultHostToDDOL"), new GUIContent("Permanent"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playMode"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("loopMode"), true);
            // If replicas or loop
            EditorGUI.indentLevel++;
            if(c.loopMode == AudioPlaylist.LoopMode.Count)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iterationsVal"), new GUIContent("Iterations"), true);
            else if(c.loopMode == AudioPlaylist.LoopMode.Clone)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("replicasVal"), new GUIContent("Replicas"), true);
            EditorGUI.indentLevel--;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("audioMixerGroup"), true);


            serializedObject.ApplyModifiedProperties();

        }
    }

}
