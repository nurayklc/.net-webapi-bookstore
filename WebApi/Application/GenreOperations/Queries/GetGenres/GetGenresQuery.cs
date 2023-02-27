using System.Collections.Generic;
using WebApi.DBOperations;
using AutoMapper;
using System.Linq;

namespace WebApi.Application.GenreOperations.Queries.GetGenresQuery
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context {get; set;}
        private readonly IMapper _mapper {get; set;}

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive == true).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}