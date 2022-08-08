using AutoMapper;
using WebAPI.Applications.AuthorOperations.Commands.CreateAuthors;
using WebAPI.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebAPI.Applications.AuthorOperations.Queries.GetAuthors;
using WebAPI.Applications.BookOperations.Commands.CreateBooks;
using WebAPI.Applications.BookOperations.Queries.GetBooks;
using WebAPI.Applications.GenreOperations.Commands.CreateGenres;
using WebAPI.Applications.GenreOperations.Commands.UpdateGenres;
using WebAPI.Applications.GenreOperations.Queries.GetGenreDetail;
using WebAPI.Applications.GenreOperations.Queries.GetGenres;
using WebAPI.Applications.UserOperations.Commands.CreateTokenCommand;
using WebAPI.Applications.UserOperations.Commands.CreateUserCommand;
using WebAPI.Entitys;

namespace WebAPI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(
                dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)
            ).ForMember(
                dest => dest.Author, opt => opt.MapFrom(src => src.Author.ToString())
            );
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, UpdateGenreModel>();           
            CreateMap<Author, AuthorViewModel>();         
            CreateMap<Author, AuthorDetailViewModel>();          
            CreateMap<CreateAuthorModel, Author>();        
            CreateMap<CreateUserModel, User>();   
        }
    }
}