using Microsoft.AspNetCore.Mvc;
using WebApp1.DataAccess;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class DrdController : Controller
    {
        private readonly LitTextyDbContext Context;

        public DrdController(LitTextyDbContext context) 
        {
            Context = context ?? throw new ArgumentNullException();
        }

        public IActionResult Index()
        {
            return View(Context);
        }

        public IActionResult Edit(int id)
        {
            Hrdina hrdina = Context.Hrdina.FirstOrDefault(h => h.ID == id);
            return View(hrdina);
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        public IActionResult New() 
        { return View(); }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Hrdina created)
        {
            //foreach (var error in Tasks.AddNew(created))
            //{
            //    ModelState.AddModelError(error.Key, error.Value);
            //}
            Context.Hrdina.Add(created);
            Context.SaveChanges();

            if (!ModelState.IsValid)
            { return View(); }

            TempData["success"] = created;
            return View("Edit",created);
            //return RedirectToAction(nameof(Index));
        }
    }
}
