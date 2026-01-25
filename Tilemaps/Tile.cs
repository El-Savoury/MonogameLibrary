namespace MonogameLibrary.Tilemaps
{
    [Flags]
    public enum TileFlags : ushort
    {
        None = 0,
        FlipHorizontal = 1 << 0,
        FlipVertical = 1 << 1,
        FlipDiagonal = 1 << 2,
    }


    public struct Tile
    {
        public ushort Type;
        public ushort Flags;

        public Tile(Enum tileType, Enum tileFlags)
        {
            Type = Convert.ToUInt16(tileType);
            Flags = Convert.ToUInt16(tileFlags);
        }
    }
}
