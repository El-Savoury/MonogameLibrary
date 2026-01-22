using System.Security.Cryptography.X509Certificates;

namespace MonogameLibrary.Maths
{
    /// <summary>
    /// A rectangle with floating point dimensions
    /// </summary>
    public struct RectF
    {
        public float X { get; set; }  // Top left X co-ordinate
        public float Y { get; set; } // Top left Y co-ordinate
        public float Width { get; set; } // Width in pixels
        public float Height { get; set; }  // Height in pixels

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;
        public Vector2 Size => new Vector2(Width, Height);
        public Vector2 Min => new Vector2(X, Y);
        public Vector2 Max => Min + Size;
        public Vector2 Centre => new Vector2(X + Width * 0.5f, Y + Height * 0.5f);
        public Rectangle ToRectangle => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);


        /// <summary>
        /// Create a new RectF
        /// </summary>
        /// <remarks>
        /// Default position is 0,0 and default width and height is 10 pixels
        /// </remarks>
        public RectF()
        {
            X = 0;
            Y = 0;
            Width = 10;
            Height = 10;
        }

        public RectF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        public RectF(Vector2 pos, float width, float height)
        {
            X = pos.X;
            Y = pos.Y;
            Width = width;
            Height = height;
        }


        public RectF(Vector2 pos, Vector2 size)
        {
            X = pos.X;
            Y = pos.Y;
            Width = size.X;
            Height = size.Y;
        }


        public RectF(Rectangle rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }


        #region Utility

        /// <summary>
        /// Adds the size of the two specified rectangle
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Creates a new rectangle using the combined size</returns>
        public static RectF operator +(RectF a, RectF b)
        {
            float x = a.X - b.Width * 0.5f;
            float y = a.Y - b.Height * 0.5f;
            float width = a.Width + b.Width;
            float height = a.Height + b.Height;

            return new RectF(x, y, width, height);
        }


        /// <summary>
        /// Expands the size of the rectangle in all directions
        /// </summary>
        /// <param name="xAmount">Amount to increase left and right edges</param>
        /// <param name="yAmount">Amount to increase top and bottom edges</param>
        public void Inflate(float xAmount, float yAmount)
        {
            X -= xAmount * 0.5f;
            Y -= yAmount * 0.5f;
            Width += xAmount;
            Height += yAmount;
        }


        /// <summary>
        /// Shrinks the size of the rectangle in all directions
        /// </summary>
        /// <param name="xAmount">Amount to decrease left and right edges</param>
        /// <param name="yAmount">Amount to decrease top and bottom edges</param>
        public void Deflate(float xAmount, float yAmount)
        {
            X += xAmount * 0.5f;
            Y += yAmount * 0.5f;
            Width -= xAmount;
            Height -= yAmount;
        }

        #endregion Utility
    }
}
