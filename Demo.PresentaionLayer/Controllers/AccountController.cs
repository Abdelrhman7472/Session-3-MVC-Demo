using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.PresentaionLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{ 
            if (!ModelState.IsValid) return View(model);
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName, 
                LastName = model.LastName,
            };


            var result=  _userManager.CreateAsync(user , model.Password).Result;
            if (result.Succeeded) 
                return RedirectToAction(nameof(Login));
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(); 

		}

		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Login(LoginViewModel model )
        {
            //Server Side Validation
            if (!ModelState.IsValid) return View(model);
            // Retrive and check if user exists
            var user=_userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null) {
                //check Password
               if( _userManager.CheckPasswordAsync(user,model.Password).Result)
                {
                    //Login if Password is correct
                    var result=_signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false).Result;
                    if (result.Succeeded) return RedirectToAction(nameof(HomeController.Index),nameof(HomeController).Replace("Controller",string.Empty));
                }
            }
            ModelState.AddModelError(string.Empty, "Incorrect Email Or Password");
            return View(model);
        }

        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
             

        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            // check if user exists 
            if (!ModelState.IsValid) return View(model);
            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user is not null)
            { 
                //create reset password token
                var token = _userManager.FindByEmailAsync(model.Email).Result;
                //creata url to reset password 
                var url = Url.Action(nameof(ResetPassword),nameof(AccountController).Replace("Controller", string.Empty),
                    new {email=model.Email,Token =token },Request.Scheme);
                //creata email object (Email to user )
                var email = new Email
                {
                    Subject = "ResetPassword",
                    Body = url!,
                    Recipient = model.Email,
                };
                // send email
                MailSettings.SendEmail(email);
                // return redirect to Check Your Inbox

                return RedirectToAction(nameof(CheckYourInbox));

            }
            ModelState.AddModelError(string.Empty, "User Not Found");
            return View(model);

        
        }

        public IActionResult CheckYourInbox()
        { return View();
        
        }
        public IActionResult ResetPassword(string email,string token)
        {
            if(email is null ||token  is null) { return BadRequest(); }
            TempData["Email"]=email;
            TempData["Token"]=token;
            return View();
        }

        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            model.Token = TempData["Token"]?.ToString()??string.Empty;
            model.Email = TempData["Email"]?.ToString()??string.Empty;
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user != null)
            { 
                var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Login));
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            ModelState.AddModelError(string.Empty,"User Not Found");

            return View();
        }
    }
}
