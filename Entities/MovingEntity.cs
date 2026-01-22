namespace MonogameLibrary.Entities
{
    /// <summary>
    /// An object that can move!!!
    /// </summary>
    public abstract class MovingEntity : Entity
    {
        public Vector2 Velocity;


        public MovingEntity() : base()
        {
            Velocity = Vector2.Zero;
        }

        public MovingEntity(Vector2 position, Vector2 velocity) : base(position)
        {
            Velocity = velocity;
        }
    }
}
