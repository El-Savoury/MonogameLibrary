using MonogameLibrary.Maths;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace MonogameLibrary.Tilemaps
{
    public class Tilemap
    {
        #region Properties

        public Tileset Tileset;
        public Vector2 Position { get; set; }
        public Dictionary<string, TilemapLayer> TilemapLayers { get; }
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
        /// Create a tile map of specified position and size
        /// </summary>
        /// <param name="tileset"></param>
        /// <param name="position"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public Tilemap(Tileset tileset, Vector2 position, int tileWidth, int tileHeight, int columns, int rows)
        {
            Tileset = tileset;
            Position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = columns;
            Rows = rows;

            TilemapLayers = new Dictionary<string, TilemapLayer>();
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





        #region Utility

        /// <summary>
        /// Add a new layer to the tilemap
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddLayer(string name)
        {
            if (TilemapLayers.ContainsKey(name))
            {
                throw new ArgumentException($"Tilemap layer with name {name} already exists");
            }

            TilemapLayer layer = new TilemapLayer(Tileset, name, Position, TileWidth, TileHeight, Columns, Rows);
            TilemapLayers.Add(name, layer);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="tileIndex"></param>
        /// <returns></returns>
        public Tile GetTile(string layer, int tileIndex)
        {
            return TilemapLayers[layer].GetTile(tileIndex);
        }


        public Tile GetTile(string layer, int column, int row)
        {
            return TilemapLayers[layer].GetTile(column, row);
        }

        public T GetTile<T>(string layer, int column, int row) where T : Tile
        {
            Tile tile = GetTile(layer, column, row);
            if (tile is T)
            {
                return tile as T;
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Tile GetTile(string layer, Vector2 worldPos)
        {
            return TilemapLayers[layer].GetTile(worldPos);
        }


        public void SetTile(Enum tilemapLayer, Tile tile, int row, int column)
        {
            TilemapLayers[tilemapLayer.ToString()].SetTile(row, column, tile);
        }


        //public Tile[] GetTilesInRect(RectF rect)
        //{
        //    Vector2 topLeftTileIndex = rect.Min;
        //    int tilesAcross = (int)rect.Width / TileWidth;
        //    int tilesDown = (int)rect.Height / TileHeight;



        //}

        #endregion Utility
    }
}
