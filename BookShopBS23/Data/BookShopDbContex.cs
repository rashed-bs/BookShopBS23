using BookShopBS23.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopBS23.Data
{
    public class BookShopDbContex : DbContext
    {
        public BookShopDbContex(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}
