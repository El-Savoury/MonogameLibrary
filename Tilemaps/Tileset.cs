using MonogameLibrary.Assets;
using MonogameLibrary.Graphics;
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

                    XElement region = root.Element("Region");
                    string name = root.Element("name").Value;
                    int x = int.Parse(region.Attribute("x")?.Value);
                    int y = int.Parse(region.Attribute("y")?.Value);
                    int width = int.Parse(region.Attribute("width")?.Value);
                    int height = int.Parse(region.Attribute("height")?.Value);

                    atlas.AddRegion(name, x, y, width, height);
                    tileset.TilesetTexture = atlas.GetRegion(name);

                    // Load all tile templates
                    var templates = root.Element("Templates")?.Elements("Template");

                    if (templates != null)
                    {
                        foreach (var template in templates)
                        {
                            int id = int.Parse(template.Attribute("id").Value);

                            // TODO: Add other fields to parse here

                            // Define tile collision type
                            string collisionPath = template.Attribute("collision").Value;
                            TileCollision collision = TileCollision.None;

                            switch (collisionPath)
                            {
                                case "none":
                                    collision = TileCollision.None;
                                    break;
                                case "solid":
                                    collision = TileCollision.Solid;
                                    break;
                                case "passable":
                                    collision = TileCollision.Passable;
                                    break;
                                case "oneWay":
                                    collision = TileCollision.OneWay;
                                    break;
                            }

                            tileset.AddTileTemplate(id, collision);
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
        public void AddTileTemplate(int tilesetID, TileCollision collision)
        {
            TileTemplate template = new TileTemplate(tilesetID, collision);
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
