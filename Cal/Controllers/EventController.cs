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
                .Where(e => e.Date.Date == date.Date && e.AppUser.Email == User.Identity.Name)
                .ToListAsync();
            HttpContext.Session.SetString("ShortDate", date.ToShortDateString());
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
            };

            var aaa = createNewEvent.AppUser;
            _context.Events.Add(createNewEvent);
            _context.SaveChanges();

            return RedirectToAction("Index", "Event", new { date = createNewEvent.Date });
        }

        public IActionResult DeleteEvent(int id)
        {
            var eventToDelete = _context.Events.Find(id);
            var Date = eventToDelete.Date.Date;
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", new { date = Date });
        }

        [HttpGet]
        [Route("Event/EditEvent")]

        public IActionResult EditEvent(int id)
        {
            var Event = _context.Events.Find(id);

            /*if (Event == null)
            {
                return RedirectToAction();
            }*/
            HttpContext.Session.SetInt32("EventId", id);
            return View(Event);
        }

        [HttpPost]
        [Route("Event/EditEvent")]
        public IActionResult EditEvent(Event _event)
        {
            if (!ModelState.IsValid)
                return View(_event);

            var exist = _context.Events.Find(HttpContext.Session.GetInt32("EventId"));

            try
            {
                exist.Name = _event.Name;
                exist.Description = _event.Description;
                exist.Date = _event.Date;

            }
            catch(NullReferenceException ex)
            {
                
            }
            _context.SaveChanges();

            HttpContext.Session.Remove("EventId");

            return RedirectToAction("Index", new { date = exist.Date });

        }
    }
}
