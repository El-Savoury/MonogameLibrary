using MonogameLibrary.Graphics;
using MonogameLibrary.Maths;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;

namespace MonogameLibrary.Tilemaps
{
    public class Tilemap
    {
        #region Properties

        public Tileset Tileset;
        public TileTypeRegistry TileRegistry = new TileTypeRegistry();
        public Vector2 Position { get; set; }
        public Dictionary<string, TilemapLayer> TilemapLayers { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public int Width { get; }
        public int Height { get; }
        public int Count => Width * Height;
        public int WidthInPixels => Width * TileWidth;
        public int HeightInPixels => Height * TileHeight;

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
            Width = columns;
            Height = rows;

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
        /// Register a new tile type to this tilemap
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public void AddTileType(Enum type, int tilesetID)
        {
            TileRegistry.Add(type, tilesetID);
        }


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

            TilemapLayer layer = new TilemapLayer(name, Position, TileWidth, TileHeight, Width, Height, this);
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


        public Tile GetTile(string layer, int column, int row)
        {
            return TilemapLayers[layer].GetTile(column, row);
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

        public Vector2 GetTileWorldPos(int x, int y)
        {
            return new Vector2(Position.X + x * TileWidth, Position.Y + y * TileHeight);
        }

        public Point GetIndexfromWorldPos(Vector2 worldPos)
        {
            int xPosOffset = (int)(worldPos.X - Position.X);
            int YPosOffset = (int)(worldPos.Y - Position.Y);

            return new Point(xPosOffset / TileWidth, YPosOffset / TileWidth);
        }


        public void SetTile(Enum tilemapLayer, Tile tile, int row, int column)
        {
            TilemapLayers[tilemapLayer.ToString()].SetTile(row, column, tile);
        }


        public void SetTile(string tilemapLayer, Tile tile, int row, int column)
        {
            TilemapLayers[tilemapLayer].SetTile(row, column, tile);
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
