
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.PresentaionLayer.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchValue)
        {
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                 employees =  await _unitOfWork.Employees.GetAllWithDepartmentAsync();
            }
            else employees= await _unitOfWork.Employees.GetAllAsync(searchValue);
            var employeesViewModel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(employeesViewModel);


        }

        public async Task<IActionResult> Create()
        {
            var departments= await _unitOfWork.Departments.GetAllAsync();
            SelectList listItems = new SelectList(departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            if(employeeVM.Image is not null)
                employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");

            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

           await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int? id) =>await EmployeeControllerHandler(id, nameof(Details));


        public async Task<IActionResult> Edit(int? id) => await EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");
                    var employee=_mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (await _unitOfWork.SaveChangesAsync() > 0)
                     TempData["Message"] = "Employee Updated Successfuly"; 

                    return RedirectToAction(nameof(Index));

                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);


                }

            }
            return View(employeeVM);

        }

        public async Task<IActionResult> Delete(int? id) =>await EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee =await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee is null) return NotFound();
            try
            {
                _unitOfWork.Employees.Delete(employee);
                if(await _unitOfWork.SaveChangesAsync()>0 &&employee.ImageName is not null)
                    DocumentSettings.DeleteFile("Images" ,employee.ImageName);
                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);


            }
            return View(employee);
        }


        private async Task<IActionResult> EmployeeControllerHandler(int? id, string viewName)
        {
            if (viewName == nameof(Edit))
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();
                SelectList listItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();


            var employees = await _unitOfWork.Employees.GetAsync(id.Value);

            if (employees is null) return NotFound();
            var employeeVM = _mapper.Map<Employee,EmployeeViewModel>(employees);
            return View(viewName,employeeVM );


        }
    }
}
