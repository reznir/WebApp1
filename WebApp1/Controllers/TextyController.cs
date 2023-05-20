using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Controllers
{
    public class TextyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
