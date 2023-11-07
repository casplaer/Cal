using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            int currentMonth = HttpContext.Session.GetInt32("currentMonth") ?? DateTime.Now.Month;
            ViewBag.currentMonth = currentMonth;
            int currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;
            ViewBag.currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;

            ViewBag.Show = HttpContext.Session.GetString("true") ?? "false";

            HttpContext.Session.Remove("true");
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                List<Event> _events = _context.Events
                    .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear && e.AppUser.Email == User.Identity.Name)
                    .ToList();
                List<Category> Categories = _context.Events
                    .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear && e.AppUser.Email == User.Identity.Name)
                    .Select(e => e.Category)  // Выбор категорий из событий
                    .Distinct()  // Выбор уникальных категорий
                    .ToList();
                ViewBag.Categories = Categories;
                return View(_events);
            }
            else return View();
        }

        [HttpPost]
        public IActionResult Month(bool plus)
        {
            int currentMonth = HttpContext.Session.GetInt32("currentMonth") ?? DateTime.Now.Month;
            int currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;


            if (plus) currentMonth++;
            else currentMonth--;

            if (currentMonth > 12)
            {
                currentMonth = 1;
                currentYear++;
            }

            if (currentMonth < 1)
            {
                currentMonth = 12;
                currentYear--;
            }
            HttpContext.Session.SetInt32("currentYear", currentYear);
            HttpContext.Session.SetInt32("currentMonth", currentMonth);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}