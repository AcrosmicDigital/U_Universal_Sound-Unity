using System.Collections;
using System.Collections.Generic;
using U.Universal.Sound;
using UnityEngine;

public class TestSoundsSceneManager : MonoBehaviour
{
    [SerializeField] private AudioPlaylist ap;
    
    public void PauseTime()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    public void Mute()
    {
        if (ap.Mute == true) ap.Mute = false;
        else ap.Mute = true;
    }

}
