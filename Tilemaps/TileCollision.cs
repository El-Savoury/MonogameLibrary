namespace MonogameLibrary.Tilemaps
{
    public enum TileCollision
    {
        None, // Has no hit box
        Solid, // Blocks all movement
        Passable, // Has a hit box to allow interactions but does not block movement
        OneWay, // Solid on one side and passable on the others based on rotation direction
    }
}
