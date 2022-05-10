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
    [CreateAssetMenu(fileName = "NewPlaylist", menuName = "Audio/Playlist")]
    public class AudioPlaylist : ScriptableObject
    {
        public enum TimeModeOptions
        {
            DeltaTime,
            UnscaledDeltaTime,
        }

        public enum PlayModeOptions  // Playmode of the clips list
        {
            Random,
            RandomNotRepeated,
            Sequence,
            InverseSequence,
            SequenceRandomStart,
            InverseSequenceRandomStart,
        }

        public enum LoopModeOptions
        {
            Clone,  // If desactivated can use replicas
            Loop,  // Auto call play when an audio finish, cant use replicas
            LoopCount,  // Only play an certain count of loops, cant use replicas.
        }

        // Editor props
        [SerializeField] private AudioFile[] audioFiles = new AudioFile[1];
        [SerializeField] private AudioMixerGroup output;
        [SerializeField] private bool mute = false;
        [SerializeField] private LoopModeOptions loopMode = LoopModeOptions.Clone;
        [SerializeField] private TimeModeOptions timeMode = TimeModeOptions.UnscaledDeltaTime;  // For fades and time related functions
        [SerializeField] private PlayModeOptions playMode = PlayModeOptions.Random;
        [SerializeField] private int iterations = 3;
        [SerializeField] private int replicas = 1;
        [SerializeField] private bool defaultHostToDDOL = false;
        [SerializeField] private float timeBetweenPlays = 0;

        // Public getters of private props
        private AudioFile[] AudioFiles => audioFiles;
        public AudioMixerGroup Output 
        {
            get { return output; }
            set
            {
                output = value;
                SetToAllAudioSources(a => a.outputAudioMixerGroup = value);
            }
        }
        public bool Mute 
        { 
            get { return mute; } 
            set 
            {
                mute = value;
                SetToAllAudioSources(a => a.mute = value);
            } 
        }
        public LoopModeOptions LoopMode => loopMode;
        public TimeModeOptions TimeMode
        {
            get { return timeMode; }
            set 
            { 
                timeMode = value;
                timer.TimeModeOptions = value;
            }
        }
        public PlayModeOptions PlayMode => playMode;
        public int Iterations => iterations.MinInt(1);
        public int Replicas { 
            get 
            {
                // If loop return only one source
                if (LoopMode == LoopModeOptions.Loop || LoopMode == LoopModeOptions.LoopCount) return 1;
                
                return replicas.MinInt(1);
            } 
        }
        public bool DefaultHostToDDOL => defaultHostToDDOL;
        public float TimeBetweenPlays
        {
            get { return timeBetweenPlays; }
            set { timeBetweenPlays = value; }
        }
        public float AverageVolume
        {
            get
            {
                var totalVol = 0f;
                var totalFiles = 0;

                SetToAllAudioSources(a => 
                {
                    totalVol += a.volume;
                    totalFiles++;
                });

                return totalVol / totalFiles;
            }
        }
        public float AveragePitch
        {
            get
            {
                var totalPitch = 0f;
                var totalFiles = 0;

                SetToAllAudioSources(a =>
                {
                    totalPitch += a.pitch;
                    totalFiles++;
                });

                return totalPitch / totalFiles;
            }
        }
        public float AveragePan
        {
            get
            {
                var totalPan = 0f;
                var totalFiles = 0;

                SetToAllAudioSources(a =>
                {
                    totalPan += a.panStereo;
                    totalFiles++;
                });

                return totalPan / totalFiles;
            }
        }


        [NonSerialized] private AudioEventsListener listener;
        [NonSerialized] private TimeEventsListener timer;
        [NonSerialized] private bool IsStopped = true;
        [NonSerialized] private GameObject lastHost = null;
        [NonSerialized] private Queue<AudioFile> audioFilesQueue = new Queue<AudioFile>();
        [NonSerialized] private int currentSourceCounter_ = 0;
        [NonSerialized] private bool canPlay = true;


        [NonSerialized] private int currentIterations = 0;
        private int CurrentIteration
        {
            get { return currentIterations; }
            set
            {
                currentIterations = value;
            }
        }

        [NonSerialized] private AudioSource[] audioSourcesList_ = null;  // Lenght is assignet in Get function because cant be assigned here
        private AudioSource[] AudioSourcesList
        {
            get
            {
                if (audioSourcesList_ == null)
                    audioSourcesList_ = new AudioSource[Replicas];

                return audioSourcesList_;
            }
        }


        [NonSerialized] private GameObject host_ = null;
        private GameObject Host { get 
        {
            if (host_ == null)
            {
                host_ = new GameObject("AudioHost-" + name);

                if(DefaultHostToDDOL)
                    DontDestroyOnLoad(host_);

            }
            return host_;
        } }


        [NonSerialized] private MonoBehaviour behaviour_ = null;
        private MonoBehaviour Behaviour
        {
            get
            {
                if (behaviour_ == null)
                {
                    behaviour_ = Host.AddComponent<NoActionMonoBehaviour>();
                }
                return behaviour_;
            }
        }


        public void SetToAllAudioSources(Action<AudioSource> action)
        {
            for (int i = 0; i < AudioSourcesList.Length; i++)
            {
                if (AudioSourcesList[i] == null) continue;
                try
                {
                    action(AudioSourcesList[i]);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }

        private int GetNextAudioSourceIndex()
        {
            // Check for nulls
            for (int i = 0; i < AudioSourcesList.Length; i++)
            {
                if (AudioSourcesList[i] == null) return i;
                if (!AudioSourcesList[i].isPlaying) return i;
            }


            var current = currentSourceCounter_;
            currentSourceCounter_++;
            if (currentSourceCounter_ > (Replicas - 1)) currentSourceCounter_ = 0;
            return current;
        }

        private AudioFile GetNextAudioFile()
        {
            if (AudioFiles == null)
            {
                Debug.LogError(new NullReferenceException("AudioPlaylist: Audio files cant be a null array"));
                return null;
            }

            if (AudioFiles.Length < 1)
            {
                Debug.LogError(new NullReferenceException("AudioPlaylist: Audio files array cant be a empty array"));
                return null;
            }

            if (PlayMode == PlayModeOptions.RandomNotRepeated)
            {
                // If queue is empty, Fill it
                if (audioFilesQueue.Count() < 1)
                {
                    audioFilesQueue = AudioFiles.Shuffle().ToQueue();
                }

                // Return a point
                return audioFilesQueue.Dequeue();
            }
            else if (PlayMode == PlayModeOptions.Sequence)
            {
                // If queue is empty, Fill it
                if (audioFilesQueue.Count() < 1)
                {
                    audioFilesQueue = AudioFiles.ToQueue();
                }

                // Return a point
                return audioFilesQueue.Dequeue();
            }
            else if (PlayMode == PlayModeOptions.InverseSequence)
            {
                // If queue is empty, Fill it
                if (audioFilesQueue.Count() < 1)
                {
                    audioFilesQueue = AudioFiles.Reverse().ToQueue();
                }

                // Return a point
                return audioFilesQueue.Dequeue();
            }
            else if (PlayMode == PlayModeOptions.SequenceRandomStart)
            {
                // If queue is empty, Fill it
                if (audioFilesQueue.Count() < 1)
                {
                    audioFilesQueue = AudioFiles.ToQueue().Jump(StaticFunctions.RandomInt(0, AudioFiles.Length));
                }

                // Return a point
                return audioFilesQueue.Dequeue();
            }
            else if (PlayMode == PlayModeOptions.InverseSequenceRandomStart)
            {
                // If queue is empty, Fill it
                if (audioFilesQueue.Count() < 1)
                {
                    audioFilesQueue = AudioFiles.Reverse().ToQueue().Jump(StaticFunctions.RandomInt(0, AudioFiles.Length));
                }

                // Return a point
                return audioFilesQueue.Dequeue();
            }
            else // if (playMode == PlayMode.Random)
            {
                // Return a point
                var position = StaticFunctions.RandomInt(0, AudioFiles.Length - 1);
                return AudioFiles[position];
            }
        }

        private AudioSource GetNextAudioSource(GameObject host)
        {
            // Precreate host, in case is internal host
            host.transform.position = host.transform.position;

            if (host == null)
            {
                Debug.LogError(new NullReferenceException("AudioPlaylist: Host is null"));
                return null;
            }

            // Check if can return a new AudioSource or not
            if (!canPlay) return null;


            // Save the lastHost
            lastHost = host;

            //Get the audio souce index
            var i = GetNextAudioSourceIndex();
            var file = GetNextAudioFile();

            if (file == null)
            {
                Debug.LogError(new NullReferenceException("AudioPlaylist: Audiofile is null"));
                return null;
            }

            // If is in a diferent gameObject destroy the source
            if (AudioSourcesList[i] != null)
            {
                //Debug.Log("AudioSource is already created");
                if (AudioSourcesList[i].gameObject != host)
                {
                    //Debug.Log("AudioSource is in diferente gameobject: destroying audiosource");
                    UnityEngine.Object.Destroy(AudioSourcesList[i]);
                    AudioSourcesList[i] = null;
                }
            }

            // If is a new created audiosource
            if(AudioSourcesList[i] == null)
            {
                //Debug.Log("AudioSource is null: crating a new one in: " + host);
                AudioSourcesList[i] = host.AddComponent<AudioSource>();
            }

            // Assign fixed props
            AudioSourcesList[i].bypassEffects = false; // Always false, is controled from this class
            AudioSourcesList[i].bypassListenerEffects = false; // Always false, is controled from this class
            AudioSourcesList[i].bypassReverbZones = false; // Always false, is controled from this class
            AudioSourcesList[i].playOnAwake = false;  // Neverplay on awake only when Play() is called
            AudioSourcesList[i].loop = false; // Always false, is controled from this class

            // Assign variable properties
            AudioSourcesList[i].clip = file.AudioClip;
            AudioSourcesList[i].outputAudioMixerGroup = Output;
            AudioSourcesList[i].mute = Mute;
            AudioSourcesList[i].priority = file.Priority;
            AudioSourcesList[i].volume = file.Volume;
            AudioSourcesList[i].pitch = file.Pitch;
            AudioSourcesList[i].panStereo = file.Pan;
            AudioSourcesList[i].spatialBlend = file.SpatialBlend;
            AudioSourcesList[i].reverbZoneMix = file.ReverbZone;
            
            // Check for loop
            AddLoopCallback(AudioSourcesList[i], file);

            //Debug.Log("AudioSource returned in: " + audioSources[i].gameObject.name);

            return AudioSourcesList[i];

        }

        private void AddLoopCallback(AudioSource source, AudioFile file)
        {
            // If is no playing return
            if (!Application.isPlaying)
                return;

            if (LoopMode == LoopModeOptions.Clone)
                return;

            if (listener != null)
                Destroy(listener);

            listener = source.gameObject.AddComponent<AudioEventsListener>();
            listener.source = source;
            listener.replays = file.Replays;
            listener.IfIsStopped = (el, replays) =>
            {

                // If is stopped and should be playing
                if (IsStopped)
                    return;

                // If replay enabled
                //Debug.Log($"Replay: {el.replays}");
                if (el.replays > 0)
                {
                    // Replay same file
                    el.Replayed();

                    // Return
                    return;
                }

                //Debug.Log($"No more replays");


                if (LoopMode == LoopModeOptions.Loop)
                {
                    //Debug.Log("loop mode play");
                    if (lastHost != null)
                        RePlay(lastHost);

                }

                else if (LoopMode == LoopModeOptions.LoopCount)
                {
                    CurrentIteration++;
                    //Debug.Log("I: " + iterations + " CI: " + CurrentIteration);
                    if (CurrentIteration < Iterations)
                        if (lastHost != null)
                            RePlay(lastHost);
                }

                // Destroy the event
                Destroy(el);

            };

        }

        private void AddTimeBetweenPlaysTime()
        {
            // If is no playing return
            if (!Application.isPlaying)
                return;

            if (TimeBetweenPlays <= 0)
                return;

            // If can Play because is already Playing
            if (!canPlay) return;

            if (timer == null)
                timer = Host.AddComponent<TimeEventsListener>();

            // Cant play next until time ends
            canPlay = false;

            timer.TimeModeOptions = timeMode;
            timer.SetAndPlay(TimeBetweenPlays);
            timer.TimeOver = () => 
            {
                canPlay = true;
            };

        }

        private void RePlay(GameObject host)
        {
            //Debug.Log("H: " + host.name);
            GetNextAudioSource(host)?.Play();
        }

        public void Play(GameObject host)
        {
            CurrentIteration = 0;
            IsStopped = false;
            GetNextAudioSource(host)?.Play();
            AddTimeBetweenPlaysTime();
        }

        public void Play() => Play(Host);



        public void PlayDelayed(float delay, GameObject host)
        {
            CurrentIteration = 0;
            IsStopped = false;
            GetNextAudioSource(host)?.PlayDelayed(delay);
            AddTimeBetweenPlaysTime();
        }

        public void PlayDelayed(float delay) => PlayDelayed(delay, Host);



        public void PlayScheduled(float time, GameObject host)
        {
            CurrentIteration = 0;
            IsStopped = false;
            GetNextAudioSource(host)?.PlayScheduled(time);
            AddTimeBetweenPlaysTime();
        }

        public void PlayScheduled(float time) => PlayScheduled(time, Host);



        public void Pause()
        {
            IsStopped = true;
            SetToAllAudioSources(a => a.Pause());
        }

        public void UnPause()
        {
            IsStopped = false;
            SetToAllAudioSources(a => a.UnPause());
        }

        public void Stop()
        {
            IsStopped = true;
            CurrentIteration = 0;
            canPlay = true;
            SetToAllAudioSources(a => a.Stop());
        }


        public void DestroyAllSources()
        {
            IsStopped = true;
            CurrentIteration = 0;
            Destroy(listener);
            Destroy(timer);
            for (int i = 0; i < AudioSourcesList.Length; i++)
            {
                if (AudioSourcesList[i] == null) continue;

                Destroy(AudioSourcesList[i]);

                AudioSourcesList[i] = null;

            }
            if(host_ != null)
                Destroy(host_);
        }





        #region Fade

        private IEnumerator DoFadeVolume(float startVolume, float targetVolume, float duration, Action OnComplete)
        {
            //Debug.Log("Fading volume of: " + name);

            float currentTime = 0;
            //float startVol = volume;
            targetVolume = targetVolume.MinMaxFloat(0f, 1f);
            duration = duration.MinFloat(0f);

            //Debug.Log("curr: " + currentTime + " < durat: " + duration);
            while (currentTime <= duration)
            {
                if (TimeMode == TimeModeOptions.DeltaTime)
                    currentTime += Time.deltaTime;
                else
                    currentTime += Time.unscaledDeltaTime;

                if (duration == 0) SetToAllAudioSources(a => a.volume = targetVolume);
                else SetToAllAudioSources(a => a.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration).MinMaxFloat(0f,1f));
                //Debug.Log("Fading: " + volume);
                yield return null;
            }
            //Debug.Log("End of fade");
            OnComplete();
            yield break;
        }

        public void FadeVolume(float startVolume, float targetVolume, float duration)
        {
            Behaviour.StartCoroutine(DoFadeVolume(startVolume, targetVolume, duration, () => { }));
        }

        public void FadeVolumeAndStop(float startVolume, float targetVolume, float duration)
        {
            Behaviour.StartCoroutine(DoFadeVolume(startVolume, targetVolume, duration, () => { Stop(); }));
        }

        public void FadeVolumeAndDestroyAllSources(float startVolume, float targetVolume, float duration)
        {
            Behaviour.StartCoroutine(DoFadeVolume(startVolume, targetVolume, duration, () => { DestroyAllSources(); }));
        }



        private IEnumerator DoFadePitch(float startPitch, float targetPitch, float duration, Action OnComplete)
        {
            //Debug.Log("Fading pitch of: " + name);
            //Debug.Log("Fading pitch of: " + startPitch + " " + targetPitch);

            float currentTime = 0;
            //float startVol = pitch;
            targetPitch = targetPitch.MinMaxFloat(-3f, 3f);
            duration = duration.MinFloat(0f);

            //Debug.Log("curr: " + currentTime + " < durat: " + duration);
            while (currentTime <= duration)
            {
                if (TimeMode == TimeModeOptions.DeltaTime)
                    currentTime += Time.deltaTime;
                else
                    currentTime += Time.unscaledDeltaTime;

                if(duration == 0) SetToAllAudioSources(a => a.pitch = targetPitch);
                else SetToAllAudioSources(a => a.pitch = Mathf.Lerp(startPitch, targetPitch, currentTime / duration).MinMaxFloat(-3f,3f));
                //Debug.Log("Fading: " + pitch);
                yield return null;
            }
            //Debug.Log("End of fade");
            OnComplete();
            yield break;
        }

        public void FadePitch(float startPitch, float targetPitch, float duration)
        {
            Behaviour.StartCoroutine(DoFadePitch(startPitch, targetPitch, duration, () => { }));
        }

        public void FadePitchAndStop(float startPitch, float targetPitch, float duration)
        {
            Behaviour.StartCoroutine(DoFadePitch(startPitch, targetPitch, duration, () => { Stop(); }));
        }

        public void FadePitchAndDestroyAllSources(float startPitch, float targetPitch, float duration)
        {
            Behaviour.StartCoroutine(DoFadePitch(startPitch, targetPitch, duration, () => { DestroyAllSources(); }));
        }





        private IEnumerator DoFadePan(float startPan, float targetPan, float duration, Action OnComplete)
        {
            //Debug.Log("Fading pan of: " + name);

            float currentTime = 0;
            //float startVol = pan;
            targetPan = targetPan.MinMaxFloat(0f, 1f);
            duration = duration.MinFloat(0f);

            //Debug.Log("curr: " + currentTime + " < durat: " + duration);
            while (currentTime <= duration)
            {
                if (TimeMode == TimeModeOptions.DeltaTime)
                    currentTime += Time.deltaTime;
                else
                    currentTime += Time.unscaledDeltaTime;

                if (duration == 0) SetToAllAudioSources(a => a.panStereo = targetPan);
                else SetToAllAudioSources(a => a.panStereo = Mathf.Lerp(startPan, targetPan, currentTime / duration).MinMaxFloat(0f, 1f));
                //Debug.Log("Fading: " + pan);
                yield return null;
            }
            //Debug.Log("End of fade");
            OnComplete();
            yield break;
        }

        public void FadePan(float startPan, float targetPan, float duration)
        {
            Behaviour.StartCoroutine(DoFadePan(startPan, targetPan, duration, () => { }));
        }

        public void FadePanAndStop(float startPan, float targetPan, float duration)
        {
            Behaviour.StartCoroutine(DoFadePan(startPan, targetPan, duration, () => { Stop(); }));
        }

        public void FadePanAndDestroyAllSources(float startPan, float targetPan, float duration)
        {
            Behaviour.StartCoroutine(DoFadePan(startPan, targetPan, duration, () => { DestroyAllSources(); }));
        }

        #endregion Fade


        private class NoActionMonoBehaviour : MonoBehaviour { }

    }
}
