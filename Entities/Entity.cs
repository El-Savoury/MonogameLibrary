using MonogameLibrary.Maths;

namespace MonogameLibrary.Entities
{
    /// <summary>
    /// Represents an object in the game world
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

        public bool Enabled { get; set; } = true;


        public RectF Bounds
        {
            get
            {
                return new RectF(Position, new Vector2(Size.X, Size.Y));
            }
        }


        /// <summary>
        /// Creates a new game object
        /// </summary>
        public Entity()
        {
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Creates a new game object at specified position
        /// </summary>
        /// <param name="position">World position to create entity</param>
        public Entity(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Creates a new game object with specified position and size
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
            Enabled = false;
        }
    }
}
