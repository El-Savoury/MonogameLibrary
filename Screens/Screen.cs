namespace MonogameLibrary.Screens
{
    /// <summary>
    /// Designated area for drawing game elements
    /// </summary>
    public abstract class Screen
    {
        #region rMembers

        protected RenderTarget2D _renderTarget;

        public int Width { get; }
        public int Height { get; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public bool IsUpdateWhenInactive { get; set; }
        public bool IsVisibleWhenInactive { get; set; }

        #endregion rMembers






        #region rInit

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Screen(GraphicsDeviceManager graphics, int width, int height)
        {
            Width = width;
            Height = height;
            _renderTarget = new RenderTarget2D(graphics.GraphicsDevice, width, height);
        }


        /// <summary>
        /// Load content for this screen
        /// </summary>
        public virtual void LoadContent(ContentManager content) { }


        /// <summary>
        /// Called when the screen is activated
        /// </summary>
        public virtual void OnActivate() { }


        /// <summary>
        /// Called when the screen is deactivated
        /// </summary>
        public virtual void OnDeactivate() { }

        #endregion rInit





        #region rUpdate

        /// <summary>
        /// Update the screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public abstract void Update(GameTime gameTime);

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="spriteBatch"></param>
        /// <returns></returns>
        public abstract RenderTarget2D DrawToRenderTarget(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);

        #endregion rDraw          
    }
}