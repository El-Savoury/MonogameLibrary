namespace MonogameLibrary.Tilemaps
{
    [Flags]
    public enum TileFlags : byte
    {
        None = 0,
        FlipX = 1 << 0,
        FlipY = 1 << 1,
        Rotate90 = 1 << 2,
        Rotate180 = 1 << 3,
        Rotate270 = 1 << 4,
    }


    public struct Tile
    {
        public Enum Type;
        public ushort Flags;

        public Tile(Enum tileType/*, Enum tileFlags*/)
        {
            Type = tileType;
            //Flags = Convert.ToUInt16(tileFlags);
        }
    }
}
