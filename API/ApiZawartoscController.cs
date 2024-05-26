using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.API;

[Route("api/[controller]")]
[ApiController]
public class ApiZawartoscController : ControllerBase
{
  private readonly ApplicationDBContext _context;

  public ApiZawartoscController(ApplicationDBContext context)
  {
    _context = context;
  }

  // POST: api/ApiZawartosc
  [HttpPost]
  public async Task<IActionResult> PostZawartosc(Zawartosc zawartosc)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    var czekoladka = await _context.Czekoladki.FindAsync(zawartosc.id_czekoladki);

    if (czekoladka == null)
    {
      return BadRequest("Invalid czekoladka ID");
    }
    
    zawartosc.Czekoladka = czekoladka;

    _context.Zawartosc.Add(zawartosc);
    await _context.SaveChangesAsync();

    return CreatedAtAction("", new { id = zawartosc.id }, zawartosc);
  }

}