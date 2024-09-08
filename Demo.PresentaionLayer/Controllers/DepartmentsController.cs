using Demo.BusinessLogicLayer.Repositories;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentaionLayer.Controllers
{
    public class DepartmentsController : Controller
    {
     private readonly IDepartmentRepository DepartmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            DepartmentRepository = departmentRepository;
        }

        public IActionResult Index()
        { 
            var departments=DepartmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create() 
        { 
          return View();

        }

        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            DepartmentRepository.Create(department);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details()
        {
            var departments = DepartmentRepository.Get();
            return View(departments);
        }



    }
}
