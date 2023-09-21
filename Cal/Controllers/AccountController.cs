using Cal.Data;
using Cal.Models;
using Cal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Route("Account/Login")]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [Route("Account/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again.";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrond credentials. Try again.";
            return View(loginViewModel);
        }

        [Route("Account/Register")]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if(user != null)
            {
                TempData["Error"] = "Аккаунт с таким адресом уже существует.";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            
            return View("Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
