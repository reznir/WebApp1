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

        public IActionResult TeloJizva(int hrdinaId)
        {
            bool cure = hrdinaId < 0;
            hrdinaId = Math.Abs(hrdinaId);
            var model = CreateHrdinaModel(hrdinaId);
            if (cure)
            {
                if (model.Hrdina.TeloJizva > 0)
                    model.Hrdina.TeloJizva--;
            }
            else
            {
                model.Hrdina.TeloJizva++;
            }
            Context.SaveChanges();
            return View("Edit", model);
        }

        public IActionResult DuseJizva(int hrdinaId)
        {
            bool cure = hrdinaId < 0;
            hrdinaId = Math.Abs(hrdinaId);
            var model = CreateHrdinaModel(hrdinaId);
            if (cure)
            {
                if (model.Hrdina.DuseJizva > 0)
                { model.Hrdina.DuseJizva--; }
            }
            else
            {
                model.Hrdina.DuseJizva++;
            }
            Context.SaveChanges();
            return View("Edit", model);
        }
        public IActionResult VlivJizva(int hrdinaId)
        {
            bool cure = hrdinaId < 0;
            hrdinaId = Math.Abs(hrdinaId);
            var model = CreateHrdinaModel(hrdinaId);
            if (cure)
            {
                if (model.Hrdina.VlivJizva > 0)
                    model.Hrdina.VlivJizva--;
            }
            else
            {
                model.Hrdina.VlivJizva++;
            }
            Context.SaveChanges();
            return View("Edit", model);
        }

        public IActionResult Edit(int id)
        {
            var model = CreateHrdinaModel(id);
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
            try
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
                var model = CreateHrdinaModel(created.ID);
                return View("Index", Context.Hrdina.ToList());
            }
            catch (Exception ex)
            {
                TempData["error"] = ex;
                return View("Error");
            }
            //return RedirectToAction(nameof(Index));
        }

        HrdinaModel CreateHrdinaModel(int hrdinaId)
        {
            Hrdina hrdina = Context.Hrdina.First(h => h.ID == hrdinaId);
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

            return model;
        }
    }
}
