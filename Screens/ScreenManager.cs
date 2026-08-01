using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Utilities;
using System.ComponentModel;

namespace MonogameLibrary.Screens
{
    /// <summary>
    /// Class to manage all screens
    /// </summary>
    public class ScreenManager : Singleton<ScreenManager>
    {
        #region rMembers

        private List<Screen> _screens;
        private Dictionary<string, Screen> _screenTypes;

        public Screen CurrentScreen
        {
            get
            {
                if (_screens.Count > 0)
                {
                    return _screens.Last();
                }

                return null;
            }
        }

        #endregion rMembers






        #region rInitialise

        /// <summary>
        /// Constructor
        /// </summary>
        public ScreenManager()
        {
            _screens = new List<Screen>();
            _screenTypes = new Dictionary<string, Screen>();
        }


        /// <summary>
        /// Registers a screen to the screen manager but doesn't load it yet
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="screen"></param>
        public void RegisterScreen(string screenName, Screen screen)
        {
            _screenTypes.Add(screenName, screen);
        }


        /// <summary>
        /// Loads content for all registered screens
        /// </summary>
        /// <param name="content"></param>
        public void LoadScreens(ContentManager content)
        {
            foreach (Screen screen in _screenTypes.Values)
            {
                screen.LoadContent(content);
            }
        }

        #endregion rInitialise 





        #region Update

        /// <summary>
        /// Updates all screens
        /// </summary>
        /// <remarks>
        /// Updates active screen first and continues down through stack of screens
        /// </remarks>
        /// <param name="gameTime"></param>
        public void UpdateScreens(GameTime gameTime)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                Screen screen = _screens[i];

                if (screen.IsUpdateWhenInactive || screen == CurrentScreen)
                {
                    screen.Update(gameTime);
                }
            }
        }

        #endregion Update






        #region Draw

        /// <summary>
        /// Draws all screens
        /// </summary>
        /// <remarks>
        /// Draws active screen first and continues down through stack of screens
        /// </remarks>
        /// <param name="graphicsDevice"></param>
        /// <param name="spriteBatch"></param>
        public void DrawScreens(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Rectangle renderDestination)
        {
            // Draw each screen to it's own render target
            foreach (Screen screen in _screens)
            {
                graphicsDevice.SetRenderTarget(screen.RenderTarget);
                graphicsDevice.Clear(Color.Transparent);

                screen.Draw(spriteBatch);
            }

            // Reset draw target to back buffer
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Black);


            // Draw all screens to backbuffer
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (Screen screen in _screens)
            {
                if (screen.IsVisibleWhenInactive || screen == CurrentScreen)
                {
                    spriteBatch.Draw(screen.RenderTarget, renderDestination, Color.White);
                }
            }

            spriteBatch.End();
        }

        #endregion Draw





        #region rUtility

        /// <summary>
        /// Gets a screen by name
        /// </summary>
        /// <param name="screenName">Name of screen to find</param>
        /// <returns>Screen with specified name, null if screen does not exist</returns>
        public Screen GetScreen(string screenName)
        {
            if (_screenTypes.ContainsKey(screenName))
            {
                return _screenTypes[screenName];
            }

            return null;
        }


        /// <summary>
        /// Adds a new screen to the stack of screens
        /// </summary>
        /// <param name="screenName"></param>
        public void ActivateScreen(string screenName)
        {
            if (_screenTypes.ContainsKey(screenName))
            {
                Screen screen = _screenTypes[screenName];
                _screens.Add(screen);
                screen.OnActivate();
            }
        }


        /// <summary>
        /// Removes a screen from the stack of screens
        /// </summary>
        /// <param name="screenName"></param>
        public void DeactivateScreen(string screenName)
        {
            if (_screenTypes.ContainsKey(screenName))
            {
                Screen screen = _screenTypes[screenName];
                _screens.Remove(screen);
                screen.OnDeactivate();
            }
        }


        /// <summary>
        /// Closes the active screen and replaces it with a new one
        /// </summary>
        /// <param name="screenName"></param>
        public void ChangeScreen(string screenName)
        {
            if (_screenTypes.ContainsKey(screenName))
            {
                Screen screen = _screenTypes[screenName];

                // Add new screen
                _screens.Add(screen);
                screen.OnActivate();

                // Remove existing screen
                _screens.Remove(CurrentScreen);
                CurrentScreen.OnDeactivate();
            }
        }


        /// <summary>
        /// Removes all screens
        /// </summary>
        public void ClearScreens()
        {
            _screens.Clear();
        }

        #endregion rUtility
    }
}