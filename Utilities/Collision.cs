using MonogameLibrary.Maths;

namespace MonogameLibrary.Utilities
{
    /// <summary>
    /// Results of a ray collision
    /// </summary>
    public struct CollisionResult()
    {
        public float t = 1.0f;
        public Vector2 collisionPoint;
        public Vector2 normal;

        public RectF expandRect;

        public Vector2 tnear;
        public Vector2 tfar;

        //public bool isColliding
        //{
        //    get
        //    {
        //        return t.HasValue && t <= 1.0f;
        //    }
        //}

        public static CollisionResult None
        {
            get
            {
                CollisionResult none = new CollisionResult();

                none.collisionPoint = Vector2.Zero;
                none.normal = Vector2.Zero;
                none.normal = Vector2.Zero;
                none.t = 1.0f;

                return none;
            }
        }
    }


    public class Collision : Singleton<Collision>
    {
        // Epsilon to avoid division by zero
        private const float EPSILON = 0.0001f;




        //public bool SweptAABB(RectF dynamicRect, Vector2 velocity, RectF targetRect)
        //{
        //    // Do broad phase collision check
        //    RectF broadPhase = BroadPhaseRect(dynamicRect, velocity);
        //    if (!RectFVsRectF(broadPhase, targetRect)) { return false; }

        //    Vector2 tNear;
        //    Vector2 tFar;

        //    // Get near and far collision distances 
        //    if (velocity.X > 0)
        //    {
        //        tNear.X = targetRect.Left - dynamicRect.Right;
        //        tFar.X = targetRect.Right - dynamicRect.Left;
        //    }
        //    else
        //    {
        //        tNear.X = targetRect.Right - dynamicRect.Left;
        //        tFar.X = targetRect.Left - dynamicRect.Right;
        //    }

        //    if (velocity.Y > 0)
        //    {
        //        tNear.Y = targetRect.Top - dynamicRect.Bottom;
        //        tFar.Y = targetRect.Bottom - dynamicRect.Top;
        //    }
        //    else
        //    {
        //        tNear.Y = targetRect.Bottom - dynamicRect.Top;
        //        tFar.Y = targetRect.Top - dynamicRect.Bottom;
        //    }

        //    // Find collision times for each axis
        //    // Checking against epsilon value avoids floating point precision errors
        //    // Infinity values prevent possible divide-by-zero error 
        //    Vector2 tHitNear;
        //    Vector2 tHitFar;

        //    if (velocity.X == 0)
        //    {
        //        tHitNear.X = float.NegativeInfinity;
        //        tHitFar.X = float.PositiveInfinity;
        //    }
        //    else
        //    {
        //        tHitNear.X = tNear.X / velocity.X;
        //        tHitFar.X = tFar.X / velocity.X;
        //    }

        //    if (velocity.Y == 0)
        //    {
        //        tHitNear.Y = float.NegativeInfinity;
        //        tHitFar.Y = float.PositiveInfinity;
        //    }
        //    else
        //    {
        //        tHitNear.Y = tNear.Y / velocity.Y;
        //        tHitFar.Y = tFar.Y / velocity.Y;
        //    }

        //    // Find earliest/latest collision times
        //    float entryTime = Math.Max(tHitNear.X, tHitNear.Y);
        //    float exitTime = Math.Min(tHitFar.X, tHitFar.Y);

        //    // If no collision
        //    if (entryTime > exitTime ||
        //        tHitNear.X < 0.0f && tHitNear.Y < 0.0f ||
        //        tHitNear.X > 1.0f && tHitNear.Y > 1.0f)
        //    {
        //        return false;
        //    }

        //    return true;
        //}


        //public RectF BroadPhaseRect(RectF dynamicRect, Vector2 velocity)
        //{
        //    float x = velocity.X > 0 ? dynamicRect.X : dynamicRect.X + velocity.X;
        //    float y = velocity.Y > 0 ? dynamicRect.Y : dynamicRect.Y + velocity.Y;
        //    float width = velocity.X > 0 ? dynamicRect.Width + velocity.X : dynamicRect.Width - velocity.X;
        //    float height = velocity.Y > 0 ? dynamicRect.Height + velocity.Y : dynamicRect.Height - velocity.Y;

        //    return new RectF(x, y, width, height);
        //}


        private Vector2 CalculateNormal(RayF ray, float nearX, float nearY)
        {
            Vector2 normal = Vector2.Zero;

            if (nearX > nearY)
            {
                int xDir = ray.Direction.X > 0 ? -1 : 1;
                normal = new Vector2(xDir, 0);
            }
            else if (nearX < nearY)
            {
                int yDir = ray.Direction.Y > 0 ? -1 : 1;
                normal = new Vector2(0, yDir);
            }

            return normal;
        }


        public bool PointVsRect(Point point, Rectangle rect)
        {
            return point.X < rect.Right &&
                point.X > rect.Left &&
                point.Y > rect.Top &&
                point.Y < rect.Bottom;
        }


        public bool PointVsRect(Vector2 point, Rectangle rect)
        {
            return point.X < rect.Right &&
                point.X > rect.Left &&
                point.Y > rect.Top &&
                point.Y < rect.Bottom;
        }

        /// <summary>
        /// Checks if two recrangles are overlapping
        /// </summary>
        /// <remarks>
        /// Non-inclusive of rectangle edges
        /// </remarks>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True if rectangles overlap</returns>
        public bool RectVsRect(Rectangle a, Rectangle b)
        {
            return a.Left < b.Right &&
                    a.Right > b.Left &&
                    a.Top < b.Bottom &&
                    a.Bottom > b.Top;
        }


        /// <summary>
        /// Checks if two recrangles are overlapping
        /// </summary>
        /// <remarks>
        /// Non-inclusive of rectangle edges
        /// </remarks>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True if rectangles overlap</returns>
        public bool RectVsRect(RectF a, RectF b)
        {
            return a.Left < b.Right &&
                    a.Right >b.Left &&
                    a.Top < b.Bottom &&
                    a.Bottom > b.Top;
        }
    }
}
