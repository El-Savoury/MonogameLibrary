using MonogameLibrary.Input;
using MonogameLibrary.Utilities;

namespace MonogameLibrary
{
    /// <summary>
    /// Entry point into the program
    /// </summary>
    /// <remarks>
    /// Contains a set of basic game window utilities
    /// </remarks>
    public class Core : Game
    {
        internal static Core self;

        public static SpriteBatch SpriteBatch { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static new GraphicsDevice GraphicsDevice { get; private set; }
        public static new ContentManager Content { get; private set; }


        /// <summary>
        /// Create a core instance
        /// </summary>
        /// <param name="title">Game window title</param>
        /// <param name="width">Default game window width</param>
        /// <param name="height">Default game window height</param>
        /// <param name="fullscreen">Determines if game window opens in fullscreen</param>


        public Core(string title, int width, int height, bool fullscreen)
        {
            // Ensure that multiple cores are not created
            if (self != null)
            {
                throw new InvalidOperationException($"Only a single Core instance can be created");
            }

            // Store reference to self for global member access
            self = this;

            Graphics = new GraphicsDeviceManager(this);

            // Set graphics defaults 
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.IsFullScreen = fullscreen;
            Graphics.ApplyChanges();

            // Set window info
            Window.Title = title;
            IsMouseVisible = true;

            Content = base.Content;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            InputManager.I.Init();
            FontManager.I.Init(Content);

            base.Initialize();

            GraphicsDevice = base.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }



        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            InputManager.I.Update(gameTime);
        }
    }
}
