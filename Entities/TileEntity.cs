using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Graphics;
using MonogameLibrary.Tilemaps;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace MonogameLibrary.Entities
{
    public abstract class TileEntity : Entity
    {
        public Tilemap Map { get; private set; }
        public Point Index;


        public new Vector2 Position => Map.IndexToWorldPos(Index.X, Index.Y);

        public TileEntity(Tilemap map, Point index)
        {
            Map = map;
            Index = index;
        }


        public Point GetNeighbourIndex(int x, int y)
        {
            return new Point(Index.X + x, Index.Y + y);
        }


        public Tile GetNeighbourTile(int x, int y)
        {
            Point index = GetNeighbourIndex(Index.X, Index.Y);
            return Map.GetTile(index, "defaultLayer");
        }


        public List<Point> GetNeighbours(Point index)
        {
            return new List<Point>
            {
              new Point(index.X - 1, index.Y -1), // Top Left  
              new Point(index.X, index.Y - 1), // Up
              new Point(index.X + 1, index.Y - 1), // Top right
              new Point(index.X + 1, index.Y), // Right
              new Point(index.X + 1, index.Y + 1), // Bottom Right
              new Point(index.X, index.Y + 1), // Down
              new Point(index.X - 1, index.Y + 1), // Bottom left
              new Point(index.X - 1, index.Y), // Left                 
            };
        }


        public List<Point> GetCardinalNeighbours(Point index)
        {
            return new List<Point>
            {
              new Point(index.X, index.Y - 1), // Up
              new Point(index.X + 1, index.Y), // Right
              new Point(index.X, index.Y + 1), // Down
              new Point(index.X - 1, index.Y), // Left
            };
        }


        public virtual void Draw(SpriteBatch spriteBatch, Point index)
        {
        }
    }
}
