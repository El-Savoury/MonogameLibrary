namespace MonogameLibrary.Graphics
{
    public class AnimatedSprite : Sprite
    {
        private Spritesheet _spritesheet;
        private Animation _animation;

        public AnimationController AnimationController { get; private set; }


        public AnimatedSprite(Spritesheet spritesheet, string defaultAnimationName)
        {
            _spritesheet = spritesheet;
            _animation = spritesheet.GetAnimation(defaultAnimationName);
            AnimationController = new AnimationController(_animation);

            Region = AnimationController.CurrentFrame.Region;
        }


        public void Update(GameTime gameTime)
        {
            AnimationController.Update(gameTime);
            Region = AnimationController.CurrentFrame.Region;
        }


        public void SetAnimation(string animationName)
        {
            Animation animation = _spritesheet.GetAnimation(animationName);
            AnimationController = new AnimationController(animation);
        }


        public void SetCurrentFrame(int frameIndex)
        {
            AnimationController.CurrentFrameIndex = frameIndex;
        }

    }
}
