using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookQuery 
    {
        private readonly BookStoreDbContext _dbcontext;
        public GetBookQuery(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<BookViewModel> Handle(int id)
        {
            var book = _dbcontext.Books.Where(book => book.Id == id).SingleOrDefault();
            List<BookViewModel> vm = new List<BookViewModel>();
            vm.Add(new BookViewModel()
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount
            });
            return vm;
        }
    }

    public class BookViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}