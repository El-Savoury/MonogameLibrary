using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A layer of tiles in a tilemap
    /// </summary>
    public class TilemapLayer
    {
        #region Properties

        public string Name { get; }
        public Vector2 Position { get; }
        public Tile[,] Tiles { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public bool IsVisible { get; set; }
        public float Opacity { get; set; }
        public Tileset Tileset { get; set; }

        #endregion Properties





        #region Init

        public TilemapLayer(string name, Vector2 position, int tileWidth, int tileHeight, int columns, int rows, Tileset tileset)
        {
            Name = name;
            Position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = columns;
            Rows = rows;
            IsVisible = true;
            Opacity = 1.0f;
            Tileset = tileset;
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
            if(!IsVisible) { return; }

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    TextureRegion region = Tileset.GetTileTexture(Tiles[x, y].TilesetID);
                    float rotation = CardinalDirExtension.ToRadians(Tiles[x, y].Rotation);

                    int tileOffsetX = x * TileWidth;
                    int tileOffsetY = y * TileHeight;
                    Vector2 tilePosition = new Vector2(Position.X + tileOffsetX, Position.Y + tileOffsetY);
                    Vector2 tileOrigin = new Vector2(TileWidth, TileHeight) * 0.5f;

                    region.Draw(spriteBatch, tilePosition + tileOrigin, Color.White, 1.0f, rotation, tileOrigin, 1.0f, SpriteEffects.None, 1.0f);
                }
            }
        }

        #endregion Draw






        #region Util

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
        /// <param name="worldPos">World position coordinates</param>
        /// <returns>Tile at specified world coordinates</returns>
        public Tile GetTile(Vector2 worldPos)
        {
            Vector2 offset = worldPos - Position;
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
        public void SetTile(int column, int row, int tilesetID)
        {
            Tile tile = Tile.FromTemplate(Tileset.TileTemplates[tilesetID]);
            Tiles[column, row] = tile;
        }

        #endregion Util
    }
}
