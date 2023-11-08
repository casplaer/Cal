using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Include(e => e.Category)
                .Where(e => e.Date.Date == date.Date && e.AppUser.Email == User.Identity.Name)
                .ToListAsync();

            var uniqueCategories = events.Select(e => e.Category).Distinct().ToList();
            var categories = await _context.Categories.Where(c => uniqueCategories.Contains(c)).ToListAsync();
            HttpContext.Session.SetString("ShortDate", date.ToShortDateString());
            ViewBag.Date = date;
            ViewBag.Categories = categories;
            return View(events);
        }

        [HttpGet]
        public IActionResult NewEvent()
        {
            DateTime aboba;
            DateTime.TryParse(HttpContext.Session.GetString("ShortDate"), out aboba);

            // Создайте словарь для соответствия категорий и их цветов.
            ViewBag.Categories = _context.Categories.Where(c=>c.AppUser.Email == User.Identity.Name).Distinct().ToList();

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

            DateTime passing;
            DateTime.TryParse(HttpContext.Session.GetString("ShortDate"), out passing);
            TimeSpan ts = new TimeSpan(Convert.ToInt16(newEvent.Date.Hour), Convert.ToInt16(newEvent.Date.Minute), 0);
            passing = passing.Date + ts;

            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            var trueCategory = _context.Categories.FirstOrDefault(c=>c.CategoryName==newEvent.Category.CategoryName);

            var createNewEvent = new Event()
            {
                Date = passing,
                Name = newEvent.Name,
                Description = newEvent.Description,
                AppUser = user,
                UserId = user.Id,
                Category = trueCategory,
            };
            _context.Events.Add(createNewEvent);
            _context.SaveChanges();

            return RedirectToAction("Index", "Event", new { date = createNewEvent.Date });
        }


        public IActionResult DeleteEvent(int id)
        {
            var eventToDelete = _context.Events.Find(id);
            var Date = eventToDelete.Date.Date;

            if (eventToDelete.AppUser.Email != User.Identity.Name)
            {
                return RedirectToAction("Error", "Error", 404);
            }

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

            var Event = _context.Events
                .Include(e => e.Category)
                .Include(e => e.AppUser)
                .FirstOrDefault(e => e.Id == id);
            
            if(Event.AppUser.Email != User.Identity.Name)
            {
                return RedirectToAction("Error", "Error", 404);
            }

            ViewBag.Categories = _context.Categories.Distinct().ToList();

            HttpContext.Session.SetInt32("EventId", id);
            return View(Event);
        }

        [HttpPost]
        [Route("Event/EditEvent")]
        public async Task<IActionResult> EditEvent(Event _event)
        {
            if (!ModelState.IsValid)
                return View(_event);

            var exist = _context.Events.Find(HttpContext.Session.GetInt32("EventId"));

            var trueCategory = _context.Categories.FirstOrDefault(c => c.CategoryName == _event.Category.CategoryName);

            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            try
            {
                exist.Name = _event.Name;
                exist.Description = _event.Description;
                exist.Date = _event.Date;
                exist.Category = trueCategory;
            }
            catch(NullReferenceException ex)
            {
                
            }
            _context.SaveChanges();

            HttpContext.Session.Remove("EventId");

            return RedirectToAction("Index", new { date = exist.Date });

        }

        [HttpPost]
        public IActionResult ShareEvents(List<int> selectedEvents)
        {
            List<Event> events = _context.Events
                .Where(e => selectedEvents.Contains(e.Id))
                .Include(e=>e.Category)
                .Include(e=>e.AppUser)
                .ToList();

            string uniqueIdentifier = Guid.NewGuid().ToString();

            SharedEvents sharedEvents = new SharedEvents
            {
                UniqueIdentifier = uniqueIdentifier,
                Events = events,
                CreationTime = DateTime.Now
            };

            _context.SharedEvents.Add(sharedEvents);
            _context.SaveChanges();

            return Redirect($"/ShareEvents/{uniqueIdentifier}");
        }

        [HttpGet]
        [Route("ShareEvents/{uniqueIdentifier}")]
        public IActionResult ShareEvents(string uniqueIdentifier)
        {
            SharedEvents shared = _context.SharedEvents
                .Include(se => se.Events)
                    .ThenInclude(e=>e.Category)
                    .ThenInclude(e=>e.AppUser)
                .FirstOrDefault(se => se.UniqueIdentifier == uniqueIdentifier);

            return View(shared);
        }
    }
}
