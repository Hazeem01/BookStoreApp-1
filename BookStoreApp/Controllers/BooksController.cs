using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Data;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Models;

namespace BookStoreApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDBContext appDBcontext;

        public BooksController(AppDBContext context)
        {
            appDBcontext = context;
        }

        public async Task<IActionResult> Index()
        { var books = await appDBcontext.Books.ToListAsync();
            return View(books);
        }

        [HttpPost]

        public async Task<IActionResult> CreateBook(Models.Book book)
        {
            appDBcontext.Add(book);
            await appDBcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var book = await appDBcontext.Books.FirstOrDefaultAsync(bI => bI.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await appDBcontext.Books.FindAsync(id);
            if (book != null)
            {
                appDBcontext.Books.Remove(book);
                await appDBcontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { }

            var book = await appDBcontext.Books.FindAsync(id);

            if (book == null)
            {
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    appDBcontext.Update(book);
                    await appDBcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } 
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View(book);
        }

        [HttpGet]

        public IActionResult CreateBook()
        {
            return View();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
