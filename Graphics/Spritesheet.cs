namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// A collection of sprites and animations created from a single base texture atlas
    /// </summary>
    public class Spritesheet
    {
        #region Members

        public string Name { get; }
        public TextureAtlas TextureAtlas { get; }

        // Collection of named animations
        private readonly Dictionary<string, Animation> animations = [];

        #endregion Members




        #region Init

        public Spritesheet()
        {
        }


        public Spritesheet(string name, TextureAtlas textureAtlas)
        {
            ArgumentNullException.ThrowIfNull(textureAtlas);

            Name = name;
            TextureAtlas = textureAtlas;
        }

        #endregion Init





        #region Utility

        /// <summary>
        /// Add an animation to the atlas
        /// </summary>
        /// <param name="name">Name to give this animation animation</param>
        /// <param name="animation">Animation to add</param>
        public void AddAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }


        public void AddAnimation(Enum animEnum, Animation animation)
        {
            animations.Add(animEnum.ToString(), animation);
        }


        /// <summary>
        /// Remove the specified animation from this atlas
        /// </summary>
        /// <param name="animationName">Name of animation to remove</param>
        /// <returns>True if animation removed</returns>
        public bool RemoveAnimation(string animationName)
        {
            return animations.Remove(animationName);
        }


        public bool RemoveAnimation(Enum animEnum)
        {
            return animations.Remove(animEnum.ToString());
        }


        /// <summary>
        /// Get the specified animation from this atlas
        /// </summary>
        /// <param name="animationName">Name of animation to get</param>
        /// <returns>Animation with this name</returns>
        public Animation GetAnimation(string animationName)
        {
            return animations[animationName];
        }


        public Animation GetAnimation(Enum animEnum)
        {
            return animations[animEnum.ToString()];
        }

        #endregion Utility






        #region Factory

        /// <summary>
        /// Create a new sprite using texture region name
        /// </summary>
        /// <param name="regionName">Name of texture region</param>
        /// <returns>New sprite from region with specified name</returns>
        public Sprite CreateSprite(string regionName)
        {
            TextureRegion region = TextureAtlas.GetRegion(regionName);
            return new Sprite(region);
        }


        /// <summary>
        /// Create a new sprite using texture region index
        /// </summary>
        /// <param name="regionIndex">Regions index in texture atlas</param>
        /// <returns>New sprite from region at specified index</returns>
        public Sprite CreateSprite(int regionIndex)
        {
            TextureRegion region = TextureAtlas.GetRegion(regionIndex);
            return new Sprite(region);
        }


        public void CreateAnimation(string animationName, List<AnimationFrame> frames, bool isReversed, bool isPingPong, bool isLooping)
        {
            Animation animation = new Animation(frames, isReversed, isPingPong, isLooping);
            animations.Add(animationName, animation);
        }


        public void CreateAnimation(string animationName, TimeSpan frameDuration, bool isReversed, bool isPingPong, bool isLooping, params int[] regionIndexes)
        {
            List<AnimationFrame> frames = new List<AnimationFrame>();

            foreach (int index in regionIndexes)
            {
                AnimationFrame frame = new AnimationFrame(TextureAtlas.GetRegion(index), frameDuration);
                frames.Add(frame);
            }

            CreateAnimation(animationName, frames, isReversed, isPingPong, isLooping);
        }

        #endregion Factory
    }
}
