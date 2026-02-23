namespace MonogameLibrary.Graphics
{
    public class AnimatedSprite : Sprite
    {
        private Spritesheet _spritesheet;
        private Animation _animation;

        public AnimationController AnimationController { get; private set; }


        public AnimatedSprite(Spritesheet spritesheet, string defaultAnimation)
        {
            _spritesheet = spritesheet;
            _animation = spritesheet.GetAnimation(defaultAnimation);
            AnimationController = new AnimationController(_animation);
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
    }
}
