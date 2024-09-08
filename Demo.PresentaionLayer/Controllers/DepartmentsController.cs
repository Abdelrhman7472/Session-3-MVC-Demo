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

        public IActionResult Details(int? id )
        {
            if (!id.HasValue) 
                return BadRequest();
            
            
            var departments = DepartmentRepository.Get(id.Value);

            if (departments is null)  return NotFound(); 

            return View(departments);
        }  
        public IActionResult Edit(int? id )
        {
            if (!id.HasValue) 
                return BadRequest();
            
            
            var departments = DepartmentRepository.Get(id.Value);

            if (departments is null)  return NotFound(); 

            return View(departments);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if(id!=department.Id)return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    DepartmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));

                }

                catch (Exception ex)
                { 
                    ModelState.AddModelError("",ex.Message);
                
                
                }

            }
            return View(department);

        }
    


    }
}
