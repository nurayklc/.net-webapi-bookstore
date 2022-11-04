using Microsoft.EntityFrameworkCore;
using WebApi.Entity.Book;
using WebApi.Entity.Genre;
using WebApi.Entity.Author;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext 
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}