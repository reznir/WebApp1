using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class EncryptController : Controller
    {
        private readonly ILogger<EncryptController> _logger;

        public EncryptController(ILogger<EncryptController> logger)
        {
            _logger = logger;
        }

        public IActionResult AtoN()
        {
            return View();
        }

        public IActionResult StartEnd(StartEnd cipher)
        {
            AssignTempData(cipher);
            return View("StartEnd", cipher);
        }

        [HttpPost]
        public IActionResult AtoN(AtoN cipher)
        {
            AssignTempData(cipher);
            return View("AtoN", cipher);
        }

        public IActionResult Morse(Morseovka cipher)
        {
            AssignTempData(cipher);
            return View("Morseovka", cipher);
        }

        private void AssignTempData(ICipher cipher)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = !string.IsNullOrWhiteSpace(cipher.Encrypt());
                TempData["Encrypted"] = cipher.Encrypted;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}