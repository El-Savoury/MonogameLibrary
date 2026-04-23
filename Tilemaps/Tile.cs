using MonogameLibrary.Utilities;
using System;

namespace MonogameLibrary.Tilemaps
{
    public struct Tile
    {
        #region Constants

        private const uint ROTATION_MASK = 0b11; // bits 0 and 1

        #endregion Constants




        #region Properties

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

        #endregion Properties





        #region Init

        public Tile(ushort typeID, CardinalDir rotation = 0)
        {
            Type = typeID;
            Rotation = rotation;
        }


        public Tile(ushort typeID, CardinalDir rotation, uint flag)
        {
            Type = typeID;
            Rotation = rotation;
            Flags |= flag;
        }

        #endregion Init






        #region Util

        public void AddFlag(Enum flag)
        {
            Flags |= Convert.ToUInt32(flag);
        }


        public bool HasFlag(Enum flag)
        {
            return (Flags & Convert.ToUInt32(flag)) != 0;
        }

        #endregion Util
    }
}
