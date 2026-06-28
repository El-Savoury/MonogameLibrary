using MonogameLibrary.Maths;

namespace MonogameLibrary.Tilemaps.TilemapObjects
{
    // An object in a tilemap that has behaviour 
    public abstract class TilemapObject
    {
        #region Members

        protected Tilemap _tilemap;
        public int Width { get; set; }
        public int Height { get; set; }
        public Point MapIndex { get; set; }
        public Vector2 Position { get; set; }
        public RectF Bounds => new RectF(Position.X, Position.Y, Width, Height);

        #endregion Members






        #region Init

        public TilemapObject(Tilemap tilemap, int xIndex, int yIndex)
        {
            _tilemap = tilemap;
            MapIndex = new Point(xIndex, yIndex);
            Position = _tilemap.IndexToWorldPos(MapIndex.X, MapIndex.Y);
        }


        public abstract void LoadContent();

        #endregion Init







        #region Update

        public abstract void Update(GameTime gameTime);

        #endregion Update






        #region Draw

        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion Draw
    }
}
