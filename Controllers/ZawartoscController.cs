using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Candy_Shop.Utilities;

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
            var username = UserSession.GetUsername(HttpContext.Session);
            var zawartosc = new Zawartosc
            {
                username = username
            };

            ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa");
            return View(zawartosc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,sztuk,id_czekoladki,username")] Zawartosc zawartosc)
        {
            _logger.LogInformation("Create action triggered.");
            _logger.LogInformation($"ModelState.IsValid: {ModelState.IsValid}");
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Error: {error.ErrorMessage}");
                    }
                }
                ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
                return View(zawartosc);
            }

            // Retrieve the User object based on the username
            var user = await _context.Users.SingleOrDefaultAsync(u => u.username == zawartosc.username);
            if (user == null)
            {
                ModelState.AddModelError("username", "Invalid username.");
                ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
                return View(zawartosc);
            }

            zawartosc.User = user;

            if (ModelState.IsValid)
            {
                _context.Add(zawartosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Czekoladki"] = new SelectList(_context.Czekoladki, "id", "nazwa", zawartosc.id_czekoladki);
            return View(zawartosc);
        }

        // GET: Zawartosc/Index
        public async Task<IActionResult> Index()
        {
          var username = UserSession.GetUsername(HttpContext.Session);
          if (string.IsNullOrEmpty(username))
          {
            _logger.LogWarning("Index action: User not logged in.");
            return RedirectToAction("Login", "Auth");
          }

          if (UserSession.isAdmin(HttpContext.Session))
          {
            var zawartoscItems = await _context.Zawartosc
              .Include(z => z.Czekoladka)
              .ToListAsync();

            if (zawartoscItems.Count == 0)
            {
              ViewData["EmptyMessage"] = "Brak danych";
            }

            return View(zawartoscItems);
          }
          else
          {
            var zawartoscItems = await _context.Zawartosc
              .Include(z => z.Czekoladka)
              .Where(z => z.username == username)
              .ToListAsync();

            if (zawartoscItems.Count == 0)
            {
              ViewData["EmptyMessage"] = "Dodaj produkty";
            }

            return View(zawartoscItems);
          }
        }


        // GET: Zawartosc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
          if (id == null)
          {
            return NotFound();
          }

          var username = UserSession.GetUsername(HttpContext.Session);
          if (string.IsNullOrEmpty(username))
          {
            _logger.LogWarning("Delete action: User not logged in.");
            return RedirectToAction("Login", "Auth");
          }

          if (!UserSession.isAdmin(HttpContext.Session))
          {
            _logger.LogWarning("Delete action: User is not an admin.");
            return RedirectToAction(nameof(Index));
          }

          var zawartosc = await _context.Zawartosc.FirstOrDefaultAsync(m => m.id == id);
          if (zawartosc == null)
          {
            return NotFound();
          }

          return View(zawartosc);
        }

// POST: Zawartosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          var username = UserSession.GetUsername(HttpContext.Session);
          if (string.IsNullOrEmpty(username))
          {
            _logger.LogWarning("DeleteConfirmed action: User not logged in.");
            return RedirectToAction("Login", "Auth");
          }

          if (!UserSession.isAdmin(HttpContext.Session))
          {
            _logger.LogWarning("DeleteConfirmed action: User is not an admin.");
            return RedirectToAction(nameof(Index));
          }

          var zawartosc = await _context.Zawartosc.FirstOrDefaultAsync(z => z.id == id);
          if (zawartosc == null)
          {
            return NotFound();
          }

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
    
          var username = UserSession.GetUsername(HttpContext.Session);
          if (string.IsNullOrEmpty(username))
          {
            _logger.LogWarning("Edit action: User not logged in.");
            return RedirectToAction("Login", "Auth");
          }

          if (!UserSession.isAdmin(HttpContext.Session))
          {
            _logger.LogWarning("Edit action: User is not an admin.");
            return RedirectToAction(nameof(Index));
          }

          var zawartosc = await _context.Zawartosc.FirstOrDefaultAsync(z => z.id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,sztuk,id_czekoladki,username")] Zawartosc zawartosc)
        {
          if (id != zawartosc.id)
          {
            return NotFound();
          }

          var username = UserSession.GetUsername(HttpContext.Session);
          if (string.IsNullOrEmpty(username))
          {
            _logger.LogWarning("Edit action: User not logged in.");
            return RedirectToAction("Login", "Auth");
          }

          // Pobranie pierwotnego właściciela rekordu
          var originalOwner = await _context.Zawartosc.Where(z => z.id == id).Select(z => z.username).FirstOrDefaultAsync();

          // Sprawdzenie czy użytkownik jest administratorem lub właścicielem rekordu
          if (!UserSession.isAdmin(HttpContext.Session) && originalOwner != username)
          {
            _logger.LogWarning("Edit action: Unauthorized access attempted by user: {username}");
            return Unauthorized();
          }

          if (ModelState.IsValid)
          {
            try
            {
              // Zachowanie pierwotnego właściciela rekordu
              zawartosc.username = originalOwner;

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
            var username = UserSession.GetUsername(HttpContext.Session);

            try
            {
                // Clear the Zawartosc table for the specific user
                var zawartoscItems = _context.Zawartosc.Where(z => z.username == username);
                _context.Zawartosc.RemoveRange(zawartoscItems);
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
