using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;

namespace MonogameLibrary.Assets
{
    public class AssetManager : Singleton<AssetManager>
    {
        ContentManager _content;

        private readonly Dictionary<string, TextureAtlas> _textureAtlases = [];
        private readonly Dictionary<string, SpriteFont> _fonts = [];


        public void Init(ContentManager content)
        {
            _content = content;
        }


        public T Load<T>(string filePath)
        {
            return _content.Load<T>(filePath);
        }

        public void AddTextureAtlas(string name, TextureAtlas atlas)
        {
            _textureAtlases.Add(name, atlas);
        }


        //public void AddTextureAtlas(string name, string filePath)
        //{
        //    _textureAtlases.Add(name, Load<TextureAtlas>(filePath));
        //}


        //public void AddTextureAtlas(string name, Texture2D texture)
        //{
        //    TextureAtlas atlas = new TextureAtlas(texture);
        //    _textureAtlases.Add(name, atlas);
        //}


        public TextureAtlas GetTextureAtlas(string name)
        {
            return _textureAtlases[name];
        }


        public void RemoveTextureAtlas(string name)
        {
            _textureAtlases.Remove(name);
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
