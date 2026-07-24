using MonogameLibrary.Entities;
using MonogameLibrary.Utilities;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;

namespace MonogameLibrary.Tilemaps
{
    /// <summary>
    /// A 2D grid of tiles. 
    /// </summary>
    /// 
    /// <remarks>
    /// Can be a single tile layer or multiple stacked layers.
    /// </remarks>
    public class Tilemap
    {
        #region Properties

        public Tileset Tileset { get; }
        public Dictionary<string, TilemapLayer> TilemapLayers { get; }

        public Vector2 Position { get; set; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Columns { get; }
        public int Rows { get; }
        public int Count => Columns * Rows;
        public int WidthInPixels => Columns * TileWidth;
        public int HeightInPixels => Rows * TileHeight;


        #endregion Properties





        #region Init

        /// <summary>
        /// Create an empty tilemap
        /// </summary>
        public Tilemap()
        {
            TilemapLayers = new Dictionary<string, TilemapLayer>();
        }


        /// <summary>
        /// Create a tile map with specified position and size
        /// </summary>
        /// <param name="tileset">The tileset we want to use in this tilemap</param>
        /// <param name="position">The top left position of the tilemap</param>
        /// <param name="columns">The number of columns of tiles in the tilemap</param>
        /// <param name="rows">The number of rows of tiles in the tilemap</param>
        public Tilemap(Tileset tileset, Vector2 position, int columns, int rows)
        {
            Tileset = tileset;
            Position = position;
            TileWidth = tileset.TileWidth;
            TileHeight = tileset.TileHeight;
            Columns = columns;
            Rows = rows;

            TilemapLayers = new Dictionary<string, TilemapLayer>();
        }


        /// <summary>
        /// Load tiles from an xml file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void LoadLevelFromFile(ContentManager content, string fileName)
        {
            string path = Path.Combine(content.RootDirectory, fileName);

            using (Stream stream = TitleContainer.OpenStream(path))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument doc = XDocument.Load(reader);
                    XElement root = doc.Root;

                    // Set layer to add tiles to
                    string layer = root.Element("Layer")?.Value ?? "defaultLayer";

                    // Parse tile rows into array of strings
                    XElement tilesElement = root.Element("Tiles");
                    string[] tileRows = tilesElement.Value.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    for (int y = 0; y < Rows; y++)
                    {
                        string[] tileColumns = tileRows[y].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        for (int x = 0; x < Columns; x++)
                        {
                            int id = int.Parse(tileColumns[x]);
                            SetTile(layer, id, x, y);
                        }
                    }
                }
            }
        }

