using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U.Universal.Sound {
    public class AudioAlbumPlayer : MonoBehaviour
    {
        public enum PlayOnAwkeMode
        {
            Disabled,

            PlayByName,
            PlayByNameDelayed,
            PlayByNameScheduled,

            PlayByIndex,
            PlayByIndexDelayed,
            PlayByIndexScheduled,

            PlayRandom,
            PlayRandomDelayed,
            PlayRandomScheduled,


            PlayAll,
            PlayAllDelayed,
            PlayAllScheduled,

        }

        public bool useDefaultHost = true;
        public GameObject host;
        public AudioAlbum album;
        public PlayOnAwkeMode playOnAwake = PlayOnAwkeMode.Disabled;
        public string playlistName = "";
        public int playlistIndex = 0;
        public float time = 0;
        public float delay = 0;



        private void Awake()
        {
            // Try to read the host
            if (!useDefaultHost && host == null)
                host = gameObject;


            if (playOnAwake == PlayOnAwkeMode.Disabled)
            {
                return;
            }

            else if(playOnAwake == PlayOnAwkeMode.PlayAll)
            {
                PlayAll();
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayAllDelayed)
            {
                PlayAllDelayed(delay);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayAllScheduled)
            {
                PlayAllScheduled(time);
            }

            else if (playOnAwake == PlayOnAwkeMode.PlayRandom)
            {
                PlayRandom();
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayRandomDelayed)
            {
                PlayRandomDelayed(delay);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayRandomScheduled)
            {
                PlayRandomScheduled(time);
            }

            else if (playOnAwake == PlayOnAwkeMode.PlayByName)
            {
                Play(playlistName);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayByNameDelayed)
            {
                PlayDelayed(playlistName, delay);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayByNameScheduled)
            {
                PlayScheduled(playlistName, time);
            }

            else if (playOnAwake == PlayOnAwkeMode.PlayByIndex)
            {
                Play(playlistIndex);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayByIndexDelayed)
            {
                PlayDelayed(playlistIndex, delay);
            }
            else if (playOnAwake == PlayOnAwkeMode.PlayByIndexScheduled)
            {
                PlayScheduled(playlistIndex, time);
            }
        }



        public void Create(string name)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name +"is not defined"));
                return;
            }

            if (useDefaultHost) album.Create(name);
            album.Create(name, host);
        }

        public void Create(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.Create(index);
            album.Create(index, host);
        }

        public void CreateAll(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.CreateAll();
            album.CreateAll(host);
        }



        public void Play(string clipName)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.Play(clipName);
            album.Play(clipName, host);
        }

        public void Play(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.Play(index);
            album.Play(index, host);
        }

        public void PlayAll()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayAll();
            album.PlayAll(host);
        }

        public void PlayRandom()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayRandom();
            album.PlayRandom(host);
        }

        public void Play(string clipName, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.Play(clipName, properties);
            album.Play(clipName, host, properties);
        }

        public void Play(int index, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.Play(index, properties);
            album.Play(index, host, properties);
        }

        public void PlayAll(PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayAll(properties);
            album.PlayAll(host, properties);
        }

        public void PlayRandom(PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayRandom(properties);
            album.PlayRandom(host, properties);
        }



        public void PlayDelayed(string clipName, float delay)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayDelayed(clipName, delay);
            album.PlayDelayed(clipName, delay, host);
        }

        public void PlayDelayed(int index, float delay)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayDelayed(index, delay);
            album.PlayDelayed(index, delay, host);
        }

        public void PlayAllDelayed(float delay)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayAllDelayed(delay);
            album.PlayAllDelayed(delay, host);
        }

        public void PlayRandomDelayed(float delay)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayRandomDelayed(delay);
            album.PlayRandomDelayed(delay, host);
        }

        public void PlayDelayed(string clipName, float delay, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayDelayed(clipName, delay, properties);
            album.PlayDelayed(clipName, delay, host, properties);
        }

        public void PlayDelayed(int index, float delay, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayDelayed(index, delay, properties);
            album.PlayDelayed(index, delay, host, properties);
        }

        public void PlayAllDelayed(float dela, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayAllDelayed(delay, properties);
            album.PlayAllDelayed(delay, host, properties);
        }

        public void PlayRandomDelayed(float delay, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayRandomDelayed(delay, properties);
            album.PlayRandomDelayed(delay, host, properties);
        }



        public void PlayScheduled(string clipName, float time)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduled(clipName, time);
            album.PlayScheduled(clipName, time, host);
        }

        public void PlayScheduled(int index, float time)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduled(index, time);
            album.PlayScheduled(index, time, host);
        }

        public void PlayAllScheduled(float time)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduledAll(time);
            album.PlayScheduledAll(time, host);
        }

        public void PlayRandomScheduled(float time)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduledRandom(time);
            album.PlayScheduledRandom(time, host);
        }

        public void PlayScheduled(string clipName, float time, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduled(clipName, time, properties);
            album.PlayScheduled(clipName, time, host, properties);
        }

        public void PlayScheduled(int index, float time, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduled(index, time, properties);
            album.PlayScheduled(index, time, host, properties);
        }

        public void PlayAllScheduled(float time, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduledAll(time, properties);
            album.PlayScheduledAll(time, host, properties);
        }

        public void PlayRandomScheduled(float time, PlayProperties properties)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            if (useDefaultHost) album.PlayScheduledRandom(time, properties);
            album.PlayScheduledRandom(time, host, properties);
        }




        public void Pause(string clipName)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Pause(clipName);
        }

        public void Pause(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Pause(index);
        }

        public void PauseAll()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.PauseAll();
        }



        public void UnPause(string clipName)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.UnPause(clipName);
        }

        public void UnPause(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.UnPause(index);
        }

        public void UnPauseAll()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.UnPauseAll();
        }




        public void Stop(string clipName)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Stop(clipName);
        }

        public void Stop(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Stop(index);
        }

        public void StopAll()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.StopAll();
        }




        public void Destroy(string clipName)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Destroy(clipName);
        }

        public void Destroy(int index)
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.Destroy(index);
        }

        public void DestroyAll()
        {
            if (album == null)
            {
                Debug.LogError(new System.NullReferenceException("AudioAlbumPlayer: Album in GameObject: " + gameObject.name + "is not defined"));
                return;
            }

            album.DestroyAll();
        }




    }
}

