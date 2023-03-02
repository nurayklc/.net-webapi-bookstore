using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entity;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext dbContext)
        {
            dbContext.Books.AddRange(
             new Book
             {
                 //Id = 1,
                 Title = "Yalnızız",
                 GenreId = 1, // Roman
                 PageCount = 250,
                 PublishDate = new DateTime(2001, 01, 12),
                 AuthorId = 1
             },
             new Book
             {
                 //Id = 2,
                 Title = "Veba Geceleri",
                 GenreId = 1, // Roman
                 PageCount = 500,
                 PublishDate = new DateTime(1990, 11, 02),
                 AuthorId = 2
             },
             new Book
             {
                 //Id = 3,
                 Title = "Yaşamanın Anlam ve Amacı",
                 GenreId = 2, // Psikolojik
                 PageCount = 354,
                 PublishDate = new DateTime(2008, 10, 05),
                 AuthorId = 3
             });
        }
    }
}
