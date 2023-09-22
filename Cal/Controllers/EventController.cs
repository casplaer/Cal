using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cal.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime date)
        {
            var events = await _context.Events
                .Where(e => e.Date.Date == date.Date)
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public IActionResult NewEvent()
        {
            return View();
        }

/*        [HttpPost]
        public async Task<IActionResult> NewEvent(Event newEvent)
        {
            if (!ModelState.IsValid)
                return View(newEvent);

            var createNewEvent = new Event()
            {
                Date = newEvent.Date,
                Name = newEvent.Name,
                Description = newEvent.Description,
                AppUser = 
            };

        }*/
    }
}
