
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

        public async Task<IEnumerable<Employee>> GetAllAsync(string name)    
        =>await  _dbContext.Set<Employee>().Where(e=>e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToListAsync();

        public async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync()
        => await _dbContext.Set<Employee>().Include(e=>e.Department).ToListAsync();

    }
}
