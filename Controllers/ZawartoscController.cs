using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Candy_Shop.Controllers
{
  public class ZawartoscController : Controller
  {
    private readonly ApplicationDBContext _context;
    private readonly ILogger<ZawartoscController> _logger;

    public ZawartoscController(ApplicationDBContext context, ILogger<ZawartoscController> logger)
    {
      _context = context;
      _logger = logger;
    }

    // GET: Zawartosc/Create
    public IActionResult Create()
    {
      ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa");
      return View();
    }


    // POST: Zawartosc/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,sztuk,id_czekoladki")] Zawartosc zawartosc)
    {
      _logger.LogInformation("Create action triggered.");
      if (ModelState.IsValid)
      {
        _context.Add(zawartosc);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      _logger.LogInformation($"Received zawartosc: {zawartosc}");

      ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
      return View(zawartosc);
    }


    // GET: Zawartosc/Index
    public async Task<IActionResult> Index()
    {
      var candyShopContext = _context.Zawartosc.Include(z => z.Czekoladka);
      return View(await candyShopContext.ToListAsync());
    }
  }
}
