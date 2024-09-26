using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentaionLayer.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class UsersController : Controller
    {
    private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                var users = await _userManager.Users.Select(u=>new UserViewModel
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Id=u.Id,
                    UserName=u.UserName,
                    Roles=_userManager.GetRolesAsync(u).GetAwaiter().GetResult() // get awaiter w get result same as await and async

                }).ToListAsync();
                return View(users);
            }
            
            var user=await _userManager.FindByEmailAsync(email);
            if (user is null)  
                 return View(Enumerable.Empty<UserViewModel>());

            var model = new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                Roles = await _userManager.GetRolesAsync(user)

            };

            return View(model);



        }

        public async Task<IActionResult> Details(string id,string viewName=nameof(Details))
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var user= await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var model = new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                Roles = await _userManager.GetRolesAsync(user)

            };
            return View(viewName,model);
            


        }
   
        public async Task<IActionResult>Edit(string id)=> await Details(id,nameof(Edit));
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if(id!=model.Id) return BadRequest();
            if(!ModelState.IsValid) return View(model);

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null) return NotFound();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            { 
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);


        }

        public async Task<IActionResult>Delete(string id)=> await Details(id,nameof(Delete));

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id) 
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return NotFound();
     
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }



    }
}
