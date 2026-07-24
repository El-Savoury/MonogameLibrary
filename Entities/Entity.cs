using MonogameLibrary.Maths;

namespace MonogameLibrary.Entities
{
    /// <summary>
    /// An object in the game world with a position and bounding box
    /// </summary>
    public abstract class Entity
    {
        // World position
        public Vector2 Position;

        // Width in pixels
        public int Width { get; set; } = 0;

        // Height in pixels
        public int Height { get; set; } = 0;

        public Point Size => new Point(Width, Height);

        public bool IsEnabled { get; set; } = true;


        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
            }
        }


        /// <summary>
        /// Create a new entity
        /// </summary>
        public Entity()
        {
            Position = Vector2.Zero;
        }


        /// <summary>
        /// Creates a new entity at specified position
        /// </summary>
        /// <param name="position">World position to create entity</param>
        public Entity(Vector2 position)
        {
            Position = position;
        }


        /// <summary>
        /// Creates a new enitity with specified position and size
        /// </summary>
        /// <param name="position">World position</param>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        public Entity(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }


        /// <summary>
        /// Load assests used by this entity
        /// </summary>
        public abstract void LoadContent();


        /// <summary>
        /// Update this entity
        /// </summary>
        /// <param name="gameTime">Monogame frame info</param>
        public abstract void Update(GameTime gameTime);


        /// <summary>
        /// Draw this entity
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);


        /// <summary>
        /// Remove this entity from the game
        /// </summary>
        public virtual void Destroy()
        {
            IsEnabled = false;
        }
    }
}
