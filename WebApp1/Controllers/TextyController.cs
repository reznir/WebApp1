using Microsoft.AspNetCore.Mvc;
using WebApp1.DataAccess;

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
    }
}
