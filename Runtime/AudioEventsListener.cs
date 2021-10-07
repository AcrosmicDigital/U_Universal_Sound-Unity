using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace U.Universal.Sound
{
    public class AudioEventsListener : MonoBehaviour
    {

        [NonSerialized] public AudioSource source;
        [NonSerialized] public Action<AudioEventsListener> IfIsStopped;


        private void Update()
        {

            try
            {

                // If is playing cant be stoped
                if (source.isPlaying)
                    return;

                try
                {
                    IfIsStopped.Invoke(this);
                }
                catch (Exception) { }

            }
            catch (Exception)
            {
                Destroy(this);
            }

        }
    }
}
