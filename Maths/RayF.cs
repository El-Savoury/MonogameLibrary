namespace MonogameLibrary.Maths
{
    public struct RayF
    {
        public Vector2 Origin;
        public Vector2 Direction;

        public RayF(Vector2 origin, Vector2 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
