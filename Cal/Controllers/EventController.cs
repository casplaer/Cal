using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cal.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> userManager;
        public EventController(ApplicationDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            userManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime date)
        {
            var events = await _context.Events
                .Where(e => e.Date.Date == date.Date)
                .ToListAsync();
            HttpContext.Session.SetString("ShortDate" ,date.ToShortDateString());
            ViewBag.Date = date;
            return View(events);
        }

        [HttpGet]
        public IActionResult NewEvent()
        {
            DateTime aboba;
            DateTime.TryParse(HttpContext.Session.GetString("ShortDate"), out aboba);
            Event lol = new Event()
            {
                Date = aboba,
            };
            return View(lol);
        }

        [HttpPost]
        public async Task<IActionResult> NewEvent(Event newEvent)
        {
            if (!ModelState.IsValid)
                return View(newEvent);

            var user = await userManager.FindByEmailAsync("user@mail.ru");
            var createNewEvent = new Event()
            {
                Date = newEvent.Date,
                Name = newEvent.Name,
                Description = newEvent.Description,
                AppUser = user,
                UserId = user.Id,
                //Id = _context.Events.Count()
            };

            var newEventResponse = _context.Events.Add(createNewEvent);
            _context.SaveChanges();

            return RedirectToAction("Index", "Event", new { date = createNewEvent.Date });
        }
    }
}
