using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace U.Universal.Sound
{
    public class TimeEventsListener : MonoBehaviour
    {

        [NonSerialized] private AudioPlaylist.TimeModeOptions timeModeOptions;
        [NonSerialized] private float time = 0;
        [NonSerialized] private Action timeOver;

        public AudioPlaylist.TimeModeOptions TimeModeOptions
        {
            get { return timeModeOptions; }
            set { timeModeOptions = value; }
        }

        public Action TimeOver
        {
            get { return timeOver; }
            set { timeOver = value; }
        }


        public void SetAndPlay(float time)
        {
            this.time = time.MinFloat(0);
        }

        private void Update()
        {

            if(time <= 0) return;

            if (timeModeOptions == AudioPlaylist.TimeModeOptions.DeltaTime) time -= Time.deltaTime;
            else time -= Time.unscaledDeltaTime;

            if(time <= 0)
            {
                try
                {
                    TimeOver?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError("AudioTimer: " + e);
                }
            }

        }

        private void OnDestroy()
        {
            try
            {
                TimeOver?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError("AudioTimer: " + e);
            }
        }
    }
}
