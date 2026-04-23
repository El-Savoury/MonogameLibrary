namespace MonogameLibrary.Tilemaps
{
    [Flags]
    public enum TileFlags : byte
    {
        None = 0,
        Solid = 1 << 2,
        Occupied = 1 << 3,
    }
}
