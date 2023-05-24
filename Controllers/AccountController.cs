using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StartBootstrapTemplate.Models;
using StartBootstrapTemplate.ViewModels.Account;

namespace StartBootstrapTemplate.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser()
            {
                Name=registerVM.Name,
                Email=registerVM.Email,
                Surname=registerVM.Surname,
                UserName=registerVM.Username
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser,registerVM.Password);
            if (!!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(appUser,false);
       
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (appUser==null)
                {
                    ModelState.AddModelError(String.Empty, "Bele hesab yoxdur");
                    return View();
                }
            }
            await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, true);
            return RedirectToAction("Index","Home");
           
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
