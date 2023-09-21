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

        [HttpGet("{Id}/{date}")]
        public async Task<IActionResult> Index(string Id, DateTime date)
        {
            var events = await _context.Events
                .Where(e => e.AppUser.Id == Id && e.Date.Date == date.Date)
                .ToListAsync();

            return View(events);
        }
    }
}
