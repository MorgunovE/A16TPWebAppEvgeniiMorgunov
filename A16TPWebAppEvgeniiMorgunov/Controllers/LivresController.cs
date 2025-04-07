using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A16TPWebAppEvgeniiMorgunov.Data;
using A16TPWebAppEvgeniiMorgunov.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace A16TPWebAppEvgeniiMorgunov.Controllers
{
    public class LivresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LivresController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Livres
        public async Task<IActionResult> Index(string searchString, string genre, decimal? minPrice, decimal? maxPrice)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentGenre"] = genre;
            ViewData["CurrentMinPrice"] = minPrice;
            ViewData["CurrentMaxPrice"] = maxPrice;

            ViewData["Genres"] = await _context.Livres
                .Select(l => l.Genre)
                .Distinct()
                .Where(g => g != null)
                .ToListAsync();

            var livres = from l in _context.Livres select l;

            if (!string.IsNullOrEmpty(searchString))
            {
                livres = livres.Where(s => (s.Title != null && s.Title.Contains(searchString))
                                     || (s.Author != null && s.Author.Contains(searchString)));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                livres = livres.Where(l => l.Genre == genre);
            }

            if (minPrice.HasValue)
            {
                livres = livres.Where(l => l.Price >= (double)minPrice);
            }

            if (maxPrice.HasValue)
            {
                livres = livres.Where(l => l.Price <= (double)maxPrice);
            }

            return View(await livres.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,Genre,Price,Quantity")] Livre livre, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("imageFile", "Une image est obligatoire");
                return View(livre);
            }

            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "livre");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                livre.Image = uniqueFileName;

                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livre);
        }


        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            return View(livre);
        }

        // POST: Livres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Description,Author,Genre,Image,Price,Quantity")] Livre livre, IFormFile imageFile)
        {
            if (id != livre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingLivre = await _context.Livres.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
                    string oldImageName = existingLivre?.Image;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "livre");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        if (!string.IsNullOrEmpty(oldImageName))
                        {
                            string oldFilePath = Path.Combine(uploadsFolder, oldImageName);
                            if (System.IO.File.Exists(oldFilePath))
                                System.IO.File.Delete(oldFilePath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        livre.Image = uniqueFileName;
                    }
                    else
                    {
                        livre.Image = oldImageName;
                    }

                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.Id))
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
            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre != null)
            {
                if (!string.IsNullOrEmpty(livre.Image))
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "livre");
                    string filePath = Path.Combine(uploadsFolder, livre.Image);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                _context.Livres.Remove(livre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(long id)
        {
            return _context.Livres.Any(e => e.Id == id);
        }
    }
}