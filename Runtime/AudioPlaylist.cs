using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace U.Universal.Sound
{
    [CreateAssetMenu(fileName = "NewAudioPlaylist", menuName = "U Audio/Playlist")]
    public class AudioPlaylist : ScriptableObject
    {
        public enum TimeMode
        {
            DeltaTime,
            UnscaledDeltaTime,
        }

        public enum PlayMode
        {
            Random,
            RandomNotRepeated,
            Sequence,
            InverseSequence,
            SequenceRandomStart,
            InverseSequenceRandomStart,
        }

        public enum LoopMode
        {
            Clone,  // If desactivated can use replicas
            Loop,  // Auto call play when an audio finish, cant use replicas
            Count,  // Only play an certain count of loops, cant use replicas.
        }

        private class NoActionMonoBehaviour : MonoBehaviour { }


        public AudioFile[] audioFiles;
        public TimeMode timeMode = TimeMode.UnscaledDeltaTime;  // For fades and time related functions
        public PlayMode playMode = PlayMode.Random;
        public LoopMode loopMode;
        public int iterationsVal;
        public int replicasVal = 1;
        public bool defaultHostToDDOL = false;
        public bool allowOverlap = true;
        public AudioMixerGroup audioMixerGroup;



        [NonSerialized] private AudioEventsListener listener;
        [NonSerialized] private bool isStopped = true;
        [NonSerialized] private GameObject lastHost = null;
        [NonSerialized] private int currentIterations = 0;
        private int iterations => iterationsVal.MinInt(1);
        private int replicas { 
            get 
            {
                // If loop return only one source
                if (loopMode != LoopMode.Clone)
                    return 1;

                return replicasVal.MinInt(1);
            } 
        }
        [NonSerialized] private AudioSource[] audioSourcesInstance = null;
        private AudioSource[] audioSources { get 
            {
                if (audioSourcesInstance == null)
                    audioSourcesInstance = new AudioSource[replicas];

                return audioSourcesInstance;
            } }
        [NonSerialized] private GameObject hostInstance = null;
        private GameObject _host { get 
            {
                if (hostInstance == null)
                {
                    hostInstance = new GameObject("AudioHost-" + name);

                    if(defaultHostToDDOL)
                        UnityEngine.Object.DontDestroyOnLoad(hostInstance);

                }
                return hostInstance;
            } }
        [NonSerialized] private int currentSourceInstance = 0;
        private int? currentSource { 
            get 
            {
                if (!allowOverlap)
                {
                    for (int i = 0; i < audioSources.Length; i++)
                    {
                        if (audioSources[i] == null) return i;

                        if (!audioSources[i].isPlaying) return i;
                    }

                    return null;
                }

                var current = currentSourceInstance;
                currentSourceInstance++;
                if (currentSourceInstance > (replicas - 1)) currentSourceInstance = 0;
                return current;

            } 
        }
        [NonSerialized] private Queue<AudioFile> audioFilesQueue = new Queue<AudioFile>();
        private AudioFile currentAudioFile { get 
            {
                if (audioFiles == null)
                {
                    Debug.LogError(new NullReferenceException("AudioPlaylist: Audio files cant be a null array"));
                    return null;
                }

                if (audioFiles.Length < 1)
                {
                    Debug.LogError(new NullReferenceException("AudioPlaylist: Audio files array cant be a empty array"));
                    return null;
                }

                if (playMode == PlayMode.RandomNotRepeated)
                {
                    // If queue is empty, Fill it
                    if (audioFilesQueue.Count() < 1)
                    {
                        audioFilesQueue = audioFiles.Shuffle().ToQueue();
                    }

                    // Return a point
                    return audioFilesQueue.Dequeue();
                }
                else if (playMode == PlayMode.Sequence)
                {
                    // If queue is empty, Fill it
                    if (audioFilesQueue.Count() < 1)
                    {
                        audioFilesQueue = audioFiles.ToQueue();
                    }

                    // Return a point
                    return audioFilesQueue.Dequeue();
                }
                else if (playMode == PlayMode.InverseSequence)
                {
                    // If queue is empty, Fill it
                    if (audioFilesQueue.Count() < 1)
                    {
                        audioFilesQueue = audioFiles.Reverse().ToQueue();
                    }

                    // Return a point
                    return audioFilesQueue.Dequeue();
                }
                else if (playMode == PlayMode.SequenceRandomStart)
                {
                    // If queue is empty, Fill it
                    if (audioFilesQueue.Count() < 1)
                    {
                        audioFilesQueue = audioFiles.ToQueue().Jump(StaticFunctions.RandomInt(0, audioFiles.Length));
                    }

                    // Return a point
                    return audioFilesQueue.Dequeue();
                }
                else if (playMode == PlayMode.InverseSequenceRandomStart)
                {
                    // If queue is empty, Fill it
                    if (audioFilesQueue.Count() < 1)
                    {
                        audioFilesQueue = audioFiles.Reverse().ToQueue().Jump(StaticFunctions.RandomInt(0, audioFiles.Length));
                    }

                    // Return a point
                    return audioFilesQueue.Dequeue();
                }
                else // if (playMode == PlayMode.Random)
                {
                    // Return a point
                    var position = StaticFunctions.RandomInt(0, audioFiles.Length - 1);
                    return audioFiles[position];
                }

            } 
        }
        [NonSerialized] private MonoBehaviour monoHostInstance = null;
        private MonoBehaviour _monoHost
        {
            get
            {
                if (monoHostInstance == null)
                {
                    monoHostInstance = new GameObject("AudioMonoHost-" + name).AddComponent<NoActionMonoBehaviour>();

                    UnityEngine.Object.DontDestroyOnLoad(monoHostInstance.gameObject);

                }
                return monoHostInstance;
            }
        }


        private AudioSource GetSource(GameObject host)
        {
            if (host == null)
                host = _host;

            // Save the lastHost
            lastHost = host;

            //Debug.Log("Getting source in: " + host.name);
            var ix = currentSource;

            // If no available source
            if (ix == null) return null;

            // If available source
            var i = (int)currentSource;

            var file = currentAudioFile;
            if (file == null)
            {
                Debug.LogError(new NullReferenceException("AudioPlaylist: Audiofile is null"));
                return null;
            }

            // If is diferent gameObject destroy the source
            if (audioSources[i] != null)
            {
                //Debug.Log("AudioSource is already created");
                if (audioSources[i].gameObject != host)
                {
                    //Debug.Log("AudioSource is in diferente gameobject: destroying audiosource");
                    UnityEngine.Object.Destroy(audioSources[i]);
                    audioSources[i] = null;
                }
            }

            // If is a new created audiosource
            if(audioSources[i] == null)
            {
                //Debug.Log("AudioSource is null: crating a new one in: " + host);
                audioSources[i] = host.AddComponent<AudioSource>();

                // Assign one time properties
                audioSources[i].loop = false; // Always false, is controled from this class
                audioSources[i].playOnAwake = false;  // Neverplay on awake only when Play() is called
                audioSources[i].outputAudioMixerGroup = audioMixerGroup;
            }

            
            // Assign multi time properties
            audioSources[i].clip = file.audioClip;
            audioSources[i].volume = file.volume;
            audioSources[i].pitch = file.pitch;
            
            // Check for loop
            CheckForLoop(audioSources[i]);

            //Debug.Log("AudioSource returned in: " + audioSources[i].gameObject.name);

            return audioSources[i];

        }

        private void CheckForLoop(AudioSource source)
        {
            // If is no playing return
            if (!Application.isPlaying)
                return;

            if (loopMode == LoopMode.Clone)
                return;

            //Debug.Log("Is loopmode addiing behaviour");
            if (listener != null)
                Destroy(listener);

            listener = source.gameObject.AddComponent<AudioEventsListener>();
            listener.source = source;
            listener.IfIsStopped = (el) =>
            {
                //Debug.Log("IsStopped: " + isStopped);

                // If is stopped and should be playing
                if (isStopped)
                    return;

                // Destroy the event
                Destroy(el);

                if (loopMode == LoopMode.Loop)
                {
                    //Debug.Log("loop mode play");
                    if (lastHost != null)
                        PlayInside(lastHost);

                }

                else if (loopMode == LoopMode.Count)
                {
                    currentIterations++;
                    if (currentIterations < iterations)
                        if (lastHost != null)
                            PlayInside(lastHost);
                }

            };

        }

        private void PlayInside(GameObject host)
        {
            GetSource(host)?.Play();
        }



        public void Create(GameObject host)
        {
            if (host == null) return;

            for (int i = 0; i < replicas; i++)
            {
                GetSource(host);
            }
        }

        public void Create()
        {
            Create(_host);
        }



        public void Play(GameObject host, PlayProperties properties)
        {
            if (host == null) return;

            var s = GetSource(host);

            if (s == null) return;

            // If overide properties
            if(properties != null)
            {
                s.volume = properties.volume;
                s.pitch = properties.pitch;
            }

            currentIterations = 0;
            isStopped = false;
            s.Play();
        }

        public void Play(GameObject host)
        {
            Play(host, null);
        }

        public void Play(PlayProperties properties)
        {
            Play(_host, properties);
        }

        public void Play()
        {
            Play(_host);
        }



        public void PlayDelayed(float delay, GameObject host, PlayProperties properties)
        {
            if (host == null) return;

            var s = GetSource(host);

            if (s == null) return;

            // If overide properties
            if (properties != null)
            {
                s.volume = properties.volume;
                s.pitch = properties.pitch;
            }

            currentIterations = 0;
            isStopped = false;
            s.PlayDelayed(delay.MinFloat(0f));
        }

        public void PlayDelayed(float delay, GameObject host)
        {
            PlayDelayed(delay, host, null);
        }

        public void PlayDelayed(float delay, PlayProperties properties)
        {
            PlayDelayed(delay, _host, properties);
        }

        public void PlayDelayed(float delay)
        {
            PlayDelayed(delay, _host);
        }



        public void PlayScheduled(float time, GameObject host, PlayProperties properties)
        {
            if (host == null) return;

            var s = GetSource(host);

            if (s == null) return;

            // If overide properties
            if (properties != null)
            {
                s.volume = properties.volume;
                s.pitch = properties.pitch;
            }

            currentIterations = 0;
            isStopped = false;
            s.PlayScheduled(time.MinFloat(time));
        }

        public void PlayScheduled(float time, GameObject host)
        {
            PlayScheduled(time, host, null);
        }

        public void PlayScheduled(float time, PlayProperties properties)
        {
            PlayScheduled(time, _host, properties);
        }

        public void PlayScheduled(float time)
        {
            PlayScheduled(time, _host);
        }



        public void Pause()
        {
            isStopped = true;
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (audioSources[i] == null) continue;
                audioSources[i].Pause();
            }
        }

        public void UnPause()
        {
            isStopped = false;
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (audioSources[i] == null) continue;
                audioSources[i].UnPause();
            }
        }

        public void Stop()
        {
            isStopped = true;
            currentIterations = 0;
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (audioSources[i] == null) continue;
                audioSources[i].Stop();

            }
        }



        public void Destroy()
        {
            isStopped = true;
            currentIterations = 0;
            Destroy(listener);
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (audioSources[i] == null) continue;

                UnityEngine.Object.Destroy(audioSources[i]);

                audioSources[i] = null;

            }
            if(hostInstance != null)
                Destroy(hostInstance);
        }


        #region Fade

        public void RestoreVol()
        {
            //foreach (var source in audioSources)
            //{
            //    if (source == null) continue;

            //    source.
            //}
            foreach (var file in audioFiles)
            {
                if (file == null) continue;

                file.RestoreVol();
            }
        }

        public void RestorePitch()
        {
            //foreach (var source in audioSources)
            //{
            //    if (source == null) continue;

            //    source.pitch = value.MinMaxFloat(-3f, 3f);
            //}
            foreach (var file in audioFiles)
            {
                if (file == null) continue;

                file.RestorePitch();
            }
        }

        private IEnumerator DoFadeVol(float targetVolume, float duration)
        {
            //Debug.Log("Fading volume of: " + name);

            float currentTime = 0;
            float startVol = volume;
            targetVolume = targetVolume.MinMaxFloat(0f, 1f);
            duration = duration.MinFloat(0f);

            //Debug.Log("curr: " + currentTime + " < durat: " + duration);
            while (currentTime < duration)
            {
                if(timeMode == TimeMode.DeltaTime)
                    currentTime += Time.deltaTime;
                else
                    currentTime += Time.unscaledDeltaTime;

                volume = Mathf.Lerp(startVol, targetVolume, currentTime / duration);
                //Debug.Log("Fading: " + volume);
                yield return null;
            }
            //Debug.Log("End of fade");
            yield break;
        }

        public void FadeVolume(float targetVolume, float duration)
        {
            
            _monoHost.StartCoroutine(DoFadeVol(targetVolume, duration));

        }

        public void FadeOutVolume(float duration)
        {
            IEnumerator FadeOut(float duration)
            {
                yield return DoFadeVol(0, duration);
                Stop();
            }

            _monoHost.StartCoroutine(FadeOut(duration));
        }



        private IEnumerator DoFadePitch(float targetPitch, float duration)
        {
            //Debug.Log("Fading pitch of: " + name);

            float currentTime = 0;
            float startPitch = pitch;
            targetPitch = targetPitch.MinMaxFloat(-3f, 3f);
            duration = duration.MinFloat(0f);

            //Debug.Log("curr: " + currentTime + " < durat: " + duration);
            while (currentTime < duration)
            {
                if (timeMode == TimeMode.DeltaTime)
                    currentTime += Time.deltaTime;
                else
                    currentTime += Time.unscaledDeltaTime;

                pitch = Mathf.Lerp(startPitch, targetPitch, currentTime / duration);
                //Debug.Log("Fading: " + pitch);
                yield return null;
            }
            //Debug.Log("End of fade");
            yield break;
        }

        public void FadePitch(float targetPitch, float duration)
        {

            _monoHost.StartCoroutine(DoFadePitch(targetPitch, duration));

        }

        #endregion


        #region Other publics

        public bool isplaying { 
            get 
            {
                var playing = false;

                foreach(var source in audioSources)
                {
                    if (source == null) continue;

                    playing = playing || source.isPlaying;
                }

                return playing;
            } 
        }

        public float volume  // Get the average volume of al soources, or set the volume to all sources
        {
            get
            {
                var totalVolume = 0f;
                var totalCounts = 0;

                foreach (var source in audioSources)
                {
                    if (source == null) continue;

                    totalCounts++;
                    totalVolume += source.volume;
                }

                return totalVolume / totalCounts;
            }
            set
            {
                foreach (var source in audioSources)
                {
                    if (source == null) continue;

                    source.volume = value.MinMaxFloat(0f, 1f);
                }
                foreach (var file in audioFiles)
                {
                    if (file == null) continue;

                    file.volume = value.MinMaxFloat(00f, 1f);
                }
            }
        }

        public float pitch  // Get the average pitch of al soources, or set the pitch to all sources
        {
            get
            {
                var totalPitch = 0f;
                var totalCounts = 0;

                foreach (var source in audioSources)
                {
                    if (source == null) continue;

                    totalCounts++;
                    totalPitch += source.pitch;
                }

                return totalPitch / totalCounts;
            }
            set
            {
                foreach (var source in audioSources)
                {
                    if (source == null) continue;

                    source.pitch = value.MinMaxFloat(-3f, 3f);
                }
                foreach (var file in audioFiles)
                {
                    if (file == null) continue;

                    file.pitch = value.MinMaxFloat(00f, 1f);
                }
            }
        }

        #endregion



        #region FIND

        public static AudioPlaylist LoadFromResources(string path)
        {
            return Resources.Load<AudioPlaylist>(path);
        }

        public static AudioPlaylist LoadFromResourcesAudioPlaylist(string path)
        {
            return Resources.Load<AudioPlaylist>("Audio/Playlists/" + path);
        }

#endregion

    }

}
