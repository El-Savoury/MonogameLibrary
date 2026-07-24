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


        public List<Point> GetNeighbours()
        {
            List<Point> neighbours = new List<Point>();

            for (int x = -1; x < neighbours.Count; x++)
            {
                for (int y = -1; y < 3; y++)
                {
                    neighbours.Add(new Point(x, y));
                }
            }
            return neighbours;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point index)
        {
        }
    }
}
