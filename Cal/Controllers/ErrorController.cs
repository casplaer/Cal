using Microsoft.AspNetCore.Mvc;

namespace Cal.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
            }
            HttpContext.Session.SetString("true", "true");
            return RedirectToAction("Index", "Home");
        }
    }
}
