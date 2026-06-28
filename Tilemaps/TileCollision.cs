namespace MonogameLibrary.Tilemaps
{
    public enum TileCollision
    {
        // Has no hit box
        None = 0,

        // Blocks all movement
        Solid = 1,

        // Has a hit box to allow interactions but does not block movement
        Passable = 2,

        // Solid on one side and passable on the others based on rotation direction
        OneWay = 3,
    }
}
