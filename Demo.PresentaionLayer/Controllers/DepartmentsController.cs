using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentaionLayer.Controllers
{
    public class DepartmentsController : Controller
    {
     private readonly IDepartmentRepository _repo;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repo = departmentRepository;
        }

        public IActionResult Index()
        { 
            var departments=_repo.GetAll();
            return View(departments);
        }

        public IActionResult Create() 
        { 
          return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Create(Department department) 
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            _repo.Create(department);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int? id ) => DepartmentControllerHandler(id,nameof(Details));

        
        public IActionResult Edit(int? id ) => DepartmentControllerHandler(id,nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if(id!=department.Id)return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(department);
                    return RedirectToAction(nameof(Index));

                }

                catch (Exception ex)
                { 
                    ModelState.AddModelError("",ex.Message);
                
                
                }

            }
            return View(department);

        }

        public IActionResult Delete(int? id) => DepartmentControllerHandler(id,nameof(Delete));
     
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult ConfirmDelete(int? id ) 
        { 
            if (!id.HasValue)  return BadRequest();
            var department= _repo.Get(id.Value);
            if (department is null) return NotFound();
            try
            { 
                _repo.Delete(department);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex) { 
            
                ModelState.AddModelError(string.Empty,ex.Message);
            
            
            }
            return View(department);
        }


        private IActionResult DepartmentControllerHandler(int? id ,string viewName)
        {

            if (!id.HasValue) return BadRequest();


            var departments = _repo.Get(id.Value);

            if (departments is null) return NotFound();

            return View(viewName,departments);


        }
    }
}
