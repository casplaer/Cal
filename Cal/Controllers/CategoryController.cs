using Cal.Data;
using Cal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cal.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> userManager;

        public CategoryController(ApplicationDbContext context, UserManager<AppUser> manager) 
        {
            _context = context;
            userManager = manager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Task<IActionResult> NewCategory(Category category)
        {
            if (!ModelState.IsValid)
                return Task.FromResult<IActionResult>(View(category));

            var NewCategory = new Category()
            {
                CategoryColor = category.CategoryColor,
                CategoryName = category.CategoryName,
            };

            _context.Categories.Add(NewCategory);
            _context.SaveChanges();

            return Task.FromResult<IActionResult>(RedirectToAction("Index", "Event", new { date = HttpContext.Session.GetString("ShortDate") }));
        }
    }
}
