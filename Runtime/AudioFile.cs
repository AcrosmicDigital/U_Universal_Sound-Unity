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
        
        public AudioClip audioClip;
        [Space(8)]
        public bool rndVol = false;
        [Range(0f, 1f)] public float volMin = 1f;
        [Range(0f, 1f)] public float volMax = 1f;
        [Space(8)]
        public bool rndPitch = false;
        [Range(-3f, 3f)] public float pitchMin = 1f;
        [Range(-3f, 3f)] public float pitchMax = 1f;

        // The virtual values are returned if exist
        [NonSerialized] private float? virtualVol = null;
        [NonSerialized] private float? virtualPitch = null;

        public float volume { 
            get 
            {
                // If exist virtual value
                if (virtualVol != null)
                    return (float)virtualVol;

                if (rndVol)
                {
                    return StaticFunctions.RandomFloat(volMin.MinMaxFloat(0f, 1f), volMax.MinMaxFloat(0f, 1f)).MinMaxFloat(0f, 1f);
                }
                else
                {
                    return volMax.MinMaxFloat(0f, 1f);
                }
            }
            set
            {
                virtualVol = value.MinMaxFloat(0f, 1f);
            }
        }

        public float pitch
        {
            get
            {
                // If exist virtual value
                if (virtualPitch != null)
                    return (float)virtualPitch;

                if (rndPitch)
                {
                    return StaticFunctions.RandomFloat(pitchMin.MinMaxFloat(-3f, 3f), pitchMax.MinMaxFloat(-3f, 3f)).MinMaxFloat(-3f, 3f);
                }
                else
                {
                    return pitchMax.MinMaxFloat(-3f, 3f);
                }
            }
            set
            {
                virtualPitch = value.MinMaxFloat(-3f, 3f);
            }
        }

        public void RestoreVol()
        {
            virtualVol = null;
        }

        public void RestorePitch()
        {
            virtualPitch = null;
        }
    }
}
