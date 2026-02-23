using MonogameLibrary.Utilities;

namespace MonogameLibrary.Graphics
{
    public class TextureRegion
    {
        public string Name { get; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
       
        // Width in pixels
        public int Width => SourceRectangle.Width;  
        
        // Height in pixels
        public int Height => SourceRectangle.Height;  


        public TextureRegion() { }


        public TextureRegion(string name, Texture2D texture, int x, int y, int width, int height)
        {
            Name = name;
            Texture = texture;
            SourceRectangle = new Rectangle(x, y, width, height);
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color colour)
        {
            Draw2D.I.DrawTexturePart(spriteBatch, Texture, position, SourceRectangle);
        }
    }
}
