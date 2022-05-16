using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private static List<Book> BookList = new List<Book>(){
            new Book {
                Id = 1,
                Title = "Yalnızız",
                GenreId = 1, // Roman
                PageCount = 250,
                PublishDate = new DateTime(2001, 01, 12)
            },
            new Book {
                Id = 2,
                Title = "Veba Geceleri",
                GenreId = 1, // Roman
                PageCount = 500,
                PublishDate = new DateTime(1990, 11, 02)
            },
            new Book {
                Id = 3,
                Title = "Yaşamanın Anlam ve Amacı",
                GenreId = 2, // Psikolojik
                PageCount = 354,
                PublishDate = new DateTime(2008, 10, 05)
            }
        };
        // Get all books
        [HttpGet]
        public List<Book> GetBooks(){
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        // Get book by id
        [HttpGet("{id}")]
        public Book GetById(int id){
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        // FromQuery Example : Okunabilirlik açısından diğer get istekleri daha mantıklıdır.
        /* [HttpGet]
        public Book Get([FromQuery] string id){
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        } */

        // Post Book
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook){
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null)
                return BadRequest();
            
            BookList.Add(newBook);
            return Ok();
        }

        // Put Book
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook){
            var book = BookList.SingleOrDefault(x=> x.Id == id);
            if(book is null)
                return BadRequest();
            
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            return Ok();
        }

        // Delete Book
        [HttpDelete("{id}")]
        
        public IActionResult DeleteBook(int id, [FromBody] Book deleteBook){
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();
            
            BookList.Remove(book);
            return Ok();
        }
    }
}