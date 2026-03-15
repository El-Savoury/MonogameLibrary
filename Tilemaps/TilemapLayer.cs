using MonogameLibrary.Graphics;
using MonogameLibrary.Input;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    public class TilemapLayer
    {
        #region Properties

        private TileTypeRegistry _tileTypeRegistry;

        public Tileset Tileset { get; set; }
        public string Name { get; }
        public Vector2 Position { get; }
        public Tile[,] Tiles { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }

        #endregion Properties





        #region Init

        public TilemapLayer(string name, Vector2 position, int tileWidth, int tileHeight, int columns, int rows, Tilemap parent)
        {
            Name = name;
            Position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = columns;
            Rows = rows;

            // TODO : Don't do this
            _tileTypeRegistry = parent.TileRegistry;
            Tileset = parent.Tileset;

            Tiles = new Tile[columns, rows];
        }

        #endregion Init





        #region Update

        public void Update(GameTime gameTime)
        {
        }

        #endregion Update






        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    TileInfo info = _tileTypeRegistry.GetInfo(Tiles[x, y].Type);
                    TextureRegion region = Tileset.GetTileTexture(info.TilesetID);

                    float rotation = 0;

                    switch (Tiles[x, y].Rotation)
                    {
                        case CardinalDir.Up:
                            rotation = 0;
                            break;
                        case CardinalDir.Right:
                            rotation = 90;
                            break;
                        case CardinalDir.Down:
                            rotation = 180;
                            break;
                        case CardinalDir.Left:
                            rotation = 270;
                            break;
                    }

                    int tileOffsetX = x * TileWidth;
                    int tileOffsetY = y * TileHeight;
                    Vector2 tilePosition = new Vector2(Position.X + tileOffsetX, Position.Y + tileOffsetY);
                    Vector2 tileOrigin = new Vector2(TileWidth, TileHeight) * 0.5f;

                    //  region.Draw(spriteBatch, tilePosition, Color.White);
                    region.Draw(spriteBatch, tilePosition + tileOrigin, Color.White, 1.0f, MathHelper.ToRadians(rotation), tileOrigin, 1.0f, SpriteEffects.None, 1.0f);
                }
            }
        }

        #endregion Draw






        #region Utility

        /// <summary>
        /// Get the tile at the specified tilemap layer column and row 
        /// </summary>
        /// <param name="column">The column required tile is in</param>
        /// <param name="row">The row required tile is in</param>
        /// <returns>Tile at specified column and row</returns>
        public Tile GetTile(int column, int row)
        {
            return Tiles[column, row];
        }


        /// <summary>
        /// Get the tile at specified world position
        /// </summary>
        /// <param name="worldPosition">World position coordinates</param>
        /// <returns>Tile at specified world coordinates</returns>
        public Tile GetTile(Vector2 worldPosition)
        {
            Vector2 offset = worldPosition - Position;
            int column = (int)(offset.X / TileWidth);
            int row = (int)(offset.Y / TileHeight);

            return GetTile(column, row);
        }


        /// <summary>
        ///  Set the tile at specified column and row
        /// </summary>
        /// <param name="column">Column containing tile</param>
        /// <param name="row">Row containing tile</param>
        /// <param name="tile">Tile to set</param>
        public void SetTile(int column, int row, Tile tile)
        {
            Tiles[column, row] = tile;
        }

        #endregion Utility
    }
}
