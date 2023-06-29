using Microsoft.AspNetCore.Mvc;
using WebApp1.DataAccess;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class TextyController : Controller
    {
        private readonly LitTextyDbContext context;

        public TextyController(LitTextyDbContext Context)
        {
            context = Context ?? throw new ArgumentNullException();
        }
        public IActionResult Index()
        {
            return View(context);
        }

        [HttpPost]
        public IActionResult Index(string text, DateTime date, Cyklus cyklus)
        {
            var a = text.GetType();
            var b = text.ToString();
            return View(context);
        }

        public IActionResult adventni1()
        {
            return View(context);
        }
    }
}
