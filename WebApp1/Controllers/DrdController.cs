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
            List<Hrdina> hrdinove = Context.Hrdina.ToList();
            return View(hrdinove);
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
            Context.Hrdina.Remove(Context.Hrdina.First(p => p.ID == id));
            Context.SaveChanges();
            return View("Index", Context.Hrdina.ToList());
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
            created.Telo = created.TeloLimit;
            created.Vliv = created.VlivLimit;
            created.Duse = created.DuseLimit;
            Context.Hrdina.Add(created);
            Context.SaveChanges();

            //if (!ModelState.IsValid)
            //{ return View(); }

            TempData["success"] = created;
            List<int> schopnostiId = Context.HrdinaSchopnosts.Where(s => s.HrdinaId == created.ID).Select(s => s.SchopnostId).ToList();
            List<Schopnost> schopnosti = Context.Schopnost.Where(s => schopnostiId.Contains(s.ID)).ToList();
            List<int> povolaniId = Context.HrdinaPovolani.Where(s => s.PovolaniId == created.ID).Select(s => s.PovolaniId).ToList();
            List<Povolani> povolani = Context.Povolani.Where(s => povolaniId.Contains(s.ID)).ToList();
            HrdinaModel modelHrdina = new() 
            { 
                Hrdina = created,
                Schopnosti = schopnosti,
                Povolani = povolani
            };
            return View("Edit", modelHrdina);
            //return RedirectToAction(nameof(Index));
        }
    }
}
