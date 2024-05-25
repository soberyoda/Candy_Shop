using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candy_Shop.API;

[ApiController]
[Route("api/czekoladki/")]
public class ApiCzekoladkiController : ControllerBase {
  private readonly ApplicationDBContext _context;

  public ApiCzekoladkiController(ApplicationDBContext context) {
    _context = context;
  }

  // GET: /api/czekoladki/
  [HttpGet]
  public async Task<IEnumerable<Czekoladka>> Get() {
    return await _context.Czekoladki.ToListAsync();
  }

  // POST: /api/czekoladki/
  [HttpPost]
  public async Task<ActionResult<Czekoladka>> Post(Czekoladka czekoladka) {
    if (ModelState.IsValid) {
      _context.Czekoladki.Add(czekoladka);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(Get), new { id = czekoladka.id }, czekoladka);
    }

    return BadRequest(ModelState);
  }
}
