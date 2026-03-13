using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingApp.Data;
using PublishingApp.Models;

namespace PublishingApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors
                .Include(a => a.AuthorBooks)
                    .ThenInclude(ab => ab.Book)
                .ToListAsync();

            return View(authors);
        }

        // GET: /Authors/Create
        public IActionResult Create() => View();

        // POST: /Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: /Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: /Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: /Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: /Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}