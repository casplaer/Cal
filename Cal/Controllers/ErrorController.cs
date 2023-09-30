using Microsoft.AspNetCore.Mvc;

namespace Cal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            HttpContext.Session.SetString("true", "true");
            return RedirectToAction("Index", "Home");
        }
    }
}
