using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery 
    {
        private readonly BookStoreDbContext  _dbcontext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbcontext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return vm;
        } 
    }
    

    public class BooksViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}