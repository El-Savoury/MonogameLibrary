namespace MonogameLibrary.Utilities
{
    // Represents one of the four cardinal directions
    public enum CardinalDir : byte
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }


    public static class CardinalDirExtension
    {

        public static Point ConvertToPoint(CardinalDir direction)
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



        public static Vector2 ConvertToVector(CardinalDir direction)
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
    }
}
