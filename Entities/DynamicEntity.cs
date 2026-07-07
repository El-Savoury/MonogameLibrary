namespace MonogameLibrary.Entities
{
    /// <summary>
    /// An entity that can move and handle collisions
    /// </summary>
    /// 
    /// <remarks>
    /// Contains some basic movement and collision functions
    /// </remarks>
    public abstract class DynamicEntity : Entity
    {
        #region Members

        public Vector2 Velocity;

        #endregion Members





        #region Init

        public DynamicEntity() : base()
        {
            Velocity = Vector2.Zero;
        }


        public DynamicEntity(Vector2 position, Vector2 velocity) : base(position)
        {
            Velocity = velocity;
        }

        #endregion Init





        #region Movement

        /// <summary>
        /// Move to specified location in the alloted time
        /// </summary>
        /// <param name="target"></param>
        /// <param name="duration"></param>
        public void MoveTo(Vector2 target, TimeSpan duration)
        {
            // TODO : Implement move to logic here
        }

        #endregion Movement





        #region Collision
        /// <summary>
        /// React to a collision with another entity. Default is to do nothing.
        /// </summary>
        /// <param name="entity">Enitity to collide with</param>
        public virtual void OnCollideEntity(Entity entity)
        {
            // Do nothing 
        }


        /// <summary>
        /// React to a collision with an immovable object
        /// </summary>
        /// <remarks>
        /// The default is to push back out of collision
        /// </remarks>
        /// <param name="rect">Bounding rect of target object</param>
        public virtual void OnCollideSolid(Rectangle rect)
        {
            PushOutOfCollision(rect);
        }


        public void PushOutOfCollision(Rectangle rect)
        {
            float overlapX = Math.Min(Bounds.Right - rect.Left, rect.Right - Bounds.Left);
            float overlapY = Math.Min(Bounds.Bottom - rect.Top, rect.Bottom - Bounds.Top);

            if (overlapX < overlapY)
            {
                Position.X += Bounds.Center.X > rect.Center.X ? overlapX : -overlapX;
                Velocity.X = 0.0f;
            }
            else
            {
                Position.Y += Bounds.Center.Y > rect.Center.Y ? overlapY : -overlapY;
                Velocity.Y = 0.0f;
            }
        }


        ///// <summary>
        ///// React to being squished between solid objects
        ///// </summary>
        //public abstract void OnSquish();

        #endregion Collision
    }
}
