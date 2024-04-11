using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Models.ViewModels;

namespace Uni_Sphere.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username,
                    loginViewModel.Password, false, false);

                if (signInResult != null && signInResult.Succeeded)
                {
                    TempData["Success"] = "You have successfully logged in";
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Invalid username or password";
            // Show error
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "You have successfully logged out";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
