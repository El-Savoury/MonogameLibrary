using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    public enum TileCollision
    {
        None = 0, // Has no hit box
        Solid = 1, // Blocks all movement 
        Passable = 2, // Has a hit box to allow interactions but does not block movenent
        Platform = 3, // Can be overlapped on three sides but acts solid on one side based on rotation
    }


    public class TileInfo
    {
        public int TilesetIndex { get; set; }
        public TileCollision Collision { get; set; }
        public List<AnimationFrame> AnimationFrames { get; set; }


        public bool IsSolid => Collision == TileCollision.Solid;


        public TileInfo(int tilesetIndex, TileCollision collision)
        {
            TilesetIndex = tilesetIndex;
            Collision = collision;
        }
    }
}
