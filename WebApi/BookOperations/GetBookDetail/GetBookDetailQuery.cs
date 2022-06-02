using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery 
    {
        private readonly BookStoreDbContext _dbcontext;
        public GetBookDetailQuery(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public BookDetailViewModel Handle(int id)
        {
            var book = _dbcontext.Books.Where(book => book.Id == id).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap bulunmamaktadır!");
            
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.PageCount = book.PageCount; 
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