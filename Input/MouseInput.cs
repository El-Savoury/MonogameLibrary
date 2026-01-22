namespace MonogameLibrary.Input
{
    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        Button1,
        Button2
    }


    public class MouseInput
    {
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        /// <summary>
        /// Gets the cursor screen position
        /// </summary>
        public Point Position => currentMouseState.Position;

        /// <summary>
        /// Gets the difference in cursor position between current and previous frame
        /// </summary>
        public Point PositionDelta => currentMouseState.Position - previousMouseState.Position;

        /// <summary>
        /// Tracks if the cursor has moved since previous frame
        /// </summary>
        public bool Moved => PositionDelta != Point.Zero;


        public MouseInput()
        {
            currentMouseState = Mouse.GetState();
            previousMouseState = new MouseState();
        }


        public void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }


        public bool IsButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Button1:
                    return currentMouseState.XButton1 == ButtonState.Pressed;
                case MouseButton.Button2:
                    return currentMouseState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }


        public bool IsButtonUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Released;
                case MouseButton.Button1:
                    return currentMouseState.XButton1 == ButtonState.Released;
                case MouseButton.Button2:
                    return currentMouseState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }


        public bool IsButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
                case MouseButton.Button1:
                    return currentMouseState.XButton1 == ButtonState.Pressed && previousMouseState.XButton1 == ButtonState.Released;
                case MouseButton.Button2:
                    return currentMouseState.XButton2 == ButtonState.Pressed && previousMouseState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }


        public bool IsButtonReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Button1:
                    return currentMouseState.XButton1 == ButtonState.Released && previousMouseState.XButton1 == ButtonState.Pressed;
                case MouseButton.Button2:
                    return currentMouseState.XButton2 == ButtonState.Released && previousMouseState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }
    }
}
