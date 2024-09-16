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

        public void Create(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);


        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public TEntity? Get(int id) => _dbContext.Set<TEntity>().Find(id);


        public IEnumerable<TEntity> GetAll() => _dbContext.Set<TEntity>().ToList();

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
    }
}
