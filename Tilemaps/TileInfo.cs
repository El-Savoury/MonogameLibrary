using MonogameLibrary.Graphics;

namespace MonogameLibrary.Tilemaps
{
    public class TileInfo
    {
        public int TilesetID { get; set; }
        public List<AnimationFrame> AnimationFrames { get; set; }

        public TileInfo(int tilesetID)
        {
            TilesetID = tilesetID;
        }
    }
}
