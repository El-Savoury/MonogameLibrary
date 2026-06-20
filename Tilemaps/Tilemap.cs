namespace MonogameLibrary.Tilemaps
{
    public class Tilemap
    {
        #region Properties

        public Tileset Tileset { get; }
        public Dictionary<string, TilemapLayer> Layers { get; }
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
            Layers = new Dictionary<string, TilemapLayer>();
        }


        /// <summary>
        /// Create a tile map of specified position and size
        /// </summary>
        /// <param name="tileset">The tileset we want to use in this tilemap</param>
        /// <param name="position">The top left position of the tilemap</param>
        /// <param name="tileWidth">The width in pixels of a tile in this tilemap</param>
        /// <param name="tileHeight">The height in pixels of a tile in this tilemap</param>
        /// <param name="columns">The number of columns of tiles in the tilemap</param>
        /// <param name="rows"The number of rows of tiles in the tilemap</param>
        public Tilemap(Tileset tileset, Vector2 position, int tileWidth, int tileHeight, int columns, int rows)
        {
            Tileset = tileset;
            Position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = columns;
            Rows = rows;

            Layers = new Dictionary<string, TilemapLayer>();
        }

        #endregion Init






        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (TilemapLayer layer in Layers.Values)
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
            foreach (TilemapLayer layer in Layers.Values)
            {
                layer.Draw(spriteBatch);
            }
        }

        #endregion Draw






        #region Layers

        /// <summary>
        /// Add a new layer to the tilemap
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddLayer(string name)
        {
            if (Layers.ContainsKey(name))
            {
                throw new ArgumentException($"Tilemap layer with name {name} already exists");
            }

            TilemapLayer layer = new TilemapLayer(name, Position, TileWidth, TileHeight, Columns, Rows, Tileset);
            Layers.Add(name, layer);
        }


        /// <summary>
        /// Add a new layer to the tilemap
        /// </summary>
        /// <param name="layerEnum">Layer name as defined in an enum of layers</param>
        public void AddLayer(Enum layerEnum)
        {
            AddLayer(layerEnum.ToString());
        }


        /// <summary>
        /// Remove a layer from the tilemap
        /// </summary>
        /// <param name="name">Name of layer to remove</param>
        /// <returns>True if layer is successfully removed</returns>
        public bool RemoveLayer(string name)
        {
            return Layers.Remove(name);
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
            return Layers[name];
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

        #endregion Layers





        #region Tiles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Tile GetTile(int column, int row, string layer)
        {
            return Layers[layer].GetTile(column, row);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Tile GetTile(Point index, string layer)
        {
            return Layers[layer].GetTile(index.X, index.Y);
        }


        /// <summary>
        /// Get the tile at the specified world position
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Tile GetTile(Vector2 worldPos, string layer)
        {
            return Layers[layer].GetTile(worldPos);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tilemapLayer"></param>
        /// <param name="tile"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void SetTile(Enum tilemapLayer, Tile tile, int row, int column)
        {
            Layers[tilemapLayer.ToString()].SetTile(row, column, tile);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tilemapLayer"></param>
        /// <param name="tile"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void SetTile(string tilemapLayer, Tile tile, int row, int column)
        {
            Layers[tilemapLayer].SetTile(row, column, tile);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public TileTemplate GetTileInfo(Point index, string layer)
        {
            ushort tileID = GetTile(index, layer).TilesetID;
            return Tileset.GetTileInfo(tileID);
        }

        #endregion Tiles






        //#region TileObjects

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="index"></param>
        ///// <param name="layer"></param>
        ///// <returns></returns>
        //public TilemapObject GetObject(Point index, string layer)
        //{
        //    return Layers[layer].GetObject(index);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="layer"></param>
        //public void AddObject(TilemapObject obj, string layer)
        //{
        //    Layers[layer].AddObject(obj);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="layer"></param>
        ///// <returns></returns>
        //public bool RemoveObject(TilemapObject obj, string layer)
        //{
        //   return Layers[layer].RemoveObject(obj);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tileObject"></param>
        ///// <param name="layer"></param>
        //public void DestroyObject(TilemapObject tileObject, string layer)
        //{
        //    RemoveObject(tileObject, layer);
        //    tileObject = null;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="layer"></param>
        //public void ClearObjects(string layer)
        //{
        //    Layers[layer].TileObjects.Clear();
        //}

        //#endregion TileObjects





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
