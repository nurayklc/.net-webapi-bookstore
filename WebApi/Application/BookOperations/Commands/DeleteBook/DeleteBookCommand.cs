using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunmamaktadır!");
            
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}