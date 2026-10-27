using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trafiklys.Models;

namespace Trafiklys.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View(model:"Rød");
        }

        [HttpPost]
        public IActionResult Index(string colour, string command) {
            colour = command switch {
                "Red" => "Rød",
                "Yellow" => "Gul",
                "Green" => "Grøn",
                "Drive" => "Du kørte!",
                _ => colour
            };

            return View(model: colour);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}