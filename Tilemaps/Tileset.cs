using MonogameLibrary.Assets;
using MonogameLibrary.Graphics;
using MonogameLibrary.Utilities;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A collection of tile templates used to load tiles into a tilemap
    /// </summary>
    public class Tileset
    {
        public string Name { get; }
        public TextureRegion TilesetTexture { get; set; }
        public Dictionary<int, TileTemplate> TileTemplates { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Padding { get; }
        public int Count => Columns * Rows;


        /// <summary>
        /// Create a blank tileset
        /// </summary>
        public Tileset()
        {
            TileTemplates = new Dictionary<int, TileTemplate>();
        }

        /// <summary>
        /// Create a new tileset
        /// </summary>
        /// <param name="name">The name of this tileset</param>
        /// <param name="texture">The texture region being used for this tileset</param>
        /// <param name="tileWidth">The width of a tile in this tileset</param>
        /// <param name="tileHeight">The height of a tile in this tileset</param>
        /// <param name="padding">The size in pixels of padding between tiles in the tileset texture</param>
        public Tileset(string name, TextureRegion texture, int tileWidth, int tileHeight, int padding = 0)
        {
            Name = name;
            TilesetTexture = texture;
            TileTemplates = new Dictionary<int, TileTemplate>();
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = texture.Width / TileWidth;
            Rows = texture.Height / TileHeight;
            Padding = padding;
        }


        /// <summary>
        /// Load a tileset from .xml file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Tileset FromFile(ContentManager content, string fileName)
        {
            Tileset tileset = new Tileset();

            string path = Path.Combine(content.RootDirectory, fileName);

            using (Stream stream = TitleContainer.OpenStream(path))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    // Load tileset texture region and add to texture atlas 
                    string atlasName = root.Element("Atlas").Value;
                    TextureAtlas atlas = AssetManager.I.GetTextureAtlas(atlasName);

                    XElement regionElement = root.Element("Region");
                    string name = regionElement.Attribute("name").Value;
                    int x = int.Parse(regionElement.Attribute("x")?.Value ?? "0");
                    int y = int.Parse(regionElement.Attribute("y")?.Value ?? "0");
                    int width = int.Parse(regionElement.Attribute("width")?.Value ?? atlas.Texture.Width.ToString());
                    int height = int.Parse(regionElement.Attribute("height")?.Value ?? atlas.Texture.Height.ToString());

                    if (!string.IsNullOrEmpty(name))
                    {
                        atlas.AddRegion(name, x, y, width, height);
                        tileset.TilesetTexture = atlas.GetRegion(name);
                    }

                    // Load all tile templates
                    var templates = root.Element("Templates")?.Elements("Template");

                    if (templates != null)
                    {
                        foreach (var template in templates)
                        {
                            int id = int.Parse(template.Attribute("tilesetID").Value);

                            // TODO: Add any other fields to parse here

                            int collision = int.Parse(template.Attribute("collision")?.Value ?? "0");
                            int rotation = int.Parse(template.Attribute("rotation")?.Value ?? "0");

                            tileset.AddTileTemplate(id, (TileCollision)collision, (CardinalDir)rotation);
                        }
                    }
                }
            }

            return tileset;
        }



        /// <summary>
        /// Add a new tile template to this tileset
        /// </summary>
        /// <param name="tilesetID"></param>
        /// <param name="collision"></param>
        public void AddTileTemplate(int tilesetID, TileCollision collision, CardinalDir rotation)
        {
            TileTemplate template = new TileTemplate(tilesetID, collision, rotation);
            TileTemplates.Add(tilesetID, template);
        }


        ///// <summary>
        ///// Get the texture of a tile in this tileset
        ///// </summary>
        ///// <param name="tilesetID">The tilest ID of the texture we want</param>
        ///// <returns>Texture region from tileset texture</returns>
        public TextureRegion GetTileTexture(int tilesetID)
        {
            int column = tilesetID % Columns * TileWidth;
            int row = tilesetID / Columns * TileHeight;

            return GetTileTexture(column, row);
        }


        /// <summary>
        /// Get the texture of a tile in this tileset
        /// </summary>
        /// <param name="column">Column of tileset that desired tile texture is in</param>
        /// <param name="row">Row of tileset that desired tile texture is in</param>
        /// <returns>Texture region from tileset texture</returns>
        public TextureRegion GetTileTexture(int column, int row)
        {
            int regionX = TilesetTexture.SourceRectangle.X + column * (TileWidth + Padding);
            int regionY = TilesetTexture.SourceRectangle.Y + row * (TileHeight + Padding);

            return new TextureRegion(Name, TilesetTexture.Texture, regionX, regionY, TileWidth, TileHeight);
        }
    }
}
