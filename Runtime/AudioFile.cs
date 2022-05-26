using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace U.Universal.Sound
{
    [Serializable]
    public class AudioFile
    {

        [SerializeField] private AudioClip audioClip;
        [SerializeField] private int replays = 0; // if this clip must repeat when finish 0 = no repeat
        [Range(0f, 256)] [SerializeField] private int priority = 128; // no added
        [Space(8)]
        [SerializeField] private bool rndVol = false;
        [Range(0f, 1f)] [SerializeField] private float volMin = 1f;
        [Range(0f, 1f)] [SerializeField] private float volMax = 1f;
        [Space(8)]
        [SerializeField] public bool rndPitch = false;
        [Range(-3f, 3f)] [SerializeField] private float pitchMin = 1f;
        [Range(-3f, 3f)] [SerializeField] private float pitchMax = 1f;
        [Space(8)]
        [SerializeField] public bool rndPan = false;// no added
        [Range(-1f, 1f)] [SerializeField] private float panMin = 0f;
        [Range(-1f, 1f)] [SerializeField] private float panMax = 0f;
        [Space(8)]
        [Range(0f, 1f)] [SerializeField] private float spatialBlend = 0; // no added
        [Range(0f, 1.1f)] [SerializeField] private float reverbZoneMix = 1; // no added


        public AudioClip AudioClip => audioClip;

        public int Priority => priority.MinMaxInt(0, 256);

        public int Replays => replays.MinInt(0);

        public float Volume { 
            get 
            {
                if (rndVol) return StaticFunctions.RandomFloat(volMin.MinMaxFloat(0f, 1f), volMax.MinMaxFloat(0f, 1f));
                else return volMax.MinMaxFloat(0f, 1f);
            }
        }

        public float Pitch
        {
            get
            {
                if (rndPitch) return StaticFunctions.RandomFloat(pitchMin.MinMaxFloat(-3f, 3f), pitchMax.MinMaxFloat(-3f, 3f));
                else return pitchMax.MinMaxFloat(-3f, 3f);
            }
        }

        public float Pan
        {
            get
            {
                if (rndPan) return StaticFunctions.RandomFloat(panMin.MinMaxFloat(-1f, 1f), panMax.MinMaxFloat(-1f, 1f));
                else return panMax.MinMaxFloat(-1f, 1f);
            }
        }

        public float SpatialBlend => spatialBlend.MinMaxFloat(0f, 1f);

        public float ReverbZone => reverbZoneMix.MinMaxFloat(0f, 1.1f);

    }
}
