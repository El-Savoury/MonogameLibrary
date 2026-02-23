using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    public class Tileset
    {
        private readonly TextureRegion[] _tileTextures;

        public string Name { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Count => Columns * Rows;


        public Tileset(string name, TextureRegion textureRegion, int tileWidth, int tileHeight, int padding = 0)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = textureRegion.Width / TileWidth;
            Rows = textureRegion.Height / TileHeight;

            _tileTextures = new TextureRegion[Count];

            for (int i = 0; i < Count; i++)
            {
                int x = i % Columns * TileWidth;
                int y = i / Columns * TileHeight;

                int regionX = textureRegion.SourceRectangle.X + x + padding;
                int regionY = textureRegion.SourceRectangle.Y + y + padding;

                _tileTextures[i] = new TextureRegion(name, textureRegion.Texture, regionX, regionY, tileWidth, tileHeight);
            }
        }


        public TextureRegion GetTileTexture(int index)
        {
            return _tileTextures[index];
        }


        public TextureRegion GetTileTexture(int column, int row)
        {
            int index = row * Columns + column;
            return GetTileTexture(index);
        }
    }
}
