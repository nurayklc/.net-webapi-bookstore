using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery 
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public GetBookDetailQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(int id)
        {
            var book = _dbcontext.Books.Where(book => book.Id == id).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap bulunmamaktadır!");
            
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            return vm;
        }
    }

    public class BookDetailViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}