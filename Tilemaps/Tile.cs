using MonogameLibrary.Utilities;

namespace MonogameLibrary.Tilemaps
{
    public struct Tile
    {
        private const uint ROTATION_MASK = 0b11; // bits 0 and 1

        public ushort Type = 0;
        public uint Flags = 0;

        public CardinalDir Rotation
        {
            get
            {
                return (CardinalDir)(Flags & ROTATION_MASK);
            }
            set
            {
                Flags &= ~ROTATION_MASK; // Clear current rotation bits using rotation bit mask
                Flags |= (byte)value;  // Set the new rotation bits
            }
        }


        public Tile(ushort typeID, CardinalDir rotation = 0)
        {
            Type = typeID;
            Rotation = rotation;
        }


        //public SpriteEffect GetDrawDir() 
        //{


        //} 
    }
}
