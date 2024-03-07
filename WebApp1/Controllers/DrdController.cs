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

        public IActionResult Play(int id)
        {
            HrdinaModel hrdinaModel = CreateHrdinaModel(id);
            return View(hrdinaModel);
        }

        [HttpPost]
        public IActionResult Play(IFormCollection formData)
        {
            int hrdinaId;
            if (formData.ContainsKey("hrdina"))
            {
                hrdinaId = int.Parse(formData["hrdina"]);
                HrdinaModel model = CreateHrdinaModel(hrdinaId);
                //ohrozeni
                model.Hrdina.Ohrozeni = 0;
                for (int i = 0; i < 10; i++)
                {
                    string checkboxName = $"{nameof(Hrdina.Ohrozeni)} {i}";
                    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
                    {
                        model.Hrdina.Ohrozeni++;
                    }
                }
                //vyhoda
                model.Hrdina.Vyhoda = 0;
                for (int i = 0; i < 10; i++)
                {
                    string checkboxName = $"{nameof(Hrdina.Vyhoda)} {i}";
                    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
                    {
                        model.Hrdina.Vyhoda++;
                    }
                }
                //telo
                model.Hrdina.Telo = 0;
                for (int i = 0; i < model.Hrdina.TeloLimit; i++)
                {
                    string checkboxName = $"{nameof(Hrdina.Telo)} {i}";
                    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
                    {
                        model.Hrdina.Telo++;
                    }
                }
                //duse
                model.Hrdina.Duse = 0;
                for (int i = 0; i < model.Hrdina.DuseLimit; i++)
                {
                    string checkboxName = $"{nameof(Hrdina.Duse)} {i}";
                    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
                    {
                        model.Hrdina.Duse++;
                    }
                }
                //vliv
                model.Hrdina.Vliv = 0;
                for (int i = 0; i < model.Hrdina.VlivLimit; i++)
                {
                    string checkboxName = $"{nameof(Hrdina.Vliv)} {i}";
                    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
                    {
                        model.Hrdina.Vliv++;
                    }
                }
                //Jizva
                if (formData.ContainsKey("JizvaTelo"))
                { model.Hrdina.TeloJizva++; }
                else if (formData.ContainsKey("LecitTelo") && model.Hrdina.TeloJizva > 0)
                { model.Hrdina.TeloJizva--; }
                else if (formData.ContainsKey("JizvaDuse"))
                { model.Hrdina.DuseJizva++; }
                else if (formData.ContainsKey("LecitDuse") && model.Hrdina.DuseJizva > 0)
                { model.Hrdina.DuseJizva--; }
                else if (formData.ContainsKey("JizvaVliv"))
                { model.Hrdina.VlivJizva++; }
                else if (formData.ContainsKey("LecitVliv") && model.Hrdina.VlivJizva > 0)
                { model.Hrdina.VlivJizva--; }

                if (formData.ContainsKey(nameof(Hrdina.Penize)))
                { model.Hrdina.Penize = Math.Round(decimal.Parse(formData[nameof(Hrdina.Penize)]),2); }

                if (formData.ContainsKey($"{nameof(Hrdina)}.{nameof(Hrdina.Zbrane)}"))
                { model.Hrdina.Zbrane = formData[$"{nameof(Hrdina)}.{nameof(Hrdina.Zbrane)}"]; }
                if (formData.ContainsKey($"{nameof(Hrdina)}.{nameof(Hrdina.Vybaveni)}"))
                { model.Hrdina.Vybaveni = formData[$"{nameof(Hrdina)}.{nameof(Hrdina.Vybaveni)}"]; }

                if (formData.ContainsKey(nameof(Hrdina.Suroviny)))
                { model.Hrdina.Suroviny = int.Parse(formData[nameof(Hrdina.Suroviny)]); }

                Context.SaveChanges();
                return View("Play", model);
            }
            List<Hrdina> hrdinove = Context.Hrdina.ToList();
            return View("Index", hrdinove);
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
