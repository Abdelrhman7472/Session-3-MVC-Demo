using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // SaveChanges must be async because it connects to DataBase
        Task AddAsync(TEntity entity);// change the state to added and async version of add connect to database and take the default value of PK
        void Delete(TEntity entity);// only change the state to deleted so don't have async version
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);// only change the state to updated so don't have async version
    }
}
