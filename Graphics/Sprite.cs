namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// A visible texture on screen
    /// </summary>
    public class Sprite
    {
        public TextureRegion Region { get; set; }
        public Color Colour { get; set; } = Color.White;
        public float Alpha { get; set; } = 1.0f;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; } = 0.0f;
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
        public Sprite()
        {
        }


        /// <summary>
        /// Create a sprite with a source texture region
        /// </summary>
        /// <param name="region">Region of source texture to draw as a sprite</param>
        public Sprite(TextureRegion region)
        {
            Region = region;
        }


        /// <summary>
        /// Create a sprite with all render options
        /// </summary>
        /// <param name="region"></param>
        /// <param name="colour"></param>
        /// <param name="alpha"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        public Sprite(TextureRegion region, Color colour, float alpha, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Region = region;
            Colour = colour;
            Alpha = alpha;
            Rotation = rotation;
            Origin = origin;
            Scale = new Vector2(scale, scale);
            Effects = effects;
            LayerDepth = layerDepth;
        }


        /// <summary>
        /// Draw this sprite
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // We still want the physics position to line up with the top left of the tile
            // so we offset the draw position relative to the sprites origin
            Vector2 drawPosition = position + Origin;

            Region.Draw(spriteBatch, drawPosition, Colour, Alpha, MathHelper.ToRadians(Rotation), Origin, Scale, Effects, LayerDepth);
        }


        /// <summary>
        /// Set the origin of this sprite to the centre
        /// </summary>
        public void CentreOrigin()
        {
            Origin = new Vector2(Region.Width, Region.Height) * 0.5f;
        }
    }
}