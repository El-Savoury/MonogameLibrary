using MonogameLibrary.Graphics;
using System.Net.Http.Headers;

namespace MonogameLibrary.Tilemaps
{
    public class Tileset
    {
        private readonly TextureRegion[] tileTextures;

        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Count => Columns * Rows;


        public Tileset(TextureRegion textureRegion, int tileWidth, int tileHeight, int padding = 0)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = textureRegion.Width / TileWidth;
            Rows = textureRegion.Height / TileHeight;

            tileTextures = new TextureRegion[Count];

            for (int i = 0; i < Count; i++)
            {
                int x = i % Columns * TileWidth;
                int y = i / Columns * TileHeight;

                int regionPosX = textureRegion.SourceRectangle.X + x + padding;
                int regionPosY = textureRegion.SourceRectangle.Y + y + padding;

                tileTextures[i] = new TextureRegion(textureRegion.Texture, regionPosX, regionPosY, tileWidth, tileHeight);
            }
        }


        public TextureRegion GetTileTexture(int index)
        {
            return tileTextures[index];
        }


        public TextureRegion GetTileTexture(int column, int row)
        {
            int index = row * Columns + column;
            return GetTileTexture(index);
        }

    }
}
