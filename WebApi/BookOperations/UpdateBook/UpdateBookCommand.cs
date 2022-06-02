using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand 
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        public UpdateBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle(int id)
        {
            var book = _dbcontext.Books.SingleOrDefault(x=> x.Id == id);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunmamaktadır1");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbcontext.SaveChanges();
        }
    }

    public class UpdateBookModel 
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
