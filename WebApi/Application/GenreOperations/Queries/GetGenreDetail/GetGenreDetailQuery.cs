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
        private IBookStoreDbContext _dbContext {get; set;}
        private IMapper _mapper {get; set;}

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive == true && x.Id == GenreId);
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