namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// A collection of sprites and animations created from a single base texture atlas
    /// </summary>
    public class Spritesheet
    {
        #region Members

        public TextureAtlas TextureAtlas { get; }

        private readonly Dictionary<string, TextureRegion> _sprites = [];
        private readonly Dictionary<string, Animation> _animations = [];

        #endregion Members




        #region Init

        public Spritesheet(TextureAtlas textureAtlas)
        {
            ArgumentNullException.ThrowIfNull(textureAtlas);

            TextureAtlas = textureAtlas;
        }

        #endregion Init





        #region Utility

        ///// <summary>
        ///// Add an animation to the atlas
        ///// </summary>
        ///// <param name="name">Name to give this animation</param>
        ///// <param name="animation">Animation to add</param>
        //public void AddAnimation(string name, Animation animation)
        //{
        //    _animations.Add(name, animation);
        //}


        ///// <summary>
        ///// Add an animation to the atlas
        ///// </summary>
        ///// <param name="animEnum"></param>
        ///// <param name="animation"></param>
        //public void AddAnimation(Enum animEnum, Animation animation)
        //{
        //    _animations.Add(animEnum.ToString(), animation);
        //}





        /// <summary>
        /// Remove the specified animation from this atlas
        /// </summary>
        /// <param name="animationName">Name of animation to remove</param>
        /// <returns>True if animation removed</returns>
        public bool RemoveAnimation(string animationName)
        {
            return _animations.Remove(animationName);
        }


        public bool RemoveAnimation(Enum animEnum)
        {
            return _animations.Remove(animEnum.ToString());
        }


        /// <summary>
        /// Get the specified animation from this atlas
        /// </summary>
        /// <param name="animationName">Name of animation to get</param>
        /// <returns>Animation with this name</returns>
        public Animation GetAnimation(string animationName)
        {
            return _animations[animationName];
        }


        public Animation GetAnimation(Enum animEnum)
        {
            return _animations[animEnum.ToString()];
        }

        #endregion Utility






        #region Factory

        /// <summary>
        /// Create a new sprite using texture region name
        /// </summary>
        /// <param name="regionName">Name of texture region</param>
        /// <returns>New sprite from region with specified name</returns>
        public Sprite GetSprite(string regionName)
        {
            TextureRegion region = _sprites[regionName];
            return new Sprite(region);
        }


        /// <summary>
        /// Create a new sprite using texture region index
        /// </summary>
        /// <param name="regionIndex">Regions index in texture atlas</param>
        /// <returns>New sprite from region at specified index</returns>
        public Sprite GetSprite(int regionIndex)
        {
            TextureRegion region = TextureAtlas.GetRegion(regionIndex);
            return new Sprite(region);
        }


        public void AddAnimation(string name, params AnimationFrame[] frames)
        {
            Animation anim = new Animation();

            foreach (AnimationFrame frame in frames)
            {
                anim.AddFrame(frame.Region, frame.Duration);
            }

            _animations.Add(name, anim);
        }


        public void AddAnimation(string name, TimeSpan frameDrawTime, params int[] frameIndexes)
        {
            Animation anim = new Animation();

            foreach (int index in frameIndexes)
            {
                anim.AddFrame(TextureAtlas.GetRegion(index), frameDrawTime);
            }

            _animations.Add(name, anim);
        }


        //public void CreateAnimation(string animationName, List<AnimationFrame> frames, bool isReversed, bool isPingPong, bool isLooping)
        //{
        //    Animation animation = new Animation(frames, isReversed, isPingPong, isLooping);
        //    animations.Add(animationName, animation);
        //}


        //public void CreateAnimation(string animationName, TimeSpan frameDuration, bool isReversed, bool isPingPong, bool isLooping, params int[] regionIndexes)
        //{
        //    List<AnimationFrame> frames = new List<AnimationFrame>();

        //    foreach (int index in regionIndexes)
        //    {
        //        AnimationFrame frame = new AnimationFrame(TextureAtlas.GetRegion(index), frameDuration);
        //        frames.Add(frame);
        //    }

        //    CreateAnimation(animationName, frames, isReversed, isPingPong, isLooping);
        //}

        #endregion Factory
    }
}
