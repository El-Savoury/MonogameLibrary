namespace MonogameLibrary.Utilities
{
    public class FontManager : Singleton<FontManager>
    {
        ContentManager _contentManager;
        private readonly Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();


        public void Init(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }


        public void AddFont(string fontName, string filePath)
        {
            _fonts.Add(fontName, _contentManager.Load<SpriteFont>(filePath));
        }

        public void AddFont(Enum fontEnum, string filePath)
        {
            _fonts.Add(fontEnum.ToString(), _contentManager.Load<SpriteFont>(filePath));
        }

        public SpriteFont GetFont(string fontName)
        {
            return _fonts[fontName];
        }

        public SpriteFont GetFont(Enum fontEnum)
        {
            return _fonts[fontEnum.ToString()];
        }
    }
}
