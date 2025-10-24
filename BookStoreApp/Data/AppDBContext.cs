namespace BookStoreApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using BookStoreApp.Models;
    public class AppDBContext: DbContext
    {
      public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
