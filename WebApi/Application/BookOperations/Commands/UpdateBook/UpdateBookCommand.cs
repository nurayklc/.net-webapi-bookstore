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
        public int BookId { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x=> x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbcontext.SaveChanges();
        }
    }

    public class UpdateBookModel 
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
