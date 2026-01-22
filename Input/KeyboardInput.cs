namespace MonogameLibrary.Input
{
    /// <summary>
    /// Represents keyboard input state
    /// </summary>
    public class KeyboardInput
    {
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;


        public KeyboardInput()
        {
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = new KeyboardState();
        }


        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }


        public bool IsKeyDown(Keys key) => currentKeyboardState.IsKeyDown(key);

        public bool IsKeyUp(Keys key) => currentKeyboardState.IsKeyUp(key);


        /// <summary>
        /// Was specified key just pressed?
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if key pressed on current frame</returns>
        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }


        /// <summary>
        /// Was specified key just released?
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if key released on current frame</returns>
        public bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }


        /// <summary>
        /// Is the specified key held down?
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if key down in current and previous frame</returns>
        public bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }
    }
}
