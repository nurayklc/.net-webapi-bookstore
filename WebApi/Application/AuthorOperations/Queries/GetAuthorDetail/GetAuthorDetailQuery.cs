using AutoMapper;
using System.Reflection.Metadata;
using WebApi.DBOperations;
using WebApi.Entity;

namespace WebApi.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(x => x.Id == AuthorId).FirstOrDefault();
            var authorMap = _mapper.Map<AuthorDetailViewModel>(author);
            return authorMap;
        }
    }

    public class AuthorDetailViewModel
    { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}