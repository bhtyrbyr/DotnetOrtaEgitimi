using System.Linq;
using AutoMapper;
using WebApi.Applications.MovieOperations.Commands.CreateMovieCommand;
using WebApi.Applications.MovieOperations.Queries.GetMoviesQuery;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesViewModel>().
                ForMember(
                    dest => dest.Director, opt => opt.MapFrom(m =>new string( m.Drirector.Name + " " +  m.Drirector.Surname))
                ).
                ForMember(
                    dest => dest.Actors, opt => opt.MapFrom(x => x.MovieActors.Select(s => s.Actor.Name + " " + s.Actor.Surname))
                ).
                ForMember(
                    dest => dest.Genres, opt => opt.MapFrom(x => x.MovieGenres.Select(s => s.Genre.Name))
                );
            CreateMap<CreateMovieDirectorModel, Director>();

        }
    }
}