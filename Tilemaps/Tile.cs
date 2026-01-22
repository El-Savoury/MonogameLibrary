namespace MonogameLibrary.Tilemaps
{
    public abstract class Tile
    {
        // The index of this tiles texture in a tileset
        public int TilesetIndex { get; set; }

        public Tile()
        {
        }


        public Tile(int tilesetIndex)
        {
            TilesetIndex = tilesetIndex;
        }


        public abstract void Update(GameTime gameTime);

    }
}
