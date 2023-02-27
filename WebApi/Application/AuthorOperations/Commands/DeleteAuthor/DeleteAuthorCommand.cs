using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace WebApi.AuthorOperations.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var authorBooks = _dbcontext.Books.SingleOrDefault(a => a.AuthorId == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunmamaktadır!");
            if (authorBooks is not null)
                throw new InvalidOperationException("Yazarın kitabı yayındadır, silemezsiniz!");

            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }
}