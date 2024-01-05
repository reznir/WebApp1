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
        public IActionResult Index(int svatekId)
        {
            var litTexts = context.LitText.Where(t => t.SvateId == svatekId).ToList();
            ViewData[nameof(litTexts.Count)] = litTexts.Count;
            ViewData[nameof(litTexts)] = litTexts;
            return View("Index", "TextyLayout");
        }

        [HttpPost]
        public IActionResult Index(string text, DateTime date, Cyklus cyklus)
        {
            LitText litText = new()
            {
                Text = text,
                Cyklus = cyklus.ToString(),
            };
            LiturgickyRok rok = new(date);
            litText.SvateId = rok.GetSvatekId(date);
            litText.Nazev_dne = rok.NazevDne;

            context.LitText.Add(litText);
            context.SaveChanges();
            return View(context);
        }


        [HttpPost]
        public IActionResult Index(DateTime date, Cyklus cyklus, string searchText)
        {
            return View(context);
        }

        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult SvatekSelect(int id)
        {
            var litTexts = context.LitText.Where(t => t.SvateId == id).ToList();
            ViewData[nameof(litTexts.Count)] = litTexts.Count;
            ViewData[nameof(litTexts)] = litTexts;
            return RedirectToAction(nameof(Index));
        }
    }
}
