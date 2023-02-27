using System.Security.AccessControl;
using System;
using WebApi.DBOperations;
using System.Linq;
using WebApi.Entity;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public CreateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;   
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(genre is not null)
                throw new InvalidOperationException("Kitap zaten mevcut.");

            genre = new Genre();
            genre.Name = Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}