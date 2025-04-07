using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A16TPWebAppEvgeniiMorgunov.Data;
using A16TPWebAppEvgeniiMorgunov.Models;

namespace A16TPWebAppEvgeniiMorgunov.Controllers
{
    public class BasketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BasketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Baskets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Baskets
                .Include(b => b.User)
                .Include(b => b.BasketLivres)
                .ThenInclude(bl => bl.Livre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Baskets/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.User)
                .Include(b => b.BasketLivres)
                .ThenInclude(bl => bl.Livre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // GET: Baskets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Baskets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", basket.UserId);
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", basket.UserId);
            return View(basket);
        }

        // POST: Baskets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId")] Basket basket)
        {
            if (id != basket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketExists(basket.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", basket.UserId);
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.User)
                .Include(b => b.BasketLivres)
                .ThenInclude(bl => bl.Livre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketLivres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (basket != null)
            {
                // Supprimer d'abord les relations dans BasketLivre
                if (basket.BasketLivres != null && basket.BasketLivres.Any())
                {
                    _context.BasketLivres.RemoveRange(basket.BasketLivres);
                }

                _context.Baskets.Remove(basket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Baskets/AddLivre
        public async Task<IActionResult> AddLivre(long? basketId)
        {
            if (basketId == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == basketId);

            if (basket == null)
            {
                return NotFound();
            }

            ViewData["BasketId"] = basketId;
            ViewData["LivreId"] = new SelectList(_context.Livres.Where(l => l.Quantity > 0), "Id", "Title");
            return View();
        }

        // POST: Baskets/AddLivre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLivre(long basketId, long livreId, int quantity = 1)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketLivres)
                .FirstOrDefaultAsync(b => b.Id == basketId);

            var livre = await _context.Livres.FindAsync(livreId);

            if (basket == null || livre == null || livre.Quantity < quantity)
            {
                return BadRequest();
            }

            // Vérifier si le livre est déjà dans le panier
            var basketLivre = await _context.BasketLivres
                .FirstOrDefaultAsync(bl => bl.BasketId == basketId && bl.LivreId == livreId);

            if (basketLivre != null)
            {
                // Mettre à jour la quantité
                basketLivre.Quantity += quantity;
            }
            else
            {
                // Ajouter le nouveau livre au panier
                basketLivre = new BasketLivre
                {
                    BasketId = basketId,
                    LivreId = livreId,
                    Quantity = quantity
                };

                _context.BasketLivres.Add(basketLivre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = basketId });
        }

        // POST: Baskets/RemoveLivre
        [HttpPost]
        public async Task<IActionResult> RemoveLivre(long basketId, long livreId)
        {
            var basketLivre = await _context.BasketLivres
                .FirstOrDefaultAsync(bl => bl.BasketId == basketId && bl.LivreId == livreId);

            if (basketLivre == null)
            {
                return NotFound();
            }

            _context.BasketLivres.Remove(basketLivre);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = basketId });
        }

        private bool BasketExists(long id)
        {
            return _context.Baskets.Any(e => e.Id == id);
        }
    }
}