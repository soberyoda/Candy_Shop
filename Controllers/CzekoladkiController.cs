using System.Linq; 
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

  // Sort by name
  public IActionResult SortByName() {
    var chocolates = _context.Czekoladki.ToList().OrderBy(c => c.nazwa).ToList();
    return View("Index", chocolates);
  }

  // Sort by price
  public IActionResult SortByPrice() {
    var chocolates = _context.Czekoladki.ToList().OrderBy(c => c.cena).ToList();
    return View("Index", chocolates);
  }
  
  // Orzechy laskowe 
  public IActionResult Hazelnut() {
    var chocolates = _context.Czekoladki.ToList()
      .Where(c => c.orzechy == Candy_Shop.Models.Czekoladka.Orzechy.laskowe)
      .OrderBy(c => c.nazwa)
      .ToList(); 
    return View("Index", chocolates);
  }
  
  // Orzechy laskowe 
  public IActionResult Almonds() {
    var chocolates = _context.Czekoladki.ToList()
      .Where(c => c.orzechy == Candy_Shop.Models.Czekoladka.Orzechy.migdały)
      .OrderBy(c => c.nazwa)
      .ToList(); 
    return View("Index", chocolates);
  }
  
  // Milk chocolate 
  public IActionResult MilkChocolate() {
    var chocolates = _context.Czekoladki.ToList()
      .Where(c => c.czekolada == Candy_Shop.Models.Czekoladka.Czekolada.Mleczna)
      .OrderBy(c => c.nazwa)
      .ToList(); 
    return View("Index", chocolates);
  }
  
  // Dark chocolate 
  public IActionResult DarkChocolate() {
    var chocolates = _context.Czekoladki.ToList()
      .Where(c => c.czekolada == Candy_Shop.Models.Czekoladka.Czekolada.Gorzka)
      .OrderBy(c => c.nazwa)
      .ToList(); 
    return View("Index", chocolates);
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

      return CreatedAtAction(nameof(Index), new { id = czekoladka.id }, czekoladka);
    }

    return BadRequest(ModelState);
  }
}
