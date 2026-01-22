using MonogameLibrary.Utilities;

namespace MonogameLibrary.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public KeyboardInput KeyboardInput { get; private set; }
        public MouseInput MouseInput { get; private set; }
        public GamePadInput[] GamePadInputs { get; private set; }


        public void Init()
        {
            KeyboardInput = new KeyboardInput();
            MouseInput = new MouseInput();

            GamePadInputs =
            [
                new GamePadInput(PlayerIndex.One),
                new GamePadInput(PlayerIndex.Two),
                new GamePadInput(PlayerIndex.Three),
                new GamePadInput(PlayerIndex.Four)
            ];
        }


        public void Update(GameTime gameTime)
        {
            KeyboardInput.Update();
            MouseInput.Update();

            foreach (GamePadInput gamePadInput in GamePadInputs)
            {
                gamePadInput.Update();
            }
        }
    }
}
