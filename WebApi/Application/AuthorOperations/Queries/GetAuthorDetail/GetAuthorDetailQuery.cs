using System.Reflection.Metadata;
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
            if(author is null)
                return new InvalidOperationException("Yazar Bulunamadı!");
            return authorMap = _mapper.Map<AuthorDetailViewModel>(author);
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