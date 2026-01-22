using MonogameLibrary.Maths;
using System.Diagnostics;

namespace MonogameLibrary.Entities
{
    /// <summary>
    /// An entity that can move and handle collisions
    /// </summary>
    public abstract class DynamicEntity : MovingEntity
    {
        #region Init

        public DynamicEntity() : base()
        {
        }


        public DynamicEntity(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
        }

        #endregion Init





        #region Collision

        /// <summary>
        /// React to a collision with an immovable object
        /// </summary>
        /// <remarks>
        /// The default is to push back out of collision
        /// </remarks>
        /// <param name="rect">Bounding rect of target object</param>
        public virtual void OnCollideSolid(RectF rect)
        {
            PushOutOfCollision(rect);
        }


        public void PushOutOfCollision(RectF rect)
        {
            float overlapX = Math.Min(Bounds.Right - rect.Left, rect.Right - Bounds.Left);
            float overlapY = Math.Min(Bounds.Bottom - rect.Top, rect.Bottom - Bounds.Top);

            if (overlapX < overlapY)
            {
                Position.X += Bounds.Centre.X > rect.Centre.X ? overlapX : -overlapX;
                Velocity.X = 0.0f;
            }
            else
            {
                Position.Y += Bounds.Centre.Y > rect.Centre.Y ? overlapY : -overlapY;
                Velocity.Y = 0.0f;
            }
        }

        #endregion Collision
    }
}
