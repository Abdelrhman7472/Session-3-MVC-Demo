using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllAsync(string Name); 
        public Task<IEnumerable<Employee>> GetAllWithDepartmentAsync(); 
   
    }
}
