using System.Net;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers; 

public class CzekoladkiController : Controller {
  
  // GET
  public async Task<IActionResult> Index() {
    var result = await ApiClient.Get<List<Czekoladka>>("/api/czekoladki");
    
    return View(result);
  }
}
