namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// A visible texture on screen
    /// </summary>
    public class Sprite
    {
        public TextureRegion Region { get; set; }
        public Color Colour { get; set; } = Color.White;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0.0f;

        // Width in pixels
        public float Width => Region.Width * Scale.X;

        // Height in pixels
        public float Height => Region.Height * Scale.Y;


        /// <summary>
        /// Creates new sprite
        /// </summary>
        public Sprite() { }


        /// <summary>
        /// Creates a sprite with a source texture region
        /// </summary>
        /// <param name="region">Region of source texture to draw as a sprite</param>
        public Sprite(TextureRegion region)
        {
            Region = region;
        }


        /// <summary>
        /// Draw this sprite
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Region.Draw(spriteBatch, position, Colour);
        }


        /// <summary>
        /// Set the origin of this sprite to the centre
        /// </summary>
        public void CentreOrigin()
        {
            Origin = new Vector2(Region.Width, Region.Height) * 0.5f;
        }




        //#region Factory

        //public static Sprite Create(TextureRegion region)
        //{
        //    return new Sprite(region);
        //}


        //public static Sprite Create(TextureAtlas atlas, int regionIndex)
        //{
        //    TextureRegion region = atlas.GetRegion(regionIndex);
        //    return new Sprite(region);
        //}


        //public static Sprite Create(TextureAtlas atlas, string regionName)
        //{
        //    TextureRegion region = atlas.GetRegion(regionName);
        //    return new Sprite(region);
        //}

        //#endregion Factory
    }
}