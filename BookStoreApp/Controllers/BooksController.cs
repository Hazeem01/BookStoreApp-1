using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Data;
using Microsoft.EntityFrameworkCore;

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
