using Microsoft.Xna.Framework;
using System;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Linq;

namespace MonogameLibrary.Graphics
{
    /// <summary>
    /// Represents a collection of texture regions taken from a single base texture
    /// </summary>
    public class TextureAtlas
    {
        #region Members

        public Texture2D Texture { get; set; }

        private readonly List<TextureRegion> _regionsByIndex = [];
        private readonly Dictionary<string, TextureRegion> _regionsByName = [];

        #endregion Members





        #region Init

        public TextureAtlas()
        {
        }

        public TextureAtlas(Texture2D texture)
        {
            Texture = texture;
        }


        /// <summary>
        /// Load texture atlas from an XML configuration file
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        /// <param name="fileName">Name of XML file from the content directory</param>
        /// <returns>The texture atlas defined in the XML file</returns>
        public static TextureAtlas FromFile(ContentManager content, string fileName)
        {
            TextureAtlas atlas = new TextureAtlas();
            string filePath = Path.Combine(content.RootDirectory, fileName + ".xml");

            using Stream stream = TitleContainer.OpenStream(filePath);
            {
                using XmlReader reader = XmlReader.Create(stream);
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    // Load texture from file
                    string texturePath = root.Element("Texture").Value;
                    atlas.Texture = content.Load<Texture2D>(texturePath);

                    // Get all region elements 
                    var regions = root.Element("Regions")?.Elements("Region");

                    if (regions != null)
                    {
                        // Load all regions
                        foreach (var region in regions)
                        {
                            string name = region.Attribute("name").Value;
                            int x = int.Parse(region.Attribute("x").Value);
                            int y = int.Parse(region.Attribute("y").Value);
                            int width = int.Parse(region.Attribute("width").Value);
                            int height = int.Parse(region.Attribute("height").Value);

                            if (!string.IsNullOrEmpty(name))
                            {
                                atlas.AddRegion(name, x, y, width, height);
                            }
                        }
                    }
                    return atlas;
                }
            }
        }


        /// <summary>
        /// Create a texture atlas where all regions are the same size
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        /// <param name="regionWidth"></param>
        /// <param name="regionHeight"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static TextureAtlas FromGrid(string name, Texture2D texture, int regionWidth, int regionHeight, int padding = 0)
        {
            TextureAtlas atlas = new TextureAtlas(texture);

            int columns = texture.Width / regionWidth;
            int rows = texture.Height / regionHeight;

            int count = columns * rows;

            for (int i = 0; i < count; i++)
            {
                int x = i % columns * regionHeight;
                int y = i / columns * regionHeight;

                int regionX = x + padding;
                int regionY = y + padding;

                string regionName = "name" + i;

                TextureRegion region = new TextureRegion(name, texture, regionX, regionY, regionWidth, regionHeight);
                atlas.AddRegion(regionName, region);
            }

            return atlas;
        }

        #endregion Init





        #region Utility

        private void AddRegion(string name, TextureRegion region)
        {
            _regionsByName.Add(name, region);
            _regionsByIndex.Add(region);
        }


        /// <summary>
        /// Add a region to the texture atlas
        /// </summary>
        /// <param name="name">Name of region</param>
        /// <param name="x">Top left xpos of region</param>
        /// <param name="y">Top left yPos of region</param>
        /// <param name="width">Width of region in pixels</param>
        /// <param name="height">Height of region in pixels</param>
        public void AddRegion(string name, int x, int y, int width, int height)
        {
            TextureRegion region = new TextureRegion(name, Texture, x, y, width, height);
            AddRegion(name, region);
        }


        /// <summary>
        /// Add a region to the texture atlas
        /// </summary>
        /// <param name="regionEnum">Name of region as defined in an enum of regions</param>
        /// <param name="x">Top left xpos of region</param>
        /// <param name="y">Top left yPos of region</param>
        /// <param name="width">Width of region in pixels</param>
        /// <param name="height">Height of region in pixels</param>
        public void AddRegion(Enum regionEnum, int x, int y, int width, int height)
        {
            AddRegion(regionEnum.ToString(), x, y, width, height);
        }


        public bool RemoveRegion(string name)
        {
            return _regionsByName.Remove(name);
        }


        public bool RemoveRegion(Enum regionEnum)
        {
            return _regionsByName.Remove(regionEnum.ToString());
        }


        public TextureRegion GetRegion(string name)
        {
            return _regionsByName[name];
        }


        public TextureRegion GetRegion(Enum regionEnum)
        {
            return _regionsByName[regionEnum.ToString()];
        }


        public TextureRegion GetRegion(int index)
        {
            return _regionsByIndex[index];
        }


        public void Clear()
        {
            _regionsByName.Clear();
        }


        public int GetRegionIndex(string name)
        {
            for (int i = 0; i < _regionsByIndex.Count; i++)
            {
                if (_regionsByIndex[i].Name == name) { return i; }
            }

            return -1;
        }

        #endregion Utility
    }
}