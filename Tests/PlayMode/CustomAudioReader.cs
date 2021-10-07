using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U.Universal.Sound;

internal class CustomAudioReader
{
    

    public CustomAudioReader()
    {

        var audiocontainerA = AudioAlbum.LoadFromResourcesAudioAlbumsFolder("AudioContaiiner");
        var audiocontainerB = AudioAlbum.LoadFromResourcesAudioAlbumsFolder("AudioContaiiner");

        Debug.Log("ObjectOneIs: " + audiocontainerA.GetHashCode());
        Debug.Log("ObjectOneIs: " + audiocontainerB.GetHashCode());


        audiocontainerA.Play("HitOne");
        audiocontainerB.Play("HitOne");

    }


}
