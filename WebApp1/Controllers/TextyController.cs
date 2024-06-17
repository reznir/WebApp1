using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using NuGet.Protocol;
using System.Globalization;
using System.Runtime.Serialization;
using WebApp1.DataAccess;
using WebApp1.Models;
using WebApp1.Models.Texty;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var model = CreateSvatkyModel();
            if (svatekId == 0)
            { model.LitTexty = [.. context.LitText]; }
            else
            {
                model.LitTexty = [.. context.LitText.Where(t => t.SvatekId == svatekId)];
                ViewBag.SvatekId = svatekId;
            }

            return View(model);
        }

        /// <summary>
        /// Displays Edit view or searches by specification of Cyklus, Date and text or combination of those info, based on user selection
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            int svatek = 0;
            if (formCollection.ContainsKey("svatek") && int.TryParse(formCollection["svatek"].ToString(), out int svatekId) && svatekId > 0)
            {
                svatek = svatekId;
                ViewBag.SvatekId = svatekId;
            }

            if (formCollection.ContainsKey("New"))
            { return View("Edit", CreateSvatkyModel()); }
            else if (formCollection.ContainsKey("Clear"))
            {
                var model = CreateSvatkyModel();
                model.LitTexty = [.. context.LitText];
                ViewBag.SvatekId = null;
                return View(model);
            }
            else
            {
                var model = CreateSvatkyModel();

                Cyklus? cyklus = null;
                Logic logicCyklus = Logic.And;
                if (formCollection.ContainsKey("useCyklus") && int.TryParse(formCollection["cyklus"], out int cycleInt) && cycleInt > -1)
                {
                    cyklus = (Cyklus)cycleInt;
                    if (formCollection["logicCykly"].ToString().ToUpper() == Logic.And.ToString().ToUpper())
                    { logicCyklus = Logic.And; }
                    else
                    { logicCyklus = Logic.Or; }
                }

                DateTime? date = null;
                Logic logicDate = Logic.And;
                CultureInfo info = new CultureInfo("en-us");
                if (formCollection.ContainsKey("useDatum") && DateTime.ParseExact(formCollection["date"].ToString(), "yyyy-MM-dd", info) is DateTime formDate)
                {
                    date = formDate;
                    if (formCollection["logicDate"].ToString().ToUpper() == Logic.And.ToString().ToUpper())
                    { logicDate = Logic.And; }
                    else
                    { logicDate = Logic.Or; }
                }

                string text = null;
                if (formCollection.ContainsKey("useText"))
                {
                    text = formCollection["searchText"];
                }

                model.LitTexty = SearchLitTexts(cyklus, logicCyklus, date, logicDate, text, svatek);

                return View(model);
            }

        }

        /// <summary>
        /// Creates model for most views used in this controller
        /// </summary>
        /// <returns><see cref="SvatkyModel"/>which contains list of Doby, list of Svatky and empty list of LitTexts</returns>
        private SvatkyModel CreateSvatkyModel()
        {
            SvatkyModel svatkyModel = new()
            {
                Doby = context.Doba.ToList(),
                Svatky = context.Svatek.ToList(),
                LitTexty = new()
            };

            return svatkyModel;
        }

        /// <summary>
        /// Constructs a query to find LitTexts which meet searching kriteria. If search criteria is null (or 0) it ignores them
        /// </summary>
        /// <param name="cyklus"></param>
        /// <param name="date"></param>
        /// <param name="searchText">full text search from body of PlainText</param>
        /// <param name="svatekId"></param>
        /// <returns>List of LitTexts that meet searching criteria</returns>
        private List<LitText> SearchLitTexts(Cyklus? cyklus, Logic logicCyklus, DateTime? date, Logic logicDate, string searchText, int svatekId = 0)
        {
            IQueryable<LitText> query = context.LitText;

            if (cyklus != null)
            { query = query.Where(t => t.Cyklus == cyklus.ToString()); }

            if (date != null)
            {
                var kalendar = new LiturgickyRok(date.Value);
                kalendar.GetSvatekId(date.Value);
                string den = kalendar.TypSvatku;
                List<int> svateks = context.Svatek.Where(s => s.Nazev_dne.Contains(den)).Select(s => s.Id).ToList();
                if (logicCyklus == Logic.And)
                { query = query.Where(t => svateks.Contains(t.SvatekId)); }
                else
                { query = query.Concat(context.LitText.Where(t => svateks.Contains(t.SvatekId))).Distinct(); }
            }

            if (svatekId > 0)
            { query = query.Where(t => t.SvatekId == svatekId); }

            if (!string.IsNullOrEmpty(searchText))
            {
                if (logicDate == Logic.And)
                { query = query.Where(t => t.PlainText.Contains(searchText)); }
                else
                { query = query.Concat(context.LitText.Where(t => t.PlainText.Contains(searchText))).Distinct(); }
            }

            return query.ToList();
        }


        public IActionResult Edit(int litTextId)
        {
            LitText selectedText = context.LitText.First(s => s.Id == litTextId);
            ViewBag.SvatekId = selectedText.SvatekId;

            if (Enum.TryParse(typeof(Cyklus), selectedText?.Cyklus, true, out object? cyklus)) //&& selectedSvatek?.Svatek?.Cykly ?? false
            { ViewBag.Cyklus = (int)cyklus; }
            else
            { ViewBag.Cyklus = -1; }

            var model = CreateSvatkyModel();
            model.LitTexty = [selectedText];

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection formCollection)
        {
            LitText litText;

            if (int.TryParse(formCollection["LitTextId"], out int litTextId) && context.LitText.First(t => t.Id == litTextId) is LitText editovanyText)
            {
                litText = editovanyText;
                litText.Updated = DateTime.Now;
            }
            else
            {
                litText = new LitText();
                litText.Created = DateTime.Now;
                context.LitText.Add(litText);
            }

            litText.Text = formCollection["text"];
            litText.PlainText = formCollection["textWithoutHtml"];

            if (int.TryParse(formCollection["cyklus"], out int cyklusInt) && cyklusInt > -1)
            { litText.Cyklus = ((Cyklus)cyklusInt).ToString(); }
            else
            { litText.Cyklus = null; }

            //SvatekId by drop down or from Date
            switch (formCollection["selection"])
            {
                case "svatek":
                    if (int.TryParse(formCollection["Svatek"], out int svatekId))
                    { litText.SvatekId = svatekId; }
                    break;
                case "datum":
                    if (DateTime.ParseExact(formCollection["date"].ToString(), "yyyy-MM-dd", new CultureInfo("en-us")) is DateTime formDate)
                    { litText.SvatekId = new LiturgickyRok(formDate).GetSvatekId(formDate); }
                    break;
            }

            //Nazev Dne
            var svatek = context.Svatek.First(s => s.Id == litText.SvatekId);
            if (int.TryParse(formCollection["den"], out int den) && den > -1)
            { litText.Nazev_dne = $"{den} {svatek.Nazev_dne}"; }
            else
            { litText.Nazev_dne = svatek.Nazev_dne; }

            context.SaveChanges();

            var model = CreateSvatkyModel();
            model.LitTexty = [.. context.LitText];
            return View("Index", model);
        }

        private LiturgickyRok FindSvatek(DateTime date)
        {
            var kalendar = new LiturgickyRok(date);
            kalendar.GetSvatekId(date);
            return kalendar;
        }

        public IActionResult Delete(int textId)
        {
            var litText = context.LitText.FirstOrDefault(t => t.Id == textId);
            List<LitText> litTexts = new();
            if (litText != null)
            {
                context.LitText.Remove(litText);
                litTexts = context.LitText.Where(t => t.SvatekId == litText.SvatekId).ToList();
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
            var litTexts = context.LitText.Where(t => t.SvatekId == id).ToList();
            ViewData[nameof(litTexts.Count)] = litTexts.Count;
            ViewData[nameof(litTexts)] = litTexts;
            return RedirectToAction(nameof(Index));
        }
    }
}
