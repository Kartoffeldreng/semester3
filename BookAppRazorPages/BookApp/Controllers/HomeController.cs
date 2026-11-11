using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers;

public class HomeController : Controller {

  public IActionResult Index() {
    return View();
  }
}