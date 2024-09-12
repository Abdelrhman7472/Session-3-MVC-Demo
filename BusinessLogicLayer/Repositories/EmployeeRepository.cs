
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Employee> GetAll(string Address)
        {
          return _dbContext.Set<Employee>().Where(e=>e.Address.ToLower() == Address.ToLower()).ToList(); 

        }
    }
}
