using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.AuthorOperations.UpdateAuthor
{
    public class UpdateAuthorCommand 
    {
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        public int AuthorId { get; set; }
        public UpdateAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı!");

            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;
            _dbcontext.SaveChanges();
        }
    }

    public class UpdateAuthorModel 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
