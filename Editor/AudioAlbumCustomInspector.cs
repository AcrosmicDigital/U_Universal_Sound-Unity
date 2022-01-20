using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound.Editor
{
    [CustomEditor(typeof(AudioAlbum))]
    [CanEditMultipleObjects()]
    public class AudioAlbumCustomInspector : UnityEditor.Editor
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
