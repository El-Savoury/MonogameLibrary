namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// Base tilemap layer class
    /// </summary>
    public abstract class TilemapLayer
    {
        public string Name { get; }
        public Vector2 Position { get; }
        public float Opacity { get; set; }
        public bool IsVisible { get; set; }


        public TilemapLayer(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}