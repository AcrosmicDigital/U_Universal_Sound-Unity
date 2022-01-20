using UnityEngine;
using UnityEditor;

namespace U.Universal.Sound.Editor
{
    public class VersionMenuButton : EditorWindow
    {

        [MenuItem("Universal/Sound/Version")]
        public static void PrintVersion()
        {

            Debug.Log(" U Framework: Universal Sound v1.0.0 for Unity");

        }
    }
}