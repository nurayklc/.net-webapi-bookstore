using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace WebApi.DBOperations 
{
    public class DataGenerator 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre {
                        Name = "Personal Growth"
                    },
                    new Genre {
                        Name = "Science Fiction"
                    },
                    new Genre {
                        Name = "Romance"
                    }
                );
                context.Books.AddRange(
                new Book {
                    //Id = 1,
                    Title = "Yalnızız",
                    GenreId = 1, // Roman
                    PageCount = 250,
                    PublishDate = new DateTime(2001, 01, 12)
                },
                new Book {
                    //Id = 2,
                    Title = "Veba Geceleri",
                    GenreId = 1, // Roman
                    PageCount = 500,
                    PublishDate = new DateTime(1990, 11, 02)
                },
                new Book {
                    //Id = 3,
                    Title = "Yaşamanın Anlam ve Amacı",
                    GenreId = 2, // Psikolojik
                    PageCount = 354,
                    PublishDate = new DateTime(2008, 10, 05)
                });
                context.SaveChanges();
            }
        }
    }
}