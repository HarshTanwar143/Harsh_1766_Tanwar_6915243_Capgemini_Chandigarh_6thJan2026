using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingApp.Data;
using PublishingApp.Models;

namespace PublishingApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .ToListAsync();

            return View(books);
        }

        // GET: /Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();
            return View(book);
        }

        // GET: /Books/Create
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            return View();
        }

        // POST: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, int[] selectedAuthors)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                // Add selected authors to join table
                foreach (var authorId in selectedAuthors)
                {
                    _context.AuthorBooks.Add(new AuthorBook
                    {
                        BookId = book.Id,
                        AuthorId = authorId
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = _context.Authors.ToList();
            return View(book);
        }

        // GET: /Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books
                .Include(b => b.AuthorBooks)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.SelectedAuthors = book.AuthorBooks.Select(ab => ab.AuthorId).ToList();
            return View(book);
        }

        // POST: /Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, int[] selectedAuthors)
        {
            if (id != book.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Update(book);

                // Remove old authors and re-add selected ones
                var existing = _context.AuthorBooks.Where(ab => ab.BookId == id);
                _context.AuthorBooks.RemoveRange(existing);

                foreach (var authorId in selectedAuthors)
                {
                    _context.AuthorBooks.Add(new AuthorBook
                    {
                        BookId = id,
                        AuthorId = authorId
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = _context.Authors.ToList();
            return View(book);
        }

        // GET: /Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();
            return View(book);
        }

        // POST: /Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}