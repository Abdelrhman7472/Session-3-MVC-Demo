using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork 
    {
        public IEmployeeRepository Employees { get; }

        public IDepartmentRepository Departments { get; }

        public int SaveChanges(); 
    }
}
