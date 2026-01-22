using MonogameLibrary.Utilities;

namespace MonogameLibrary.Entities
{
    public class Mover : DynamicEntity
    {
        public Mover() : base()
        {
            Width = 70;
            Height = 70;
        }

        public override void LoadContent()
        {

        }


        public override void Update(GameTime gameTime)
        {
            Position += Velocity * Utility.I.DeltaTime(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Draw2D.I.DrawRect(spriteBatch, Bounds, Color.Green * 4f);
        }
    }
}
