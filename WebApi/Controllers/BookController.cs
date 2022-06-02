using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.UpdateBook;

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
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        // Get book by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            GetBookQuery query = new GetBookQuery(_context);
            var result = query.Handle(id);
            return Ok(result);
        }

        // FromQuery Example : Okunabilirlik açısından diğer get istekleri daha mantıklıdır.
        /* [HttpGet]
        public Book Get([FromQuery] string id){
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        } */

        // Post Book
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
            
        }

        // Put Book
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook){
           UpdateBookCommand command = new UpdateBookCommand(_context);
           try
           {
               command.Model = updateBook;
               command.Handle(id);
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }
           
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