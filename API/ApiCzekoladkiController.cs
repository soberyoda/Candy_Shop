using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candy_Shop.API;

[ApiController]
[Route("api/czekoladki/")]
public class ApiCzekoladkiController(ApplicationDBContext context) : ControllerBase {
  
  // GET: /api/czekoladki/
  [HttpGet]
  public async Task<IEnumerable<Czekoladka>> Get() {
    return await context.Czekoladki.ToListAsync();
  }
}
