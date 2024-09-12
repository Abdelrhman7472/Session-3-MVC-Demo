using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.PresentaionLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _repo = employeeRepository;
        }

        public IActionResult Index()
        {
            //ViewData=> Dictionary<string,object>
            //ViewData["Message"] = new Employee { Name="Abdo"};
            ViewBag.Employee = new Employee { Name = "Seif" };
            var employees = _repo.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _repo.Create(employee);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));


        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (_repo.Update(employee)> 0);
                    TempData["Message"] = "Employee Updated Successfuly";   

                    return RedirectToAction(nameof(Index));

                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);


                }

            }
            return View(employee);

        }

        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _repo.Get(id.Value);
            if (employee is null) return NotFound();
            try
            {
                _repo.Delete(employee);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);


            }
            return View(employee);
        }


        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {

            if (!id.HasValue) return BadRequest();


            var employees = _repo.Get(id.Value);

            if (employees is null) return NotFound();

            return View(viewName, employees);


        }
    }
}
