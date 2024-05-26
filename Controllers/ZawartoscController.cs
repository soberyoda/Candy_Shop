using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Candy_Shop.Controllers {
  public class ZawartoscController : Controller {
    private readonly ApplicationDBContext _context;
    private readonly ILogger<ZawartoscController> _logger;

    public ZawartoscController(ApplicationDBContext context, ILogger<ZawartoscController> logger) {
      _context = context;
      _logger = logger;
    }

    // GET: Zawartosc/Create
    public IActionResult Create() {
      ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa");
      return View();
    }


    // POST: Zawartosc/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,sztuk,id_czekoladki")] Zawartosc zawartosc) {
      _logger.LogInformation("Create action triggered.");
      if (ModelState.IsValid) {
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
      var zawartoscItems = await _context.Zawartosc.Include(z => z.Czekoladka).ToListAsync();
    
      if (zawartoscItems.Count == 0)
      {
        ViewData["EmptyMessage"] = "Dodaj produkty";
      }

      return View(zawartoscItems);
    }


    // GET: Zawartosc/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var zawartosc = await _context.Zawartosc
        .FirstOrDefaultAsync(m => m.id == id);
      if (zawartosc == null) {
        return NotFound();
      }

      return View(zawartosc);
    }

    // POST: Zawartosc/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      var zawartosc = await _context.Zawartosc.FindAsync(id);
      _context.Zawartosc.Remove(zawartosc);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    // GET: Zawartosc/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var zawartosc = await _context.Zawartosc.FindAsync(id);
      if (zawartosc == null)
      {
        return NotFound();
      }

      ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
      return View(zawartosc);
    }

// POST: Zawartosc/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("id,sztuk,id_czekoladki")] Zawartosc zawartosc)
    {
      if (id != zawartosc.id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(zawartosc);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ZawartoscExists(zawartosc.id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
      return View(zawartosc);
    }
    private bool ZawartoscExists(int id)
    {
      return _context.Zawartosc.Any(e => e.id == id);
    }
    // POST: Zawartosc/Order
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Order()
    {
      try
      {
        // Clear the Zawartosc table
        _context.Zawartosc.RemoveRange(_context.Zawartosc);
        await _context.SaveChangesAsync();
        // Set a message indicating the order has been placed
        TempData["OrderMessage"] = "Zamówienie zostało złożone";
      }
      catch (Exception ex)
      {
        _logger.LogError($"An error occurred while placing the order: {ex.Message}");
        TempData["ErrorMessage"] = "Wystąpił błąd podczas składania zamówienia. Spróbuj ponownie.";
      }

      return RedirectToAction(nameof(Index));
    }


  }
  
}
