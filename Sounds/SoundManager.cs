using Microsoft.Xna.Framework.Media;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Sounds
{
    /// <summary>
    /// Class to manage all game sounds
    /// </summary>
    /// 
    /// <remarks>
    /// In Monogame the SoundEffect class represents a single short sound effect. The Song class represents
    /// a longer piece of music. A limitation of the Monogame Framework is that only one Song can be played at a time.
    /// </remarks>
    public class SoundManager : Singleton<SoundManager>, IDisposable
    {
        #region Properties

        private readonly Dictionary<string, Song> _songs;
        private readonly Dictionary<string, SoundEffect> _SFX;

        private readonly List<SoundEffectInstance> _activeSFX;

        private float _prevSongVolume;
        private float _prevSFXVolume;

        public bool IsMuted { get; private set; }
        public bool IsDisposed { get; private set; }

        public float SongVolume
        {
            get
            {
                if (IsMuted) { return 0.0f; }
                return MediaPlayer.Volume;
            }
            set
            {
                if (IsMuted) { return; }
                MediaPlayer.Volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }


        public float SFXVolume
        {
            get
            {
                if (IsMuted) { return 0.0f; }
                return SoundEffect.MasterVolume;
            }
            set
            {
                if (IsMuted) { return; }
                SoundEffect.MasterVolume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        #endregion Properties





        #region Init

        /// <summary>
        /// Constructor
        /// </summary>
        public SoundManager()
        {
            _SFX = new Dictionary<string, SoundEffect>();
            _songs = new Dictionary<string, Song>();
            _activeSFX = new List<SoundEffectInstance>();
        }
        

        /// <summary>
        /// Finaliser called when this object is collect by garbage collector
        /// </summary>
        ~SoundManager() => Dispose(false);


        /// <summary>
        /// Register a sound effect to this soundmanager
        /// </summary>
        /// <param name="name">Name to store SFX under</param>
        /// <param name="sfx">Sound effect to register</param>
        public void RegisterSFX(string name, SoundEffect sfx)
        {
            _SFX.Add(name, sfx);
        }


        /// <summary>
        /// Register a song to this sound manager
        /// </summary>
        /// <param name="name">Name to store song under</param>
        /// <param name="song">Song to register</param>
        public void RegisterSong(string name, Song song)
        {
            _songs.Add(name, song);
        }

        #endregion Init

        



        #region Update

        /// <summary>
        /// Update list of current SFX to remove any that have finished playing
        /// </summary>
        public void Update()
        {
            for (int i = _activeSFX.Count - 1; i >= 0; i--)
            {
                SoundEffectInstance sfx = _activeSFX[i];

                if (sfx.State == SoundState.Stopped)
                {
                    if (!sfx.IsDisposed)
                    {
                        sfx.Dispose();
                    }

                    _activeSFX.RemoveAt(i);
                }
            }
        }

        #endregion Update







        #region Util

        /// <summary>
        /// Plays a sound effect with specified name
        /// </summary>
        /// <param name="name">The name of the sound effect to play</param>
        /// <returns>The sound effect instance created by playing this sound effect</returns>
        public SoundEffectInstance PlaySFX(string name)
        {
            return PlaySFX(name, 1.0f, 0.0f, 0.0f, false);
        }


        /// <summary>
        /// Play sound effect with specified name
        /// </summary>
        /// <param name="name">The name of the sound effect to play</param>
        /// <param name="volume">The volume to play the sound effect. Silent is 0.0f. Full volume is 1.0f</param>
        /// <param name="pitch">The pitch to play the sound effect. Down a full octave is -1.0f. Up a full octave is 1.0f</param>
        /// <param name="pan">The amount to pan the sound effect. Left is -1.0f. Right is 1.0f</param>
        /// <param name="isLooped">Is the sound effect playing on loop?</param>
        /// <returns>The sound effect instance created by playing this sound effect</returns>
        /// <exception cref="ArgumentException">Throw exception if SFX name does not exist in SFX dictionary</exception>
        public SoundEffectInstance PlaySFX(string name, float volume, float pitch, float pan, bool isLooped)
        {
            if (!_SFX.ContainsKey(name))
            {
                throw new ArgumentException($"SoundEffect with name {name} does not exist");
            }

            SoundEffectInstance sfx = _SFX[name].CreateInstance();

            sfx.Volume = volume;
            sfx.Pitch = pitch;
            sfx.Pan = pan;
            sfx.IsLooped = isLooped;

            sfx.Play();
            _activeSFX.Add(sfx);

            return sfx;
        }


        /// <summary>
        /// Plays the song with specified name
        /// </summary>
        /// <param name="name">The name of the song to play</param>
        public void PlaySong(string name)
        {
            PlaySong(name, 1.0f, false);
        }


        /// <summary>
        /// Plays the song with specified name
        /// </summary>
        /// <param name="name">The name of the song to play</param>
        /// <param name="volume">The volume to play the song. Silent is 0.0f. Full volume is 1.0f</param>
        /// <param name="isLooped">Is the song on repeat?</param>
        /// <exception cref="ArgumentException">Throw exception if song name does not exist in songs dictionary</exception>
        public void PlaySong(string name, float volume, bool isLooped)
        {
            if (!_songs.ContainsKey(name))
            {
                throw new ArgumentException($"Song with name {name} does not exist");
            }

            // If a song is already playing stop it
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Volume = volume;
            MediaPlayer.IsRepeating = isLooped;

            MediaPlayer.Play(_songs[name]);
        }


        /// <summary>
        /// Pause all audio
        /// </summary>
        public void Pause()
        {
            MediaPlayer.Pause();

            foreach (SoundEffectInstance sfx in _activeSFX)
            {
                sfx.Pause();
            }
        }


        // Resume all paused audio
        public void Resume()
        {
            MediaPlayer.Resume();

            foreach (SoundEffectInstance sfx in _activeSFX)
            {
                sfx.Resume();
            }
        }


        /// <summary>
        /// Mute all audio
        /// </summary>
        public void Mute()
        {
            // Store current volume
            _prevSongVolume = MediaPlayer.Volume;
            _prevSFXVolume = SoundEffect.MasterVolume;

            MediaPlayer.Volume = 0.0f;
            SoundEffect.MasterVolume = 0.0f;
            IsMuted = true;
        }


        /// <summary>
        /// Unmute all audio
        /// </summary>
        public void UnMute()
        {
            MediaPlayer.Volume = _prevSongVolume;
            SoundEffect.MasterVolume = _prevSFXVolume;
            IsMuted = false;
        }


        /// <summary>
        /// Toggle between muted and unmuted
        /// </summary>
        public void ToggleMute()
        {
            if (IsMuted)
            {
                UnMute();
            }
            else
            {
                Mute();
            }
        }

        #endregion Util







        #region Dispose

        /// <summary>
        /// Disposes of soundmanager and cleans up resources
        /// </summary>
        /// Calling dispose releases any resources the user's system is utilising to play audio, e.g. audio channels.
        /// This prevents resource leaks after the game application is closed.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes of soundmanager and cleans up resources 
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed</param>
        /// <remarks>
        /// Calling dispose releases any resources the user's system is utilising to play audio, e.g. audio channels.
        /// This prevents resource leaks after the game application is closed.
        /// </remarks>
        public void Dispose(bool disposing)
        {
            if (IsDisposed) { return; }

            if (disposing)
            {
                foreach(SoundEffectInstance sfx in _activeSFX)
                {
                    sfx.Dispose();
                }

                _activeSFX.Clear();
            }

            IsDisposed = true;
        }

        #endregion Dispose
    }
}
