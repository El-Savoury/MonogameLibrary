using System;
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

        private readonly Dictionary<string, TextureRegion> _regions = [];

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

        #endregion Init





        #region Utility

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
            TextureRegion region = new TextureRegion(Texture, x, y, width, height);
            _regions.Add(name, region);
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


        ///// <summary>
        ///// Add multiple regions
        ///// </summary>
        ///// <remarks>
        ///// Assumes all regions have the specified width and height.
        ///// Regions are added left to right, top to bottom.
        ///// Each region name is appended by it's index i.e. 'name0', 'name1' etc.
        ///// </remarks>
        ///// <param name="name">The name for these regions</param>
        ///// <param name="startX">The x index of first region to add</param>
        ///// <param name="startY">The y index of first region</param>
        ///// <param name="regionWidth">Region width in pixels</param>
        ///// <param name="regionHeight">Region height in pixels</param>
        ///// <param name="regionsToAdd">The total number of regions to add</param>
        //public void AddRegions(string name, int startX, int startY, int regionWidth, int regionHeight, int regionsToAdd)
        //{
        //    int atlasColumns = Texture.Width / regionWidth;
        //    int atlasRows = Texture.Height / regionHeight;

        //    for (int i = 0; i < regionsToAdd; i++)
        //    {
        //        int xIndex = startX + i;
        //        int yIndex = startY + i;

        //        if (yIndex > atlasRows) { break; }

        //        if (xIndex > atlasColumns)
        //        {
        //            xIndex = 0;
        //            yIndex++;
        //        }

        //        TextureRegion region = new TextureRegion(Texture, xIndex * regionWidth, yIndex * regionHeight, regionWidth, regionHeight);
        //        string regionName = name + i;

        //        _regions.Add(regionName, region);
        //    }
        //}


        public bool RemoveRegion(string name)
        {
            return _regions.Remove(name);
        }

        public bool RemoveRegion(Enum regionEnum)
        {
            return _regions.Remove(regionEnum.ToString());
        }


        public TextureRegion GetRegion(string name)
        {
            return _regions[name];
        }

        public TextureRegion GetRegion(Enum regionEnum)
        {
            return _regions[regionEnum.ToString()];
        }


        public void Clear()
        {
            _regions.Clear();
        }

        #endregion Utility
    }
}