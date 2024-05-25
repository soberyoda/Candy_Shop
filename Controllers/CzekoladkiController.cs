using System.Net;
using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers; 

public class CzekoladkiController : Controller {
  private readonly ApplicationDBContext _context;

  public CzekoladkiController(ApplicationDBContext context) {
    _context = context;
  }

  // GET
  public async Task<IActionResult> Index() {
    var result = await ApiClient.Get<List<Czekoladka>>("/api/czekoladki");
    
    return View(result);
  }
  // POST
  [HttpPost]
  public async Task<ActionResult<Czekoladka>> Post(Czekoladka czekoladka) {
    if (ModelState.IsValid) {
      _context.Czekoladki.Add(czekoladka);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(Index), new { id = czekoladka.Id }, czekoladka);
    }

    return BadRequest(ModelState);
  }

}
