namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// A single frame of an animation
    /// </summary>
    public struct AnimationFrame
    {
        public TextureRegion Region { get; }
        public TimeSpan Duration { get; }

        /// <summary>
        /// Create an animation frame 
        /// </summary>
        /// <param name="region">Texture region for this frame</param>
        /// <param name="duration">Frame duration in milliseconds</param>
        public AnimationFrame(TextureRegion region, TimeSpan duration)
        {
            Region = region;
            Duration = duration;
        }
    }
}
