using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace U.Universal.Sound
{
    public static partial class AudioMixerFunctions
    {

        #region FIND

        public static AudioMixer LoadFromResources(string path)
        {
            return Resources.Load<AudioMixer>(path);
        }

        public static AudioMixer LoadFromResourcesAudioMixersFolder(string path)
        {
            return Resources.Load<AudioMixer>("Audio/Mixers/" + path);
        }

        #endregion

    }
}
