using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    public abstract class AnimatedTile 
    {
        private int[] tileIndexes;
        private readonly Animation animation;

        public AnimationController AnimationController { get; private set; }


        public AnimatedTile()
        {
        }


        public AnimatedTile(Tileset tileset, TimeSpan frameDuration, params int[] tilesetIndexes)
        {
            tileIndexes = tilesetIndexes;
            animation = new Animation();

            foreach (var index in tilesetIndexes)
            {
                TextureRegion region = tileset.GetTileTexture(index);
                animation.AddFrame(region, frameDuration);
            }

            animation.IsLooping = true;
            AnimationController = new AnimationController(animation);
        }


        //public override void Update(GameTime gameTime)
        //{
        //    AnimationController.Update(gameTime);

        //    int animFrameIndex = AnimationController.CurrentFrameIndex;
        //    TilesetIndex = tileIndexes[animFrameIndex];
        //}
    }
}
