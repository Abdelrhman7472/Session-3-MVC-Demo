
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

        public IEnumerable<Employee> GetAll(string name)    
        {
          return _dbContext.Set<Employee>().Where(e=>e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToList(); 

        }

        public IEnumerable<Employee> GetAllWithDepartment()
        {
            return _dbContext.Set<Employee>().Include(e=>e.Department).ToList();
        }
    }
}
