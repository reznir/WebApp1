using Microsoft.AspNetCore.Mvc;
using WebApp1.DataAccess;
using WebApp1.Models;
using WebApp1.Models.Database;

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
            Hrdina hrdina = Context.Hrdina.First(h => h.ID == id);
            List<HrdinaPovolani> povolaniList = Context.HrdinaPovolani.Where(p => p.HrdinaId == hrdina.ID).ToList();
            List<int> profeseId = povolaniList.Select(p => p.PovolaniId).ToList();
            List<Povolani> profese = Context.Povolani.Where(p => profeseId.Contains(p.ID)).ToList();
            List<int> schopnostiId = Context.HrdinaSchopnosts.Where(s => s.HrdinaId == hrdina.ID).Select(s => s.SchopnostId).ToList();
            List<Schopnost> schopnosti = Context.Schopnost.Where(s => schopnostiId.Contains(s.ID)).ToList();
            HrdinaModel model = new()
            {
                Hrdina = hrdina,
                Povolani = profese,
                HrdinovaPovolani = povolaniList,
                Schopnosti = schopnosti                
            };
            return View(model);
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

            //if (!ModelState.IsValid)
            //{ return View(); }

            TempData["success"] = created;
            return View("Edit",created);
            //return RedirectToAction(nameof(Index));
        }
    }
}
