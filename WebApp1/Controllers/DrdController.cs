using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
                { model.Hrdina.Penize = Math.Round(decimal.Parse(formData[nameof(Hrdina.Penize)]), 2); }

                if (formData.ContainsKey($"{nameof(Hrdina)}.{nameof(Hrdina.Zbrane)}"))
                { model.Hrdina.Zbrane = formData[$"{nameof(Hrdina)}.{nameof(Hrdina.Zbrane)}"]; }
                if (formData.ContainsKey($"{nameof(Hrdina)}.{nameof(Hrdina.Vybaveni)}"))
                { model.Hrdina.Vybaveni = formData[$"{nameof(Hrdina)}.{nameof(Hrdina.Vybaveni)}"]; }

                if (formData.ContainsKey(nameof(Hrdina.Suroviny)))
                { model.Hrdina.Suroviny = int.Parse(formData[nameof(Hrdina.Suroviny)]); }

                var a = formData["zkusenosti"];
                if (int.TryParse(formData[nameof(Hrdina.Zkusenosti)], out int zkusenosti))
                { model.Hrdina.Zkusenosti += zkusenosti; }//.Parse(formData[nameof(Hrdina.Zkusenosti)]); }

                Context.SaveChanges();
                return View("Play", model);
            }
            List<Hrdina> hrdinove = Context.Hrdina.ToList();
            return View("Index", hrdinove);
        }

        public IActionResult Edit(int id)
        {
            var model = CreateHrdinaModel(id);
            AddableAbilities(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formData)
        {
            int hrdinaId;
            if (formData.ContainsKey("hrdina"))
            {
                hrdinaId = int.Parse(formData["hrdina"]);
                HrdinaModel model = CreateHrdinaModel(hrdinaId);
                //limity vlasnosti
                if (formData.ContainsKey(nameof(Hrdina.TeloLimit)))
                { model.Hrdina.TeloLimit = int.Parse(formData[nameof(Hrdina.TeloLimit)]); }
                if (formData.ContainsKey(nameof(Hrdina.DuseLimit)))
                { model.Hrdina.DuseLimit = int.Parse(formData[nameof(Hrdina.DuseLimit)]); }
                if (formData.ContainsKey(nameof(Hrdina.VlivLimit)))
                { model.Hrdina.VlivLimit = int.Parse(formData[nameof(Hrdina.VlivLimit)]); }

                //povolani
                if (formData.ContainsKey("AddPovolani"))
                {
                    var novePovolani = new HrdinaPovolani()
                    {
                        HrdinaId = hrdinaId,
                        PovolaniId = int.Parse(formData[nameof(Povolani)]),
                        Level = 1
                    };
                    //model.HrdinovaPovolani.Add(novePovolani);
                    Context.HrdinaPovolani.Add(novePovolani);
                }
                for (int i = 0; i < 15; i++)            //15 je maximalni pocet povolani
                {
                    string key = $"HrdinovaPovolani[{i}].Level";
                    if (formData.ContainsKey(key))
                    {
                        model.HrdinovaPovolani[i].Level = int.Parse(formData[key]);
                    }
                }

                //schopnost
                if (formData.ContainsKey("AddSchopnost"))
                {
                    var novaSchonpost = new HrdinaSchopnost()
                    {
                        HrdinaId = hrdinaId,
                        SchopnostId = int.Parse(formData[nameof(model.Schopnosti)])
                    };
                    Context.HrdinaSchopnosts.Add(novaSchonpost);
                }

                Context.SaveChanges();

                model = CreateHrdinaModel(hrdinaId);

                if (formData.ContainsKey("Save"))
                { return View("Index", Context.Hrdina.ToList()); }
                else
                {
                    AddableAbilities(model);
                    return View(model);
                }

            }
            List<Hrdina> hrdinove = Context.Hrdina.ToList();
            return View("Index", hrdinove);
        }

        /// <summary>
        /// Pridava do ViewBag seznam Povolani a Schopnosti, ktere muze hrdina v model pridat ke svym moznostem
        /// </summary>
        /// <param name="model">Obsahuje informace co uz hrdina ma. Na tomto zaklade se naplni ViewBag temi co si jeste hrdina muze pridat</param>
        private void AddableAbilities(HrdinaModel model)
        {
            List<int> hrdinovaPovolaniIds = model.Povolani.Select(p => p.ID).ToList();
            var schopnosts = Context.Schopnost.Where(s => hrdinovaPovolaniIds.Contains(s.PovolaniId) && !model.Schopnosti.Contains(s)).OrderBy(s => s.Name).ToList();
            ViewBag.AllSchopnost = schopnosts;
            ViewBag.PossiblePovolani = Context.Povolani.Where(p => model.PossiblePovolani(model.HrdinovaPovolani).Contains(p.ID));
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

        public IActionResult NewSchopnost()
        {
            ViewBag.Povolani = Context.Povolani.ToList();
            ViewBag.Vlastnosti = new List<string>() { "Tělo", "Duše", "Vliv" };

            return View();
        }

        [HttpPost]
        public IActionResult NewSchopnost(Schopnost schopnost)
        {
            Context.Schopnost.Add(schopnost);
            Context.SaveChanges();
            return View("Index", Context.Hrdina.ToList());
        }
    }
}
