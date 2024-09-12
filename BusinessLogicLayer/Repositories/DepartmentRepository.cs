
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository /*IGenericRepository<Department>*/
    { //   private DataContext dataContext=new DataContext(); 
        /*
         Dependancy Injection:
        Method Injection => Method ([FromServices] DataContext dataContext)

          Property Injection=>
        [From Services]
        public DataContext dataContext { get; set; }

        Ctor Injection=> Most Common used


         */


        //private readonly DataContext _dataContext = new DataContext();
        public DepartmentRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
