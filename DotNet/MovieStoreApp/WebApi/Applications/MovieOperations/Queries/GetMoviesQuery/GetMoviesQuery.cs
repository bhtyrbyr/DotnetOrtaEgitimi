using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Applications.MovieOperations.Queries.GetMoviesQuery
{
    public class GetMoviesQuery
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<MoviesViewModel> Handle()
        {
            var movies = _context.Movies.Include(x => x.MovieActors).
                                        ThenInclude(t => t.Actor).
                                        Include(x => x.Drirector).
                                        Include(x => x.MovieGenres).
                                        ThenInclude(x => x.Genre).
                                        OrderBy(x => x.Id).ToList();
            List<MoviesViewModel> movieList = _mapper.Map<List<MoviesViewModel>>(movies);
            return movieList;
        }
    }
    public class MoviesViewModel
    {
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string Director { get; set; }
        public IReadOnlyList<string> Actors { get; set; }
        public int Price { get; set; }
    }
}