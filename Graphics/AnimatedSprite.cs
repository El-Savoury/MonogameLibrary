namespace MonogameLibrary.Graphics
{
    public class AnimatedSprite : Sprite
    {
        private readonly Animation animation;
        public AnimationController AnimationController { get; private set; }


        public AnimatedSprite() { }


        public AnimatedSprite(Animation anim)
        {
            animation = anim;
            AnimationController = new AnimationController(animation);
        }


        public AnimatedSprite(Spritesheet atlas, string animationName)
        {
            animation = atlas.GetAnimation(animationName);
            AnimationController = new AnimationController(animation);
        }


        public void Update(GameTime gameTime)
        {
            AnimationController.Update(gameTime);
            Region = AnimationController.CurrentFrame.Region;
        }
    }
}
