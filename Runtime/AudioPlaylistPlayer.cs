using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Universal.Sound
{
    public class AudioPlaylistPlayer : MonoBehaviour
    {

        public enum PlayOnAwkeMode
        {
            Disabled,
            Play,
            PlayDelayed,
            PlayScheduled,
        }


        public bool useDefaultHost = true;
        public GameObject host;
        public AudioPlaylist playlist;
        public PlayOnAwkeMode playOnAwake = PlayOnAwkeMode.Disabled;
        public float time = 0; // Prioridad
        public float delay = 0;

        private void Awake()
        {
            // Try to read the host
            if (!useDefaultHost && host == null)
                host = gameObject;

            if(playOnAwake == PlayOnAwkeMode.Disabled)
            {
                return;
            }
            else if (playOnAwake == PlayOnAwkeMode.Play)
            {
                Play();
            }
            else if(playOnAwake == PlayOnAwkeMode.PlayDelayed)
            {
                PlayDelayed(delay);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayScheduled)
            {
                PlayScheduled(time);
            }
        }


        public void Create()
        {

            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.Create();
            playlist.Create(host);
        }

        public void Play()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.Play();
            playlist.Play(host);
        }

        public void Play(PlayProperties properties)
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.Play(properties);
            playlist.Play(host, properties);
        }

        public void PlayDelayed(float delay)
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.PlayDelayed(delay);
            playlist.PlayDelayed(delay, host);
        }

        public void PlayDelayed(float delay, PlayProperties properties)
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.PlayDelayed(delay, properties);
            playlist.PlayDelayed(delay, host, properties);
        }

        public void PlayScheduled(float time)
        {
            if(playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.PlayScheduled(time);
            playlist.PlayScheduled(time, host);
        }

        public void PlayScheduled(float time, PlayProperties properties)
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) playlist.PlayScheduled(time, properties);
            playlist.PlayScheduled(time, host, properties);
        }

        public void Pause()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            playlist.Pause();
        }

        public void UnPause()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            playlist.UnPause();
        }

        public void Stop()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            playlist.Stop();
        }

        public void Destroy()
        {
            if (playlist == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioPlaylistPlayer: playlist in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            playlist.Destroy();
        }

    }
}

