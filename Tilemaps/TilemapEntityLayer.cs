using MonogameLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MonogameLibrary.Tilemaps
{
    public class TilemapEntityLayer : TilemapLayer
    {
        private List<TileEntity> _entities = new List<TileEntity>();

        public TilemapEntityLayer(string name, Vector2 position) : base(name, position)
        {

        }


        public void AddEntity(TileEntity entity)
        {
            _entities.Add(entity);
        }


        public void RemoveEntity(TileEntity entity)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i] == entity)
                {
                    _entities[i].Destroy();
                    _entities.RemoveAt(i);
                }
            }
        }


        public void RemoveEntityAt(Point index)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].Index == index)
                {
                    _entities[i].Destroy();
                    _entities.RemoveAt(i);                    
                }
            }
        }


        public TileEntity GetEntity(Point index)
        {
            foreach (TileEntity entity in _entities)
            {
                if (entity.Index == index)
                {
                    return entity;
                }
            }
            return null;
        }


        public TileEntity GetEntity(int xIndex, int yIndex)
        {
            foreach (TileEntity entity in _entities)
            {
                if (entity.Index == new Point(xIndex, yIndex))
                {
                    return entity;
                }
            }
            return null;
        }
    }
}
