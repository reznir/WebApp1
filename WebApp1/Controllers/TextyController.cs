using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebApp1.DataAccess;
using WebApp1.Models;
using WebApp1.Models.Texty;

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
            List<LitText> litTexts = new();
            if (svatekId != 0)
            { litTexts = context.LitText.Where(t => t.SvateId == svatekId).ToList(); }            
            var model = new SvatkyModel()
            {
                Doby = context.Doba.ToList(),
                Svatky = context.Svatek.ToList(),
                LitTexty = litTexts                
            };
            return View(model);
        }

        public IActionResult Delete(int textId)
        {
            var litText = context.LitText.FirstOrDefault(t => t.Id == textId);
            List<LitText> litTexts = new();
            if (litText != null)
            {
                context.LitText.Remove(litText);
                litTexts = context.LitText.Where(t => t.SvateId == litText.SvateId).ToList();
            }
            var model = new SvatkyModel()
            {
                Doby = context.Doba.ToList(),
                Svatky = context.Svatek.ToList(),
                LitTexty = litTexts
            };
            return View("Index", model);
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
