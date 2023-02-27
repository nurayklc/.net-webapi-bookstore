using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using AutoMapper;
using System.Linq;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context {get; set;}
        private readonly IMapper _mapper {get; set;}

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive == true && x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Kitap bulunamadı");
            return  _mapper.Map<GenreDetailViewModel>(genre);
            
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}