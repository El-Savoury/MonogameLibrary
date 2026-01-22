namespace MonogameLibrary.Graphics
{
    public class Animation
    {
        #region Properties

        public List<AnimationFrame> Frames { get; set; }
        public bool IsLooping { get; set; } = false;
        public bool IsReversed { get; set; } = false;
        public bool IsPingPong { get; set; } = false;

        #endregion Properties





        #region Init

        /// <summary>
        /// Creates a new empty animation with no frames
        /// </summary>
        public Animation()
        {
            Frames = new List<AnimationFrame>();
        }


        /// <summary>
        /// Creates a new animation
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="isLooping"></param>
        /// <param name="isReversed"></param>
        public Animation(List<AnimationFrame> frames, bool isReversed, bool isPingPong, bool isLooping)
        {
            Frames = frames;
            IsReversed = isReversed;
            IsPingPong = isPingPong;
            IsLooping = isLooping;
        }

        #endregion Init





        #region Utility

        public void AddFrame(TextureRegion region, TimeSpan duration)
        {
            AnimationFrame frame = new AnimationFrame(region, duration);
            Frames.Add(frame);
        }

        #endregion Utility
    }
}
