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
        [Range(0f, 256f)] [SerializeField] private int priority = 128; // no added
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
        [Range(-1f, 1f)] [SerializeField] private float panMin = 1f;
        [Range(-1f, 1f)] [SerializeField] private float panMax = 1f;
        [Space(8)]
        [Range(0f, 1f)] [SerializeField] private float spatialBlend = 0; // no added
        [Range(0f, 1f)] [SerializeField] private float reverbZoneMix = 1; // no added


        // The virtual values are returned if exist
        [NonSerialized] private int? virtualPriority = null;
        [NonSerialized] private float? virtualVol = null;
        [NonSerialized] private float? virtualPitch = null;
        [NonSerialized] private float? virtualPan = null;
        [NonSerialized] private float? virtualBlend = null;
        [NonSerialized] private float? virtualReverbZone = null;

        public AudioClip AudioClip => audioClip;

        public int Priority
        {
            get
            {
                // If exist virtual value
                if (virtualPriority != null)
                    return (int)virtualPriority;

                return priority.MinMaxInt(0, 256);
            }
            set
            {
                virtualPriority = value.MinMaxInt(0, 256);
            }
        }

        public float Volume { 
            get 
            {
                // If exist virtual value
                if (virtualVol != null)
                    return (float)virtualVol;

                if (rndVol)
                {
                    return StaticFunctions.RandomFloat(volMin.MinMaxFloat(0f, 1f), volMax.MinMaxFloat(0f, 1f));
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

        public float Pitch
        {
            get
            {
                // If exist virtual value
                if (virtualPitch != null)
                    return (float)virtualPitch;

                if (rndPitch)
                {
                    return StaticFunctions.RandomFloat(pitchMin.MinMaxFloat(-3f, 3f), pitchMax.MinMaxFloat(-3f, 3f));
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

        public float Pan
        {
            get
            {
                // If exist virtual value
                if (virtualPan != null)
                    return (float)virtualPan;

                if (rndPan)
                {
                    return StaticFunctions.RandomFloat(panMin.MinMaxFloat(-1f, 1f), panMax.MinMaxFloat(-1f, 1f));
                }
                else
                {
                    return panMax.MinMaxFloat(-1f, 1f);
                }
            }
            set
            {
                virtualPan = value.MinMaxFloat(-1f, 1f);
            }
        }

        public float SpatialBlend
        {
            get
            {
                // If exist virtual value
                if (virtualBlend != null)
                    return (float)virtualBlend;

                return spatialBlend.MinMaxFloat(0f, 1f);
            }
            set
            {
                virtualBlend = value.MinMaxFloat(0f, 1f);
            }
        }

        public float ReverbZone
        {
            get
            {
                // If exist virtual value
                if (virtualReverbZone != null)
                    return (float)virtualReverbZone;

                return reverbZoneMix.MinMaxFloat(0f, 1.1f);
            }
            set
            {
                virtualReverbZone = value.MinMaxFloat(0f, 1.1f);
            }
        }



        public void RestorePriority()
        {
            virtualPriority = null;
        }

        public void RestoreVol()
        {
            virtualVol = null;
        }

        public void RestorePitch()
        {
            virtualPitch = null;
        }

        public void RestorePan()
        {
            virtualPan = null;
        }

        public void RestoreSpatialBlend()
        {
            virtualBlend = null;
        }

        public void RestoreReverbZoneMix()
        {
            virtualReverbZone = null;
        }

        public void SetDefaultValues()
        {
            priority = 128;
            volMin = 1;
            volMax = 1;
            pitchMin = 1;
            pitchMax = 1;
            panMin = 1;
            panMax = 1;
            spatialBlend = 0;
            reverbZoneMix = 1;
        }

    }
}
