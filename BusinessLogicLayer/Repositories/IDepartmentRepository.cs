using Demo.DataAccessLayer.Models;

namespace Demo.BusinessLogicLayer.Repositories
{
    public interface IDepartmentRepository
    {
        int Create(Department department);
        int Delete(Department department);
        Department? Get(int id);
        IEnumerable<Department> GetAll();
        int Update(Department department);
    }
}