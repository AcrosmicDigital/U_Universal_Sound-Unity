using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace U.Universal.Sound
{
    [CreateAssetMenu(fileName = "NewAudioAlbum", menuName = "U Audio/Album")]
    public class AudioAlbum : ScriptableObject
    {

        public AudioPlaylist[] playlists;

        
        public void Create(string playlistName)
        {
            if(playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if(playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Create();
        }

        public void Create(string playlistName, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Create(host);
        }

        public void Create(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if(playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Create();
        }

        public void Create(int index, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Create(host);
        }

        public void CreateAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var audioFile in playlists)
            {
                audioFile?.Create();
            }
        }

        public void CreateAll(GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var audioFile in playlists)
            {
                audioFile?.Create(host);
            }
        }



        public void Play(string playlistName)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Play();
        }

        public void Play(string playlistName, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Play(properties);
        }

        public void Play(string playlistName, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Play(host);
        }

        public void Play(string playlistName, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Play(host, properties);
        }

        public void Play(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Play();
        }

        public void Play(int index, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Play(properties);
        }

        public void Play(int index, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Play(host);
        }

        public void Play(int index, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Play(host, properties);
        }

        public void PlayAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Play();
            }
        }

        public void PlayAll(PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Play(properties);
            }
        }

        public void PlayAll(GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Play(host);
            }
        }

        public void PlayAll(GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Play(host, properties);
            }
        }

        public void PlayRandom()
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);
            
            Play(index);
        }

        public void PlayRandom(PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            Play(index, properties);
        }

        public void PlayRandom(GameObject host)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            Play(index, host);
        }

        public void PlayRandom(GameObject host, PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            Play(index, host, properties);
        }



        public void PlayDelayed(string playlistName, float delay)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayDelayed(delay);
        }

        public void PlayDelayed(string playlistName, float delay, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayDelayed(delay, properties);
        }

        public void PlayDelayed(string playlistName, float delay, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayDelayed(delay, host);
        }

        public void PlayDelayed(string playlistName, float delay, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayDelayed(delay, host, properties);
        }

        public void PlayDelayed(int index, float delay)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayDelayed(delay);
        }

        public void PlayDelayed(int index, float delay, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayDelayed(delay, properties);
        }

        public void PlayDelayed(int index, float delay, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayDelayed(delay, host);
        }

        public void PlayDelayed(int index, float delay, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayDelayed(delay, host, properties);
        }

        public void PlayAllDelayed(float delay)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayDelayed(delay);
            }
        }

        public void PlayAllDelayed(float delay, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayDelayed(delay, properties);
            }
        }

        public void PlayAllDelayed(float delay, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayDelayed(delay, host);
            }
        }

        public void PlayAllDelayed(float delay, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayDelayed(delay, host, properties);
            }
        }

        public void PlayRandomDelayed(float delay)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayDelayed(index, delay);
        }

        public void PlayRandomDelayed(float delay, PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayDelayed(index, delay, properties);
        }

        public void PlayRandomDelayed(float delay, GameObject host)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayDelayed(index, delay, host);
        }

        public void PlayRandomDelayed(float delay, GameObject host, PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayDelayed(index, delay, host, properties);
        }



        public void PlayScheduled(string playlistName, float time)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayScheduled(time);
        }

        public void PlayScheduled(string playlistName, float time, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayScheduled(time, host);
        }

        public void PlayScheduled(int index, float time)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayScheduled(time);
        }

        public void PlayScheduled(int index, float time, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayScheduled(time, host);
        }

        public void PlayScheduledAll(float time)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayScheduled(time);
            }
        }

        public void PlayScheduledAll(float time, GameObject host)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayScheduled(time, host);
            }
        }

        public void PlayScheduledRandom(float time)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayScheduled(index, time);
        }

        public void PlayScheduledRandom(float time, GameObject host)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayScheduled(index, time, host);
        }

        public void PlayScheduled(string playlistName, float time, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayScheduled(time, properties);
        }

        public void PlayScheduled(string playlistName, float time, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.PlayScheduled(time, host, properties);
        }

        public void PlayScheduled(int index, float time, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayScheduled(time, properties);
        }

        public void PlayScheduled(int index, float time, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].PlayScheduled(time, host, properties);
        }

        public void PlayScheduledAll(float time, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayScheduled(time, properties);
            }
        }

        public void PlayScheduledAll(float time, GameObject host, PlayProperties properties)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.PlayScheduled(time, host, properties);
            }
        }

        public void PlayScheduledRandom(float time, PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayScheduled(index, time, properties);
        }

        public void PlayScheduledRandom(float time, GameObject host, PlayProperties properties)
        {
            var index = StaticFunctions.RandomInt(0, playlists.Length - 1);

            PlayScheduled(index, time, host, properties);
        }



        public void Pause(string playlistName)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Pause();
        }

        public void Pause(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Pause();
        }

        public void PauseAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Pause();
            }
        }


        public void UnPause(string playlistName)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.UnPause();
        }

        public void UnPause(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].UnPause();
        }

        public void UnPauseAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.UnPause();
            }
        }


        public void Stop(string playlistName)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Stop();
        }

        public void Stop(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Stop();
        }

        public void StopAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Stop();
            }
        }


        public void Destroy(string playlistName)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null)
            {
                Debug.LogError("AudioAlbum: Cant find playlist with name: " + playlistName);
                return;
            }

            playlist.Destroy();
        }

        public void Destroy(int index)
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            if (playlists.Length < index || index < 0)
            {
                Debug.LogError("AudioAlbum: Cant find playlist at index: " + index);
                return;
            }

            if (playlists[index] == null)
            {
                Debug.LogError("AudioAlbum: Null playlist at index: " + index);
                return;
            }

            playlists[index].Destroy();
        }

        public void DestroyAll()
        {
            if (playlists == null)
            {
                Debug.LogError("AudioAlbum: No playlist set");
                return;
            }

            foreach (var playlist in playlists)
            {
                playlist?.Destroy();
            }
        }



        public bool Exist(string playlistName)
        {
            if (playlists == null) return false;

            var playlist = playlists.Where(a => a.name == playlistName).FirstOrDefault();

            if (playlist == null) return false;

            return true;
        }

        public bool Exist(int index)
        {
            if (playlists == null) return false;

            if (playlists.Length < index || index < 0) return false;

            if (playlists[index] == null) return false;

            return true;
        }



        public string[] GetPlaylistsNames()
        {
            return GetPlaylists().Select(p => p.name).ToArray();
        }

        public AudioPlaylist[] GetPlaylists()
        {
            try
            {
                return (AudioPlaylist[])playlists.Clone();
            }
            catch (System.Exception)
            {
                return new AudioPlaylist[0];
            }
        }

        public AudioPlaylist GetPlaylist(string playlistName)
        {
            if (playlists == null) return null;

            return playlists.Where(a => a.name == playlistName).FirstOrDefault();

        }

        public AudioPlaylist GetPlaylist(int index)
        {
            if (playlists == null) return null;

            if (playlists.Length < index || index < 0) return null;

            return playlists[index];

        }



        public int Count { 
            get 
            {
                try
                {
                    return playlists.Length;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            } }



        #region FIND

        public static AudioAlbum LoadFromResources(string path)
        {
            return Resources.Load<AudioAlbum>(path);
        }

        public static AudioAlbum LoadFromResourcesAudioAlbumsFolder(string path)
        {
            return Resources.Load<AudioAlbum>("Audio/Albums/" + path);
        }

        #endregion

    }
}