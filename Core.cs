using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Assets;
using MonogameLibrary.Input;
using MonogameLibrary.Screens;

namespace MonogameLibrary
{
    /// <summary>
    /// Entry point into the program
    /// </summary>
    public class Core : Game
    {
        internal static Core _self;

        public static SpriteBatch SpriteBatch { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static new GraphicsDevice GraphicsDevice { get; private set; }
        public static new ContentManager Content { get; private set; }


        /// <summary>
        /// Create a core instance
        /// </summary>
        /// <param name="title">Game window title</param>
        /// <param name="windowWidth">Default game window width</param>
        /// <param name="windowHeight">Default game window height</param>
        /// <param name="isFullscreen">Determines if game window opens in fullscreen</param>
        public Core(string title, int windowWidth, int windowHeight, bool isFullscreen)
        {
            // Ensure that multiple cores are not created
            if (_self != null)
            {
                throw new InvalidOperationException($"Only a single Core instance can be created");
            }

            // Store reference to self for global member access
            _self = this;

            Graphics = new GraphicsDeviceManager(this);

            // Set graphics defaults 
            Graphics.PreferredBackBufferWidth = windowWidth;
            Graphics.PreferredBackBufferHeight = windowHeight;
            Graphics.IsFullScreen = isFullscreen;
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
            AssetManager.I.Init(Content);

            GraphicsDevice = base.GraphicsDevice;
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            InputManager.I.Update(gameTime);

            base.Update(gameTime);
        }


        protected virtual Rectangle CalcRenderDestination(int renderTargetWidth, int renderTargetHeight)
        {
            Rectangle destinationRect = new Rectangle();

            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;

            float scaleX = windowWidth / renderTargetWidth;
            float scaleY = windowHeight / renderTargetHeight;
            float scale = Math.Max(scaleX, scaleY);

            destinationRect.Width = (int)(renderTargetWidth * scale);
            destinationRect.Height = (int)(renderTargetHeight * scale);
            if (destinationRect.Width < renderTargetWidth) destinationRect.Width = renderTargetWidth;
            if (destinationRect.Height < renderTargetHeight) destinationRect.Height = renderTargetHeight;

            destinationRect.X = (windowWidth - destinationRect.Width) / 2;
            destinationRect.Y = (windowHeight - destinationRect.Height) / 2;

            return destinationRect;
        }
    }
}
