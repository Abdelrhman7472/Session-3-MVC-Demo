using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
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
        private readonly DataContext _dataContext;

        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Department? Get(int id) => _dataContext.Departments.Find(id);

        public IEnumerable<Department> GetAll() => _dataContext.Departments.ToList();

        public int Create(Department department)
        {
            _dataContext.Departments.Add(department);
            return _dataContext.SaveChanges();

        }
        public int Delete(Department department)
        {
            _dataContext.Departments.Remove(department);
            return _dataContext.SaveChanges();

        }
        public int Update(Department department)
        {
            _dataContext.Departments.Update(department);
            return _dataContext.SaveChanges();

        }





    }
}
