using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candy_Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDBContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Order/Index
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
                var orders = await _context.Orders
                    .Include(o => o.Zawartosc)
                    .ThenInclude(z => z.Czekoladka)
                    .ToListAsync();

                if (orders.Count == 0)
                {
                    ViewData["EmptyMessage"] = "Brak zamówień";
                }

                return View(orders);
            }
            else
            {
                var orders = await _context.Orders
                    .Include(o => o.Zawartosc)
                    .ThenInclude(z => z.Czekoladka)
                    .Where(o => o.username == username)
                    .ToListAsync();

                if (orders.Count == 0)
                {
                    ViewData["EmptyMessage"] = "Brak zamówień";
                }

                return View(orders);
            }
        }

        // POST: Zawartosc/AddOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrder(int zawartoscId)
        {
            var username = UserSession.GetUsername(HttpContext.Session);
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("AddOrder action: User not logged in.");
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                var zawartosc = await _context.Zawartosc.SingleOrDefaultAsync(z => z.id == zawartoscId && z.username == username);
                if (zawartosc == null)
                {
                    _logger.LogWarning("AddOrder action: Zawartosc not found for user.");
                    return NotFound();
                }

                var order = new Order
                {
                    username = username,
                    id_zawartosci = zawartosc.id
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                TempData["OrderMessage"] = "Zamówienie zostało złożone";
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while placing the order: {ex.Message}");
                TempData["ErrorMessage"] = "Wystąpił błąd podczas składania zamówienia. Spróbuj ponownie.";
            }

            return RedirectToAction("Index", "Zawartosc");
        }
    }
}
