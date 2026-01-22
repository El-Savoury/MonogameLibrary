namespace MonogameLibrary.Graphics
{
    public class AnimationController
    {
        #region Members

        private readonly Animation animation;
        private int playDirection;
        private int remainingFrames;
        private TimeSpan elapsedTime = TimeSpan.Zero;


        public float PlaySpeed { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPaused { get; set; }
        public bool IsPingPong { get; set; }
        public bool IsLooping { get; set; }
        public bool IsReversed
        {
            get { return playDirection == -1; }

            set { playDirection = value ? -1 : 1; }
        }
        public int CurrentFrameIndex { get; private set; }
        public AnimationFrame CurrentFrame => animation.Frames[CurrentFrameIndex];
        public int TotalNumOfFrames => animation.Frames.Count - 1;

        #endregion Members




        #region Init

        /// <summary>
        /// Create a new animation controller
        /// </summary>
        /// <param name="anim">Animation to control</param>
        public AnimationController(Animation anim)
        {
            animation = anim;
            IsReversed = animation.IsReversed;
            IsPingPong = animation.IsPingPong;
            IsLooping = animation.IsLooping;

            PlaySpeed = 1.0f;
            playDirection = IsReversed ? -1 : 1;

            // A ping pong animation has double the number of frames to display than a standard animation
            remainingFrames = IsPingPong ? TotalNumOfFrames * 2 : TotalNumOfFrames;

            // Start playing by default
            Play();
        }

        #endregion Init





        #region Update

        /// <summary>
        /// Update the animation
        /// </summary>
        /// <param name="gameTime">Game time state info</param>
        public void Update(GameTime gameTime)
        {
            if (!IsPlaying || IsPaused) { return; }

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime * PlaySpeed >= CurrentFrame.Duration)
            {
                elapsedTime -= CurrentFrame.Duration / PlaySpeed;

                if (!UpdateCurrentFrame())
                {
                    Stop();
                }
            }
        }


        /// <summary>
        /// Change to the next frame in the animation
        /// </summary>
        public bool UpdateCurrentFrame()
        {
            int nextIndex = CurrentFrameIndex + playDirection;
            bool reachedEnd = nextIndex < 0 || nextIndex > TotalNumOfFrames;

            if (reachedEnd)
            {
                if (IsLooping)
                {
                    if (IsPingPong)
                    {
                        // Reverse play direction
                        playDirection = -playDirection;
                        CurrentFrameIndex += playDirection;
                    }
                    else
                    {
                        // Reset current frame to starting frame
                        CurrentFrameIndex = IsReversed ? TotalNumOfFrames : 0;
                    }
                }
                else
                {
                    remainingFrames--;

                    // If all frames have been shown end the animation
                    if (remainingFrames <= 0)
                    {
                        return false;
                    }

                    playDirection = -playDirection;
                    CurrentFrameIndex += playDirection;
                }
            }
            else
            {
                remainingFrames--;
                CurrentFrameIndex += playDirection;
            }

            return true;
        }

        #endregion Update






        #region Utility

        /// <summary>
        /// Start playing animation from first frame
        /// </summary>
        public void Play()
        {
            int statFrameIndex = IsReversed ? TotalNumOfFrames : 0;
            Play(statFrameIndex);
        }


        /// <summary>
        /// Start playing animation from specified frame
        /// </summary>
        /// <param name="startFrameIndex"></param>
        public void Play(int startFrameIndex)
        {
            if (startFrameIndex < 0 || startFrameIndex > TotalNumOfFrames)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Don't try to play again if already playing
            if (IsPlaying) { return; }

            CurrentFrameIndex = startFrameIndex;
            IsPlaying = true;
        }


        /// <summary>
        /// Start the animation from a random frame
        /// </summary>
        /// <remarks>
        /// Can be used to stop multiple animations from starting on the same frame and appearing sychronised
        /// </remarks>
        public void PlayRandom()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(0, TotalNumOfFrames);

            Play(randomIndex);
        }


        /// <summary>
        /// Pause the animation
        /// </summary>
        public void Pause()
        {
            if (!IsPlaying || IsPaused) { return; }
            IsPaused = true;
        }


        /// <summary>
        /// Resume the animation
        /// </summary>
        public void UnPause()
        {
            if (IsPlaying && !IsPaused) { return; }
            IsPaused = false;
        }


        /// <summary>
        /// Stop playing animation
        /// </summary>
        public void Stop()
        {
            if (!IsPlaying) { return; }
            IsPlaying = false;
        }


        public void Reset()
        {
            IsReversed = animation.IsReversed;
            IsPingPong = animation.IsPingPong;
            IsLooping = animation.IsLooping;
            IsPingPong = false;
            PlaySpeed = 1.0f;
            CurrentFrameIndex = IsReversed ? TotalNumOfFrames : 0;
            remainingFrames = IsPingPong ? TotalNumOfFrames * 2 : TotalNumOfFrames;
        }

        #endregion Utility
    }
}
