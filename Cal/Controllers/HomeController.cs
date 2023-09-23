﻿using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            int currentMonth = HttpContext.Session.GetInt32("currentMonth") ?? DateTime.Now.Month;
            ViewBag.currentMonth = currentMonth;
            int currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;
            ViewBag.currentYear = HttpContext.Session.GetInt32("currentYear") ?? DateTime.Now.Year;
            if (_signInManager.IsSignedIn(User))
            {
                List<Event> _events = _context.Events
                    .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear && e.AppUser.Email == User.Identity.Name)
                    .ToList();
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