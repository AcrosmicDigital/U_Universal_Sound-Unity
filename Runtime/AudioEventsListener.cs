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
        [NonSerialized] public int replays;
        [NonSerialized] public Action<AudioEventsListener, int> IfIsStopped;


        private void Update()
        {

            try
            {

                // If is playing cant be stoped
                if (source.isPlaying)
                    return;

                try
                {
                    IfIsStopped.Invoke(this, replays);
                }
                catch (Exception) { }

            }
            catch (Exception)
            {
                Destroy(this);
            }

        }

        public void Replayed()
        {
            source.Play();
            replays = (replays - 1).MinInt(0);
        }

    }
}
