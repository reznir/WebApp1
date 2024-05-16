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
            return View(CreateSvatkyModel(svatekId, TypId.Svatek));
        }

        /// <summary>
        /// Function for saving new LitText from FORM by button Ulozit
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            if (formCollection.ContainsKey("New"))
            { return View("Edit", CreateSvatkyModel((int)AllNothing.None, TypId.Svatek)); }

            LitText litText = new();
            if (formCollection.ContainsKey("cyklus") && int.TryParse(formCollection["cyklus"], out int cyklus))
            { litText.Cyklus = ((Cyklus)cyklus).ToString(); }

            if (formCollection.ContainsKey("text"))
            { litText.Text = formCollection["text"].ToString(); }

            if (formCollection.ContainsKey("date") && DateTime.TryParse(formCollection["date"], out DateTime date))
            {
                LiturgickyRok litRok = new LiturgickyRok(date);
                litText.SvatekId = litRok.GetSvatekId(date);
                litText.Nazev_dne = litRok.NazevDne;
            }

            ViewBag.NazevDne = litText.Nazev_dne;

            context.LitText.Add(litText);
            context.SaveChanges();

            return View(CreateSvatkyModel((int)AllNothing.All, TypId.Svatek));
        }

        /// <summary>
        /// Creates model for most views used in this controller
        /// </summary>
        /// <param name="id">ID of Doba, Svatek or LitText</param>
        /// <param name="typId">Typ of ID specified by <see cref="TypId"/></param>
        /// <param name="cyklus"><see cref="Cyklus"/> to search by. Can be null to search through all 3 cycles (ignore cycle asignment)</param>
        /// <returns><see cref="SvatkyModel"/>which contains list of Doby, list of Svatky and list of LitTexts matching searching criterie (ID)</returns>
        private SvatkyModel CreateSvatkyModel(int id, TypId typId, Cyklus? cyklus = null)
        {
            List<LitText> litTexts = new();

            switch (id)
            {
                case (int)AllNothing.None:
                    litTexts = new();
                    break;
                case (int)AllNothing.All:
                    litTexts = context.LitText.ToList();
                    break;
                default:
                    switch (typId)
                    {
                        case TypId.Doba:
                            if (cyklus != null)
                            { litTexts = context.LitText.Where(t => t.Svatek.DobaId == id && t.Cyklus == cyklus.ToString()).ToList(); }
                            else
                            { litTexts = context.LitText.Where(t => t.Svatek.DobaId == id).ToList(); }
                            break;
                        case TypId.Svatek:
                            if (cyklus != null)
                            { litTexts = context.LitText.Where(t => t.SvatekId == id && t.Cyklus == cyklus.ToString()).ToList(); }
                            else
                            { litTexts = context.LitText.Where(t => t.SvatekId == id).ToList(); }
                            break;
                        case TypId.LitText:
                            if (cyklus.HasValue)
                            { litTexts = context.LitText.Where(t => t.Id == id && t.Cyklus == cyklus.ToString()).ToList(); }
                            else
                            { litTexts = context.LitText.Where(t => t.Id == id).ToList(); }
                            break;
                    }
                    break;
            }

            SvatkyModel svatkyModel = new()
            {
                Doby = context.Doba.ToList(),
                Svatky = context.Svatek.ToList(),
                LitTexty = litTexts
            };

            return svatkyModel;
        }


        public IActionResult Edit(int litTextId)
        {
            LitText selectedText = context.LitText.First(s => s.Id == litTextId);
            ViewBag.SvatekId = selectedText.SvatekId;

            if (Enum.TryParse(typeof(Cyklus), selectedText?.Cyklus, true, out object? cyklus)) //&& selectedSvatek?.Svatek?.Cykly ?? false
            { ViewBag.Cyklus = (int)cyklus; }
            else
            { ViewBag.Cyklus = -1; }

            return View(CreateSvatkyModel(litTextId, TypId.LitText));
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

            if (int.TryParse(formCollection["cyklus"], out int cyklusInt))
            { litText.Cyklus = ((Cyklus)cyklusInt).ToString(); }
            else
            { litText.Cyklus = null; }

            litText.SvatekId = int.Parse(formCollection["Svatek"]);

            var svatek = context.Svatek.First(s => s.Id == litText.SvatekId);
            if (int.TryParse(formCollection["den"], out int den) && den > -1)
            { litText.Nazev_dne = $"{den} {svatek.Nazev_dne}"; }
            else
            { litText.Nazev_dne = svatek.Nazev_dne; }


            context.SaveChanges();
            return View("Index", CreateSvatkyModel(0, TypId.Doba));
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
