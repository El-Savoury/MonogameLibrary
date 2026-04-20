namespace MonogameLibrary.Utilities
{
    public class Timer
    {
        public bool IsRunning { get; private set; }
        public double ElapsedTime { get; set; }


        public Timer()
        {
            ElapsedTime = 0.0d;
            IsRunning = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning)
            {
                ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }


        public void Start()
        {
            IsRunning = true;
        }


        public void Stop()
        {
            IsRunning = false;
        }


        public void Reset()
        {
            ElapsedTime = 0.0f;
        }


        public void StopReset()
        {
            IsRunning = false;
            ElapsedTime = 0.0d;
        }
    }
}
