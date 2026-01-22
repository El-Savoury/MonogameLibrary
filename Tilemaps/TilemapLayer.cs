using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    public class TilemapLayer
    {
        #region Properties

        public Tileset Tileset { get; set; }
        public string Name { get; }
        public Vector2 Position { get; }
        public Tile[] Tiles { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }

        #endregion Properties





        #region Init

        public TilemapLayer(Tileset tilset, string name, Vector2 position, int tileWidth, int tileHeight, int columns, int rows)
        {
            Tileset = tilset;
            Name = name;
            Position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = columns;
            Rows = rows;

            int count = columns * Rows;
            Tiles = new Tile[count];
        }

        #endregion Init





        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile != null)
                {
                    tile.Update(gameTime);
                }
            }
        }

        #endregion Update






        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    int tileOffsetX = x * TileWidth;
                    int tileOffsetY = y * TileHeight;

                    Tile tile = GetTile(x, y);

                    if (tile != null)
                    {
                        TextureRegion region = Tileset.GetTileTexture(tile.TilesetIndex);
                        Vector2 tilePosition = new Vector2(Position.X + tileOffsetX, Position.Y + tileOffsetY);
                        region.Draw(spriteBatch, tilePosition, Color.White);
                    }
                }
            }
        }

        #endregion Draw






        #region Utility

        /// <summary>
        /// Get the tile at the specified tilemap layer index
        /// </summary>
        /// <param name="index">The tilemap index of desired tile</param>
        /// <returns>Tile at specified index</returns>
        public Tile GetTile(int index)
        {
            return Tiles[index];
        }


        /// <summary>
        /// Get the tile at the specified tilemap layer column and row 
        /// </summary>
        /// <param name="column">The column required tile is in</param>
        /// <param name="row">The row required tile is in</param>
        /// <returns>Tile at specified column and row</returns>
        public Tile GetTile(int column, int row)
        {
            int index = row * Columns + column;
            return GetTile(index);
        }


        /// <summary>
        /// Get the tile at specified world position
        /// </summary>
        /// <param name="worldPosition">World position coordinates</param>
        /// <returns>Tile at specified world coordinates</returns>
        public Tile GetTile(Vector2 worldPosition)
        {
            Vector2 offset = worldPosition - Position;
            int column = (int)Math.Floor(offset.X / TileWidth);
            int row = (int)Math.Floor(offset.Y / TileHeight);

            return GetTile(column, row);
        }


        /// <summary>
        /// Set the tile at specified index
        /// </summary>
        /// <param name="index">Tilemap index</param>
        /// <param name="tile">Tile to set</param>
        public void SetTile(int index, Tile tile)
        {
            Tiles[index] = tile;
        }


        /// <summary>
        ///  Set the tile at specified column and row
        /// </summary>
        /// <param name="column">Column containing tile</param>
        /// <param name="row">Row containing tile</param>
        /// <param name="tile">Tile to set</param>
        public void SetTile(int column, int row, Tile tile)
        {
            int index = row * Columns + column;
            SetTile(index, tile);
        }

        #endregion Utility
    }
}
