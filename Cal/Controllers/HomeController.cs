using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //List<Event> _events = _context.Events.ToList();

            int currentMonth = HttpContext.Session.GetInt32("currentMonth") ?? DateTime.Now.Month;
            ViewBag.currentMonth = currentMonth;
            ViewBag.currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;
            return View();
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