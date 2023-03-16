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
            TempData["success"] = cipher.Encrypt();
            return View("StartEnd", cipher);
        }

        [HttpPost]
        public IActionResult AtoN(AtoN cipher)
        {
            
            TempData["success"] = cipher.Encrypt();
            return View("AtoN",cipher);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}