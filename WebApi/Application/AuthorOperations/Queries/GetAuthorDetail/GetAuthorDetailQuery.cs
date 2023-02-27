using AutoMapper;
using System.Reflection.Metadata;
using WebApi.DBOperations;
using WebApi.Entity;

namespace WebApi.Application.AuthorOperations.Queries.GetBookDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(x => x.Id ==AuthorId).ToList();
            var authorMap = _mapper.Map<AuthorDetailViewModel>(author);
            return authorMap;
        }
    }

    public class AuthorDetailViewModel
    { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}