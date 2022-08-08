using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.MovieOperations.Queries.GetMoviesQuery;
using WebApi.DbOperations;

namespace WebApi.Applications.MovieOperations.Queries.GetMovieDetailQuery
{
    public class GetMovieDetailQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id;
        public GetMovieDetailQuery(MovieStoreDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }

        public MoviesViewModel Handle()
        {
            var movie = _context.Movies.Include(x => x.MovieActors).
                                        ThenInclude(t => t.Actor).
                                        Include(x => x.Drirector).
                                        Include(x => x.MovieGenres).
                                        ThenInclude(x => x.Genre).
                                        SingleOrDefault(x => x.Id == Id);
            if(movie is null)
                throw new   ArgumentException("Aranan film bulunamadı!");              
            MoviesViewModel movieList = _mapper.Map<MoviesViewModel>(movie);
            return movieList;
        }
    }
}