using API.Context;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository <Entity, Key>
        where Context : MyContext
        where Entity : class
    {

        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();

        }

        public int delete(Key key)
        {
            var find = entities.Find(key);
            entities.Remove(find);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public IEnumerable<Entity> Get()
        {

            return entities.ToList();
        
        }

        public Entity Get(Key key)
        {
            var find = entities.Find(key);
            return find;
        }

        public int insert(Entity entity)
        {
            try
            {
                entities.Add(entity);
                var insert = myContext.SaveChanges();
                return insert;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public int update(Entity entity, Key key)
        {
            try
            {
                myContext.Entry(entity).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;

            }
            catch (NullReferenceException)
            {

                return 0;
            }
        }
    }
}
