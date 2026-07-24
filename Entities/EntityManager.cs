namespace MonogameLibrary.Entities
{
    public class EntityManager
    {
        #region Members

        public List<Entity> Entities { get; private set; }
        
        #endregion Members





        #region Init

        public EntityManager()
        {
            Entities = new List<Entity>();
        }


        public void LoadAll()
        {
            foreach (Entity entity in Entities)
            {
                entity.LoadContent();
            }
        }

        #endregion Init





        #region Update

        /// <summary>
        /// Update all entities
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                if (entity.IsEnabled)
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
            foreach (Entity entity in Entities)
            {
                if (entity.IsEnabled)
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
        public void Register(Entity entity)
        {
            Entities.Add(entity);
        }


        /// <summary>
        /// Remove an entity from the update list
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }


        /// <summary>
        /// Remove all entities held by this manager
        /// </summary>
        public void RemoveAll()
        {
            Entities.Clear();
        }

        #endregion Utility
    }
}

