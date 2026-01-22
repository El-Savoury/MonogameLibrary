namespace MonogameLibrary.Utilities
{
    /// <summary>
    /// A generic thread safe singleton
    /// </summary>
    /// <typeparam name="TClass">Class to create as a singleton</typeparam>
    public abstract class Singleton<TClass> where TClass : class, new()
    {
        private static TClass instance = null;
        private static readonly object instanceLock = new object();

        /// <summary>
        /// Gets a single instance of the desired class
        /// </summary>
        public static TClass I
        {
            get
            {
                if (instance == null)
                {
                    // Create lock so each thread makes this check one after another
                    // if one thread has already created an instance the others will ignore
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new TClass();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Protection level keeps this hidden so can't be called anywhere else in the code
        /// </remarks>
        protected Singleton()
        {
        }
    }
}
