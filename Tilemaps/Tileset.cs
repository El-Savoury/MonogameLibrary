using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A collection of tile templates that can be loaded into a tilemap
    /// </summary>
    public class Tileset
    {
        public string Name { get; }
        public TextureRegion TilesetTexture { get; }
        public Dictionary<int, TileTemplate> TileTemplates { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Padding { get; }
        public int Count => Columns * Rows;


        /// <summary>
        /// Create a new tileset
        /// </summary>
        /// <param name="name">The name of this tileset</param>
        /// <param name="texture">The texture region being used for this tileset</param>
        /// <param name="tileWidth">The width of a tile in this tileset</param>
        /// <param name="tileHeight">The height of a tile in this tileset</param>
        /// <param name="padding">The size in pixels of padding between tiles in the tileset texture</param>
        public Tileset(string name, TextureRegion texture, int tileWidth, int tileHeight, int padding = 0)
        {
            Name = name;
            TilesetTexture = texture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = texture.Width / TileWidth;
            Rows = texture.Height / TileHeight;
            Padding = padding;
        }


        /// <summary>
        /// Add a new tile type to this tileset
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public void AddTileType(int type, int tilesetID, TileCollision collision)
        {
            TileTemplate template = new TileTemplate(type, tilesetID, collision);
            TileTemplates.Add(tilesetID, template); 
        }


        ///// <summary>
        ///// Get tile metadata
        ///// </summary>
        ///// <param name="tilesetID"></param>
        ///// <returns></returns>
        //public TileTemplate GetTileInfo(ushort tilesetID)
        //{
        //    return TileTemplates.GetInfo(tilesetID);
        //}


        /// <summary>
        /// Get the texture of a tile in this tileset
        /// </summary>
        /// <param name="tilesetID">ID of the tile we want the texture of</param>
        /// <returns>Texture region used by tile with specified ID</returns>
        public TextureRegion GetTileTexture(int tilesetID)
        {
            return TileTemplates[tilesetID].


        }


        public TextureRegion GetTileTexture(int textureIndex)
        {
            int x = textureIndex % Columns * TileWidth;
            int y = textureIndex / Columns * TileHeight;
            int regionX = TilesetTexture.SourceRectangle.X + x + Padding;
            int regionY = TilesetTexture.SourceRectangle.Y + y + Padding;

            return new TextureRegion(Name, TilesetTexture.Texture, regionX, regionY, TileWidth, TileHeight);
        }


        public TextureRegion GetTileTexture(int column, int row)
        {
            int regionX = TilesetTexture.SourceRectangle.X + column * (TileWidth + Padding);
            int regionY = TilesetTexture.SourceRectangle.Y + row * (TileHeight + Padding);

            return new TextureRegion(Name, TilesetTexture.Texture, regionX, regionY, TileWidth, TileHeight);
        }
    }
}
