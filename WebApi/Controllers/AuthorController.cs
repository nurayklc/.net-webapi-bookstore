using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using WebApi.DBOperations;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetBookDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.AuthorOperations.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Queries;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase 
    { 
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all authors
        [HttpGet]
        public IActionResult GetAuthors(){
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Get author by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }
        // Post Author
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newBook){
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newBook;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            
        }

        // Put Author
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id,[FromBody] UpdateAuthorModel updateBook){
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
           command.Model = updateBook;
           command.AuthorId = id;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
           validator.ValidateAndThrow(command);
           command.Handle();
           return Ok();
        }

        // Delete Author
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id){
           DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
           command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
           validator.ValidateAndThrow(command);
           command.Handle();
            return Ok();
        }
    }
}