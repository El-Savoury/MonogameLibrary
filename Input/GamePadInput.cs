namespace MonogameLibrary.Input
{
    public class GamePadInput
    {
        private GamePadState currentGamePadState;
        private GamePadState previousGamePadState;

        // Gets the index of which player is providing input
        public PlayerIndex PlayerIndex { get; }

        /// <summary>
        /// Tracks if gamepad is connected
        /// </summary>
        public bool IsConnected => currentGamePadState.IsConnected;

        /// <summary>
        /// Gets left thumbstick value
        /// </summary>
        public Vector2 LeftThumbstick => currentGamePadState.ThumbSticks.Left;

        /// <summary>
        /// Gets right thumbstick value
        /// </summary>
        public Vector2 RightThumbstick => currentGamePadState.ThumbSticks.Right;

        /// <summary>
        /// Gets left trigger value
        /// </summary>
        public float LeftTrigger => currentGamePadState.Triggers.Left;

        /// <summary>
        /// Gets right trigger value
        /// </summary>
        public float RightTrigger => currentGamePadState.Triggers.Right;



        public GamePadInput(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
            currentGamePadState = GamePad.GetState(playerIndex);
            previousGamePadState = new GamePadState();
        }


        public void Update()
        {
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex);
        }


        public bool IsButtonDown(Buttons button) => currentGamePadState.IsButtonDown(button);

        public bool IsButtonUp(Buttons button) => currentGamePadState.IsButtonUp(button);


        public bool IsButtonPressed(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button) && previousGamePadState.IsButtonUp(button);
        }

        public bool IsButtonReleased(Buttons button)
        {
            return currentGamePadState.IsButtonUp(button) && previousGamePadState.IsButtonDown(button);
        }
    }
}
