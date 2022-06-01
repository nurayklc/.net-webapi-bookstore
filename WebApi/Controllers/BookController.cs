using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    { 
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        // Get all books
        [HttpGet]
        public List<Book> GetBooks(){
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        // Get book by id
        [HttpGet("{id}")]
        public Book GetById(int id){
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
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
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null)
                return BadRequest();
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        // Put Book
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book is null)
                return BadRequest();
            
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();
        }

        // Delete Book
        [HttpDelete("{id}")]
        
        public IActionResult DeleteBook(int id, [FromBody] Book deleteBook){
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();
            
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}