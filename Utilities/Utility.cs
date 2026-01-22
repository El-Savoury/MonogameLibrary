namespace MonogameLibrary.Utilities
{
    public class Utility : Singleton<Utility>   
    {
        /// <summary>
        /// Get delta time from gameTime object
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        /// <returns>Time between last frame and current frame in seconds</returns>
        public float DeltaTime(GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }


        /// <summary>
        /// Swap two numbers using a tuple
        /// </summary>
        public void SwapNums(ref float x, ref float y)
        {
            (x, y) = (y, x);
        }
    }
}
