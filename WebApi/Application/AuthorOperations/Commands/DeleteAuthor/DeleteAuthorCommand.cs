using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.AuthorOperations.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunmamaktadır!");
            if(author.Books.Any(x => x.IsActive == true))
                throw new InvalidOperationException("Yazarın kitabı yayındadır, silemezsiniz!");
            
            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }
}