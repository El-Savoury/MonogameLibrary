using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Assets
{
    public class AssetManager : Singleton<AssetManager>
    {
        ContentManager _content;

        private readonly Dictionary<string, TextureAtlas> _spritesheets = [];
        private readonly Dictionary<string, SpriteFont> _fonts = [];


        public void Init(ContentManager content)
        {
            _content = content;
        }

        public T Load<T>(string filePath)
        {
            return _content.Load<T>(filePath);
        }


        public void AddSpritesheet(string name, string filePath)
        {
            _spritesheets.Add(name, Load<TextureAtlas>(filePath));
        }


        public TextureAtlas GetSpritesheet(string name)
        {
            return _spritesheets[name];
        }


        public void RemoveSpritesheet(string name)
        {
            _spritesheets.Remove(name);
        }


        public void AddFont(string name, string filePath)
        {
            _fonts.Add(name, Load<SpriteFont>(filePath));
        }


        public SpriteFont GetFont(string name)
        {
            return _fonts[name];
        }


        public void RemoveFont(string name)
        {
            _fonts.Remove(name);
        }
    }
}
