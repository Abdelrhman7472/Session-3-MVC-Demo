using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DataContext _dbContext;

        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public TEntity? Get(int id)=>_dbContext.Set<TEntity>().Find(id);
        

        public IEnumerable<TEntity> GetAll() => _dbContext.Set<TEntity>().ToList();
       
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
