using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    { 
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all books
        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Get book by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailValidator validator = new GetBookDetailValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
                // ValidationResult result = validator.Validate(command);
                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("Property: " + item.PropertyName + "Error Message: " + item.ErrorMessage);
                // else
                //     command.Handle();
            return Ok();
            
        }

        // Put Book
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook){
           UpdateBookCommand command = new UpdateBookCommand(_context);
           command.Model = updateBook;
           command.BookId = id;
           UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
           validator.ValidateAndThrow(command);
           command.Handle();
           return Ok();
        }

        // Delete Book
        [HttpDelete("{id}")]
        
        public IActionResult DeleteBook(int id){
           DeleteBookCommand command = new DeleteBookCommand(_context);
           command.BookId = id;
           DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
           validator.ValidateAndThrow(command);
           command.Handle();
            return Ok();
        }
    }
}