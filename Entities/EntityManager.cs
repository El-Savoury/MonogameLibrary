namespace MonogameLibrary.Entities
{
    public class EntityManager
    {
        #region Members

        private List<Entity> entities;

        #endregion Members





        #region Init

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        #endregion Init





        #region Update

        /// <summary>
        /// Update all entities
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in entities)
            {
                if (entity.Enabled)
                {
                    entity.Update(gameTime);
                }
            }
        }

        #endregion Update






        #region Draw

        /// <summary>
        /// Draw all entities
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in entities)
            {
                if (entity.Enabled)
                {
                    entity.Draw(spriteBatch);
                }
            }
        }

        #endregion Draw






        #region Utility

        /// <summary>
        /// Add an entity to be drawn and updated
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add(Entity entity)
        {
            entities.Add(entity);
        }


        /// <summary>
        /// Remove an entity from the update list
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(Entity entity)
        {
            entities.Remove(entity);
        }


        /// <summary>
        /// Remove all entities held by this manager
        /// </summary>
        public void RemoveAll()
        {
            entities.Clear();
        }

        #endregion Utility
    }
}

