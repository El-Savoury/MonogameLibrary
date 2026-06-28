namespace MonogameLibrary.Tilemaps
{
    public enum TileCollision
    {
        // Has no hit box
        None,

        // Blocks all movement
        Solid,

        // Has a hit box to allow interactions but does not block movement
        Passable,

        // Solid on one side and passable on the others based on rotation direction
        OneWay,
    }
}
