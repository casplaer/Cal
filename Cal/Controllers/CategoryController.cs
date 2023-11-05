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
        public async Task<IActionResult> NewCategory(Category category)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", category);

            var checking = _context.Categories.FirstOrDefault(c=>c.CategoryName == category.CategoryName);

            if(checking!=null)
            {
                TempData["Error"] = "Категория с таким названием уже существует, пожалуйста, выберите другое название.";
                return RedirectToAction("Index", category);
            }

            checking = _context.Categories.FirstOrDefault(c=>c.CategoryColor == category.CategoryColor);

            if (checking != null)
            {
                TempData["Error"] = "Категория с таким цветом уже существует, пожалуйста, выберите другой цвет.";
                return View(category);
            }

            var NewCategory = new Category()
            {
                CategoryColor = category.CategoryColor,
                CategoryName = category.CategoryName,
            };

            _context.Categories.Add(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("NewEvent", "Event");
        }

        public IActionResult DeleteCategory(int id)
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
    }
}
