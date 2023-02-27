using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Common;
using WebApi.Entity;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        public readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut!");
            
            book =_mapper.Map<Book>(Model);
            /* book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate; */
            
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }

    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}