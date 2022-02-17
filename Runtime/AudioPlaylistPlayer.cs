using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Universal.Sound
{
    public class AudioPlaylistPlayer : MonoBehaviour
    {

        public enum PlayOnAwkeModeOptions
        {
            Disabled,
            Play,
            PlayDelayed,
            PlayScheduled,
        }

        [SerializeField] private bool useDefaultHost;  // If Playlist defaultHost will be used
        [SerializeField] private GameObject host;
        [SerializeField] private AudioPlaylist playlist;
        [SerializeField] private PlayOnAwkeModeOptions playOnAwakeMode = PlayOnAwkeModeOptions.Disabled;
        [SerializeField] private float time = 0; // Prioridad
        [SerializeField] private float delay = 0;

        public bool UseDefaultHost => useDefaultHost;
        public PlayOnAwkeModeOptions PlayOnAwakeMode => playOnAwakeMode;

        private void Awake()
        {
            // Try to read the host
            host = gameObject;

            if (playOnAwakeMode == PlayOnAwkeModeOptions.Disabled)
            {
                return;
            }
            else if (playOnAwakeMode == PlayOnAwkeModeOptions.Play)
            {
                Play();
            }
            else if(playOnAwakeMode == PlayOnAwkeModeOptions.PlayDelayed)
            {
                PlayDelayed(delay);
            }
            else if (playOnAwakeMode == PlayOnAwkeModeOptions.PlayScheduled)
            {
                PlayScheduled(time);
            }
        }

        private void CheckForNullPlaylist()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
            }
        }

        public void Play()
        {
            CheckForNullPlaylist();

            if (useDefaultHost) playlist?.Play();
            else playlist?.Play(host);
        }

        public void PlayDelayed(float delay)
        {
            CheckForNullPlaylist();

            if (useDefaultHost) playlist?.PlayDelayed(delay);
            else playlist?.PlayDelayed(delay, host);
        }

        public void PlayScheduled(float time)
        {
            CheckForNullPlaylist();

            if (useDefaultHost) playlist?.PlayScheduled(time);
            else playlist?.PlayScheduled(time, host);
        }

        public void Pause()
        {
            CheckForNullPlaylist();

            playlist?.Pause();
        }

        public void UnPause()
        {
            CheckForNullPlaylist();

            playlist?.UnPause();
        }

        public void Stop()
        {
            CheckForNullPlaylist();

            playlist?.Stop();
        }

        public void Destroy()
        {
            CheckForNullPlaylist();

            playlist?.DestroyAllSources();
        }

    }
}

