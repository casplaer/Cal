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
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(ApplicationDbContext context, UserManager<AppUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult NewCategory()
        {
            return View();
        }

        //Создание новой категории.
        [HttpPost]
        public async Task<IActionResult> NewCategory(Category category)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", category);

            var checking = _context.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName && c.AppUser.Email == User.Identity.Name);

            if(checking!=null)
            {
                TempData["Error"] = "Категория с таким названием уже существует, пожалуйста, выберите другое название.";
                return RedirectToAction("Index", category);
            }

            checking = _context.Categories.FirstOrDefault(c=>c.CategoryColor == category.CategoryColor && c.AppUser.Email == User.Identity.Name);

            if (checking != null)
            {
                TempData["Error"] = "Категория с таким цветом уже существует, пожалуйста, выберите другой цвет.";
                return View(category);
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var NewCategory = new Category()
            {
                CategoryColor = category.CategoryColor,
                CategoryName = category.CategoryName,
                AppUser = user,
            };

            _context.Categories.Add(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("NewEvent", "Event");
        }

        //Список всех категорий данного пользователя.
        [HttpGet]
        public IActionResult CategoryList()
        {
            List<Category> categories = _context.Categories.Where(c=>c.AppUser.Email == User.Identity.Name).ToList();
            return View(categories);
        }

        //Удаление категории.
        public IActionResult DeleteCategory(int id)
        {
            var categoryToDelete = _context.Categories.Find(id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("CategoryList");
        }

        //Переход на страницу редактирования категории.
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var Category = _context.Categories
                .FirstOrDefault(c => c.CategoryId == id);

            HttpContext.Session.SetInt32("EventId", id);
            return View(Category);
        }


        //Редактирование категории.
        [HttpPost]
        public IActionResult EditCategory(Category _category)
        {
            if (!ModelState.IsValid)
                return View(_category);

            var trueCat = _context.Categories.Find(_category.CategoryId);

            if(trueCat != null)
            {
                trueCat.CategoryColor= _category.CategoryColor;
                trueCat.CategoryName= _category.CategoryName;
                _context.SaveChanges();
            }

            return RedirectToAction("CategoryList");

        }
    }
}
