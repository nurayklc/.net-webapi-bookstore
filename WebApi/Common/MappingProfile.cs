using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenresQuery;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Entity;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<Genre, GenresViewModel>().ReverseMap();
            CreateMap<Genre, GenreDetailViewModel>().ReverseMap();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<Author, CreateAuthorModel>().ReverseMap();
            CreateMap<Author, UpdateAuthorModel>().ReverseMap();
            CreateMap<Author, AuthorDetailViewModel>().ReverseMap();
            CreateMap<Author, AuthorsViewModel>().ReverseMap();
        }

    }
}