using System.Net;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers; 

public class CzekoladkiController : Controller {
  private static HttpClient client = new () {
    BaseAddress = new Uri("http://localhost:5112/")
  };
  
  // GET
  public async Task<IActionResult> Index() {
    var result = await client.GetFromJsonAsync<List<Czekoladka>>("/api/czekoladki");
    
    return View(result);
  }
}
