namespace MonogameLibrary.Utilities
{
    /// <summary>
    /// Represents one of the four cardinal directions
    /// </summary>
    public enum CardinalDir : byte
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }


    /// <summary>
    /// Extends cardinal direction enum with functions to convert to other useful data types
    /// </summary>
    public static class CardinalDirExtension
    {
        /// <summary>
        /// Convert a cardinal direction to a point
        /// </summary>
        /// <param name="direction">Cardinal direction to convert</param>
        /// <returns>Point representing x and y directions</returns>
        public static Point ToPoint(CardinalDir direction)
        {
            switch (direction)
            {
                case CardinalDir.Down:
                    return new Point(0, 1);
                case CardinalDir.Up:
                    return new Point(0, -1);
                case CardinalDir.Left:
                    return new Point(-1, 0);
                case CardinalDir.Right:
                    return new Point(1, 0);
            }

            return Point.Zero;
        }


        /// <summary>
        /// Convert a cardinal direction to a vector2
        /// </summary>
        /// <param name="direction">Cardinal direction to convert</param>
        /// <returns>Vector2 representing x and y directions</returns>
        public static Vector2 ToVector(CardinalDir direction)
        {
            switch (direction)
            {
                case CardinalDir.Down:
                    return new Vector2(0, 1);
                case CardinalDir.Up:
                    return new Vector2(0, -1);
                case CardinalDir.Left:
                    return new Vector2(-1, 0);
                case CardinalDir.Right:
                    return new Vector2(1, 0);
            }

            return Vector2.Zero;
        }


        /// <summary>
        /// Convert a cardinal direction to an angle in degrees
        /// </summary>
        /// <param name="direction">Cardinal direction to convert</param>
        /// <returns>Float representing a 360 degree direction</returns>
        public static float ToDegrees(CardinalDir direction)
        {
            switch (direction)
            {
                case CardinalDir.Up:
                    return 0;
                case CardinalDir.Right:
                    return 90;
                case CardinalDir.Down:
                    return 180;
                case CardinalDir.Left:
                    return 270;
            }

            return 0;
        }


        /// <summary>
        /// Convert a cardinal direction to a radian angle
        /// </summary>
        /// <param name="direction">Cardinal direction to convert</param>
        /// <returns>double representing a radian angle</returns>
        public static float ToRadians(CardinalDir direction)
        {
            float rotation = ToDegrees(direction);
            return (float)(rotation * (Math.PI / 180));
        }

    }
}