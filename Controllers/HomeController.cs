using System.Diagnostics;
using Candy_Shop.Attributes;
using Microsoft.AspNetCore.Mvc;
using Candy_Shop.Models;

namespace Candy_Shop.Controllers;

[KeepMessages]
public class HomeController : Controller {
  public IActionResult Index() {
    return View();
  }

  public IActionResult Privacy() {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