        #endregion Init






        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (TilemapLayer layer in TilemapLayers.Values)
            {
                layer.Update(gameTime);
            }
        }

        #endregion Update






        #region Draw

        /// <summary>
        /// Draw all tilemap layers
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TilemapLayer layer in TilemapLayers.Values)
            {
                layer.Draw(spriteBatch);
            }
        }

        #endregion Draw






        #region Layers

        ///// <summary>
        ///// Add a new layer to the tilemap
        ///// </summary>
        ///// <param name="name"></param>
        ///// <exception cref="ArgumentException"></exception>
        //public void AddLayer(string name)
        //{
        //    if (TilemapLayers.ContainsKey(name))
        //    {
        //        throw new ArgumentException($"Tilemap layer with name {name} already exists");
        //    }

        //    TilemapTileLayer layer = new TilemapTileLayer(name, Position, TileWidth, TileHeight, Columns, Rows, Tileset);
        //    TilemapLayers.Add(name, layer);
        //}




        public void AddLayer<T>(string name) where T : TilemapLayer
        {
            if (TilemapLayers.ContainsKey(name))
            {
                throw new ArgumentException($"Tilemap layer with name {name} already exists");
            }

            // TODO: Bit on the jank side maybe improve this at some point
            Type type = typeof(T);

            if (type == typeof(TilemapTileLayer))
            {
                TilemapTileLayer layer = new TilemapTileLayer(name, Position, TileWidth, TileHeight, Columns, Rows, Tileset);
                TilemapLayers.Add(name, layer);
            }
            else if (type == typeof(TilemapEntityLayer))
            {
                TilemapEntityLayer layer = new TilemapEntityLayer(name, Position);
                TilemapLayers.Add(name, layer);
            }
        }


        ///// <summary>
        ///// Add a new layer to the tilemap
        ///// </summary>
        ///// <param name="layerEnum">Layer name as defined in an enum of layers</param>
        //public void AddLayer(Enum layerEnum)
        //{
        //    AddLayer(layerEnum.ToString());
        //}


        /// <summary>
        /// Remove a layer from the tilemap
        /// </summary>
        /// <param name="name">Name of layer to remove</param>
        /// <returns>True if layer is successfully removed</returns>
        public bool RemoveLayer(string name)
        {
            return TilemapLayers.Remove(name);
        }


        /// <summary>
        /// Remove a layer from the tilemap
        /// </summary>
        /// <param name="layerEnum">Name of layer as defined in enum of layers</param>
        /// <returns>True if layer is successfully removed</returns>
        public bool RemoveLayer(Enum layerEnum)
        {
            return RemoveLayer(layerEnum.ToString());
        }


        /// <summary>
        /// Get layer with specified name
        /// </summary>
        /// <param name="name">Name of layer</param>
        /// <returns>Layer of specified name if one exists</returns>
        public TilemapLayer GetLayer(string name)
        {
            return TilemapLayers[name];
        }


        /// <summary>
        /// Get layer with specified enum name
        /// </summary>
        /// <param name="layerEnum">Name of layer as defined by enum of layers</param>
        /// <returns>Layer of specified enum name</returns>
        public TilemapLayer GetLayer(Enum layerEnum)
        {
            return GetLayer(layerEnum.ToString());
        }


        public T GetLayer<T>(string name) where T : TilemapLayer
        {
            if (TilemapLayers.TryGetValue(name, out TilemapLayer layer))
            {
                return layer as T;
            }

            return null;
        }

        #endregion Layers





        #region Tiles

        public TileTemplate GetTileTemplate(int tilesetID)
        {
            return Tileset.TileTemplates[tilesetID];
        }


        public TileTemplate GetTileTemplate(int column, int row, string layer)
        {
            Tile tile = GetTile(column, row, layer);
            return Tileset.TileTemplates[tile.TilesetID];
        }


        /// <summary>
        /// Get the tile at the specified index
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Tile GetTile(int column, int row, string layer)
        {
            return GetLayer<TilemapTileLayer>(layer).GetTile(column, row);
        }


        /// <summary>
        /// Get the tile at the specified index 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Tile GetTile(Point index, string layer)
        {
            return GetLayer<TilemapTileLayer>(layer).GetTile(index.X, index.Y);
        }


        /// <summary>
        /// Get the tile at the specified world position
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Tile GetTile(Vector2 worldPos, string layer)
        {
            return GetLayer<TilemapTileLayer>(layer).GetTile(worldPos);
        }


        /// <summary>
        /// Set the tile at the specified index
        /// </summary>
        /// <param name="tilemapLayer"></param>
        /// <param name="tile"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void SetTile(Enum layerEnum, int tilesetID, int row, int column)
        {
            GetLayer<TilemapTileLayer>(layerEnum.ToString()).SetTile(row, column, tilesetID);
        }


        /// <summary>
        /// Set the tile at the specified index
        /// </summary>
        /// <param name="tilemapLayer"></param>
        /// <param name="tilesetID"></param>
        /// <param name="column"></param>
        /// <param name="column"></param>
        public void SetTile(string layer, int tilesetID, int column, int row)
        {
            GetLayer<TilemapTileLayer>(layer).SetTile(column, row, tilesetID);
        }

        #endregion Tiles






        #region TileEntities

        public TileEntity GetTileEntity(Point index, string layer)
        {
            TilemapEntityLayer entLayer = GetLayer<TilemapEntityLayer>(layer);
            return entLayer.GetEntity(index);
        }


        public TileEntity GetTileEntity(int xIndex, int yIndex, string layer)
        {
            return GetLayer<TilemapEntityLayer>(layer).GetEntity(xIndex, yIndex);
        }

        #endregion TileEntities





        #region Util

        /// <summary>
        /// Get the world position of a tile from it's index in the tilemap
        /// </summary>
        /// <param name="x">Tile index on the x-axis of tilemap</param>
        /// <param name="y">Tile index on the y-axis of tilemap</param>
        /// <returns>World position of tiles top left corner</returns>
        public Vector2 IndexToWorldPos(int x, int y)
        {
            return new Vector2(Position.X + x * TileWidth, Position.Y + y * TileHeight);
        }


        /// <summary>
        /// Converts a tiles world position into it's index in the tilemap
        /// </summary>
        /// <param name="worldPos">World position of tile top left corner</param>
        /// <returns>Tilemap index of tile at this position</returns>
        public Point WorldPosToIndex(Vector2 worldPos)
        {
            int xPosOffset = (int)(worldPos.X - Position.X);
            int YPosOffset = (int)(worldPos.Y - Position.Y);

            return new Point(xPosOffset / TileWidth, YPosOffset / TileWidth);
        }

        #endregion Util
    }
}
