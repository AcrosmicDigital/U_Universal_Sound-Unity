using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U.Universal.Sound;

public static partial class TestClasses
{
    public class CustomScriptUseAudioSystem : MonoBehaviour
    {

        AudioAlbumPlayer audioSystem;

        private void Start()
        {
            audioSystem = GetComponent<AudioAlbumPlayer>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            audioSystem.Play("One");
        }

    }
}