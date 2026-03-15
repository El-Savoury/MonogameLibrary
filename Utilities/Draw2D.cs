using MonogameLibrary.Maths;

namespace MonogameLibrary.Utilities
{
    public class Draw2D : Singleton<Draw2D>
    {
        private Texture2D _dummyTexture
        {
            get
            {
                // Create an array of coloured pixels for use as a placeholder texture
                Texture2D dummyTexture = new Texture2D(Core.GraphicsDevice, 1, 1);
                Color[] data = [Color.White];
                dummyTexture.SetData(data);

                return dummyTexture;
            }
        }



        #region Primitives

        /// <summary>
        /// Draw a rectangle of specified colour
        /// </summary>
        public void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color colour)
        {
            spriteBatch.Draw(_dummyTexture, rect, colour);
        }


        /// <summary>
        /// Draw a rectangle of specified colour
        /// </summary>
        public void DrawRect(SpriteBatch spriteBatch, int x, int y, int width, int height, Color colour)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            DrawRect(spriteBatch, rect, colour);
        }


        /// <summary>
        /// Draw a rectangle of specified colour
        /// </summary>
        public void DrawRect(SpriteBatch spriteBatch, RectF rectF, Color colour)
        {
            Rectangle rect = rectF.ToRectangle;
            DrawRect(spriteBatch, rect, colour);
        }


        /// <summary>
        /// Draw a single pixel
        /// </summary>
        public void DrawDot(SpriteBatch spriteBatch, int x, int y, Color colour)
        {
            Rectangle dot = new Rectangle(x, y, 1, 1);
            DrawRect(spriteBatch, dot, colour);
        }


        /// <summary>
        /// Draw a single pixel
        /// </summary>
        public void DrawDot(SpriteBatch spriteBatch, Point position, Color colour)
        {
            Rectangle dot = new Rectangle(position.X, position.Y, 1, 1);
            DrawRect(spriteBatch, dot, colour);
        }


        /// <summary>
        /// Draw a single pixel
        /// </summary>
        public void DrawDot(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle dot = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            DrawRect(spriteBatch, dot, color);
        }


        /// <summary>
        /// Draw a line from point A to B
        /// </summary>
        public void DrawLine(SpriteBatch spriteBatch, Vector2 pointA, Vector2 pointB, Color col, float width = 1.0f)
        {
            float distance = Vector2.Distance(pointA, pointB);
            float angle = (float)Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X);
            DrawLine(spriteBatch, pointA, distance, angle, col, width);
        }


        /// <summary>
        /// Draw a line from point A by an angle.
        /// </summary>
        public void DrawLine(SpriteBatch spriteBatch, Vector2 pointA, float length, float angle, Color col, float width = 1.0f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, width);
            spriteBatch.Draw(_dummyTexture, pointA, null, col, angle, origin, scale, SpriteEffects.None, 0);
        }

        #endregion Primitives






        #region Textures

        /// <summary>
        /// Draw texture at Vector2 position
        /// </summary>
        public void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        /// <summary>
        /// Draw texture with all render options
        /// </summary>
        public void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, Color colour, float alpha, float roation, Vector2 origin, float scale, SpriteEffects effect, float depth)
        {
            spriteBatch.Draw(texture, pos, null, colour * alpha, roation, origin, scale, effect, depth);
        }


        /// <summary>
        /// Draw part of a texture defined by a source rect
        /// </summary>
        public void DrawTexturePart(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRect, Color colour)
        {
            spriteBatch.Draw(texture, position, sourceRect, colour);
        }


        /// <summary>
        /// Draw part of a texture defined by a source rect with all render options
        /// </summary>
        public void DrawTexturePart(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRect, Color colour, float alpha, float rotation, Vector2 origin, float scale, SpriteEffects effects, float depth)
        {
            spriteBatch.Draw(texture, position, sourceRect, colour * alpha, rotation, origin, scale, effects, depth);
        }



        /// <summary>
        /// Draw part of a texture defined by a source rect with all render options
        /// </summary>
        public void DrawTexturePart(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle sourceRect, Color colour, float alpha, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float depth)
        {
            spriteBatch.Draw(texture, position, sourceRect, colour * alpha, rotation, origin, scale, effects, depth);
        }


        #endregion Textures
    }
}
