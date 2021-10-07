using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound
{

    [CustomEditor(typeof(AudioAlbum))]
    [CanEditMultipleObjects()]
    public class AudioAlbumCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            AudioAlbum c = (AudioAlbum)target;

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playlists"), true);

            serializedObject.ApplyModifiedProperties();

        }
    }

}
