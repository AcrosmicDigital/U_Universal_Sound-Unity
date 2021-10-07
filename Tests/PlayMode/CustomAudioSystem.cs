using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U.Universal.Sound;


public static partial class TestClasses
{
    public class CustomAudioSystem : AudioAlbumPlayer
    {

        public enum AudioFilesNames
        {
            One = 0,
            Two = 1,
            Three = 2,
        }

        public void Play(AudioFilesNames name)
        {
            Play(name.ToString());
        }

        public void PlayByNumber(AudioFilesNames name)
        {
            Play(name);
        }











        private AudioPlaylist fileTwo;

        private void Start()
        {
            fileTwo = album.GetPlaylist("Two");
        }



        public void PlayOne()
        {
            Play(0);
        }



    }
}