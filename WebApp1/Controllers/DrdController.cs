using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;
using WebApp1.DataAccess;
using WebApp1.Models;
using WebApp1.Models.Database;
using WebApp1.Models.Drd;

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
                { model.Hrdina.Penize = Math.Round(double.Parse(formData[nameof(Hrdina.Penize)]), 2); }

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
            HrdinaModel model = SaveEditedHrdina(formData);

            if (formData.ContainsKey("Save") || model == null)
            { return View("Index", Context.Hrdina.ToList()); }
            else
            {
                AddableAbilities(model);
                return View(model);
            }
        }

        /// <summary>
        /// Pridava do ViewBag seznam Povolani a Schopnosti, ktere muze hrdina v model pridat ke svym moznostem.
        /// Pro Edit View.
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
            //Context.Hrdina.Remove(Context.Hrdina.First(p => p.ID == id));
            Context.Hrdina.First(h => h.ID == id).Name = string.Concat(Context.Hrdina.First(h => h.ID == id).Name, " deleted");
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

                //TempData["success"] = created;
                var model = CreateHrdinaModel(created.ID);
                var list = Context.Hrdina.ToList();
                return View("Index", list);
                //return View();
            }
            catch (Exception ex)
            {
                //TempData["error"] = ex;
                return View("Error");
            }
            //return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Creates HrdinaModel which contains Hrdina, his Povolani and connection table for the hrdina, his Schopnosti
        /// </summary>
        /// <param name="hrdinaId">ID of Hrdina in database</param>
        /// <returns>HrdinaModel for most views</returns>
        HrdinaModel CreateHrdinaModel(int hrdinaId)
        {
            Hrdina hrdina = Context.Hrdina.First(h => h.ID == hrdinaId);
            List<HrdinaPovolani> povolaniList = Context.HrdinaPovolani.Where(p => p.HrdinaId == hrdina.ID).ToList();
            List<int> profeseId = povolaniList.Select(p => p.PovolaniId).ToList();
            List<Povolani> profese = Context.Povolani.Where(p => profeseId.Contains(p.ID)).ToList();
            List<int> schopnostiId = Context.HrdinaSchopnosts.Where(s => s.HrdinaId == hrdina.ID).Select(s => s.SchopnostId).ToList();
            List<Schopnost> schopnosti = Context.Schopnost.Where(s => schopnostiId.Contains(s.ID)).OrderBy(s => s.PovolaniId).ToList();

            HrdinaModel model = new()
            {
                Hrdina = hrdina,
                Povolani = profese,
                HrdinovaPovolani = povolaniList,
                Schopnosti = schopnosti
            };

            return model;
        }

        [HttpPost]
        public IActionResult NewSchopnost(Schopnost schopnost, IFormCollection formData)
        {
            //if user clicked NewSchopnost button
            if (formData.ContainsKey("NewSchopnost"))
            {
                //save Hrdina from Edit View (as user could made some changes before creating new Schopnost
                HrdinaModel model = SaveEditedHrdina(formData);
                //Set ViewBag roperties for display in NewSchopnost View
                ViewBag.HrdinaId = model.Hrdina.ID;
                ViewBag.Povolani = Context.Povolani.ToList();
                ViewBag.Vlastnosti = new List<string>() { "Tělo", "Duše", "Vliv" };
                return View();
            }
            else
            {
                int hrdinaId = int.Parse(formData[nameof(Hrdina)]);
                Context.Schopnost.Add(schopnost);
                Context.SaveChanges();
                if (Context.HrdinaPovolani.Any(p => p.HrdinaId == hrdinaId && p.PovolaniId == schopnost.PovolaniId))
                {
                    Context.HrdinaSchopnosts.Add(new HrdinaSchopnost()
                    {
                        HrdinaId = hrdinaId,
                        SchopnostId = schopnost.ID
                    });
                    Context.SaveChanges();
                }
                HrdinaModel model = CreateHrdinaModel(hrdinaId);
                AddableAbilities(model);
                return View("Edit", model);
            }
        }

        /// <summary>
        /// Saves modifications entered in Edit View from its IFormCollection to database.
        /// Returns HrdinaModel used for most Views
        /// </summary>
        /// <param name="formData">IFormCollection from Edit View</param>
        /// <returns>HrdinaModel</returns>
        private HrdinaModel SaveEditedHrdina(IFormCollection formData)
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
                return model;
            }
            return null;
        }


        public IActionResult Postavy(IFormCollection formData)
        {
            var model = new Postavy();
            //from postavy.cshtml
            if (formData.ContainsKey("Created") && formData["Created"].Equals("on"))
            {
                //count sudba
                int s = 0;
                int deleted = 0;
                while (formData.ContainsKey($"sudba {s}"))
                {
                    s++;
                }
                model.Sudba = s;
                if (int.TryParse(formData["AddSudba"], out int plusSudba))
                { model.Sudba += plusSudba; }
                //count ohrozeni
                int i = 0;
                while (formData.ContainsKey(i.ToString()))
                {
                    if (!formData.ContainsKey($"delete {i}"))
                    {
                        model.Postavas.Add(new Postavy.Postava(i - deleted));
                        model.Postavas[i - deleted].Color = formData[$"barva {i}"];
                        model.Postavas[i - deleted].Popis = formData[i.ToString()];
                        int j = 0;
                        while (formData.ContainsKey($"{i} {j}"))
                        {
                            j++;
                        }
                        model.Postavas[i - deleted].Ohrozeni = j;
                    }
                    else { deleted = 1; }
                    i++;
                }
                if (formData.ContainsKey("pridat"))
                {
                    model.Postavas.Add(new Models.Drd.Postavy.Postava(i));
                    model.Postavas[i].Popis = "nova";
                }
            }
            //from Index
            else
            {
                if (formData.ContainsKey("Count"))
                {
                    int count = int.Parse(formData["Count"]);
                    for (int i = 0; i < count; i++)
                    {
                        var postava = new Postavy.Postava(i);
                        if (formData.ContainsKey("Ohrozeni"))
                        { postava.Ohrozeni = int.Parse(formData["Ohrozeni"]); }
                        model.Postavas.Add(postava);
                    }
                    if (formData.ContainsKey("Sudba"))
                    { model.Sudba = int.Parse(formData["Sudba"]) + count - 1; }
                }
            }
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Postavy(IFormCollection formData)
        //{
        //var model = new Postavy();

        //for (int i = 0; i < 10; i++)
        //{
        //    string checkboxName = $"{nameof(Hrdina.Ohrozeni)} {i}";
        //    if (formData.ContainsKey(checkboxName) && formData[checkboxName].Equals("on"))
        //    {
        //        //model.Hrdina.Ohrozeni++;
        //    }
        //}
        //return View(model);
        //}
    }
}
